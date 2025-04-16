## **1. L’API Gateway – YARP (port 5000)**

C’est **le point d’entrée unique** pour toutes les requêtes externes (du frontend ou autres apps). Elle :

- Redirige les appels vers les bons microservices (`/api/products`, `/cart`, `/search`, etc.) selon les règles définies dans le fichier `appsettings.json`.
- Gère la **sécurité avec JWT** : seuls les utilisateurs authentifiés peuvent accéder à certains endpoints (comme ajouter un produit ou modifier un panier).
- Permet un **contrôle centralisé** des logs, du CORS, et de l’authentification.

**Avantage :** Le frontend n’a besoin de connaître qu’un seul domaine/API, la Gateway s’occupe du reste.

---

## **2. Microservice Produits – `ms-product-service` (port 5001)**

Ce service est **dédié à la gestion du catalogue produit**. Il permet de :

- Ajouter, mettre à jour ou supprimer un produit (endpoints sécurisés par JWT),
- Rechercher des produits avec filtres (nom, catégories, tags),
- Lister les nouveautés, les tendances ou les produits vedettes.

Il est connecté à **MongoDB (`productsdb`)**, expose une API REST via un contrôleur, et sa logique métier est gérée via un `ProductService`.

**Particularité :** Le produit est très riche : images, prix, TVA, stock, dimensions, etc., ce qui permet une grande précision côté frontend.

---

## **3. Microservice Panier – `ms-cart-service` (port 5002)**

Ce service est **responsable des paniers utilisateurs** :

- Il gère la création, la modification, la suppression et la récupération du panier d’un utilisateur.
- Chaque action (ajout, suppression, vidage du panier) est encapsulée dans des méthodes claires du `CartService`.
- Les données sont stockées dans **MongoDB (`cartsdb`)**, totalement isolée.

**Atout technique :** Lorsqu’un produit est ajouté au panier, ses données sont copiées, ce qui évite les incohérences si le catalogue évolue.

---

## **4. Microservice Search WebSocket – `search-ws` (port 3003)**

Ce microservice propose **deux interfaces de recherche** :

- Un **endpoint HTTP** : pour rechercher des produits par nom, description ou catégorie (recherche classique).
- Une **interface WebSocket** : qui permet une **recherche en temps réel**, très utile pour l’autocomplétion côté frontend.

Il interroge la base `productsdb` via Mongoose et retourne un maximum de 10 résultats filtrés.

**Atout :** Optimisé pour la performance et l’interaction instantanée avec le client.

---

## **5. Microservice Admin – `admin` (port 5008)**

Ce service est **dédié à la gestion du catalogue produit côté administrateur** :

- Il permet de **créer, mettre à jour et supprimer** des produits,
- Gère l’**upload des images vers Firebase Storage** (angle principal, angle 1/2/3),
- Utilise **Firebase Admin SDK** pour publier automatiquement les images et stocker les URLs dans la base MongoDB.

**Architecture :** Node.js avec Express + Mongoose, routes REST + multer pour gérer les fichiers.

---

## **6. MongoDB Produits – `mongo-product` (port 27017)**

- Stocke les produits pour `ms-product-service` et `search-ws`.
- Complètement isolée des autres bases Mongo (pas de conflit de données).
- Initialisée automatiquement à la création du conteneur.

---

## **7. MongoDB Panier – `mongo-cart` (port 27018)**

- Stocke les paniers utilisateurs de manière indépendante.
- Cette séparation permet des sauvegardes ciblées et une meilleure performance.

---

## **Flux de fonctionnement complet (exemple d’usage)**

1. L’utilisateur navigue sur le frontend et cherche un tableau abstrait.
2. Il tape "abstrait" : le **search-ws** en WebSocket affiche des résultats instantanés.
3. Il clique sur un produit => requête GET `/api/products/{id}` via l’API Gateway.
4. Il l’ajoute au panier => requête POST `/cart/{userId}/items` vers `ms-cart-service`.
5. En tant qu’admin, un utilisateur connecté envoie une requête POST `/api/products` avec images => le **microservice admin** stocke les images sur Firebase et enregistre le produit.
