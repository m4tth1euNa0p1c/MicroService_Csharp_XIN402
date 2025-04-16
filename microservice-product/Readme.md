# Microservice Product

Ce microservice gère la gestion des produits pour une application e-commerce. Il est conçu pour être "production-ready" avec une architecture RESTful, une connexion à MongoDB, une sécurisation par JWT et une documentation interactive via Swagger.

## Table des Matières

- [Microservice Product](#microservice-product)
  - [Table des Matières](#table-des-matières)
  - [Vue d'ensemble](#vue-densemble)
  - [Architecture](#architecture)
  - [Technologies Utilisées](#technologies-utilisées)
  - [Structure du Projet](#structure-du-projet)
  - [Configuration](#configuration)
  - [Exécution et Déploiement](#exécution-et-déploiement)
    - [Utilisation avec Docker Compose](#utilisation-avec-docker-compose)
    - [Exécution Locale](#exécution-locale)
  - [Endpoints](#endpoints)
    - [Endpoints Publics (Accès sans JWT)](#endpoints-publics-accès-sans-jwt)
    - [Endpoints Protégés (Accès avec JWT dans le header Authorization)](#endpoints-protégés-accès-avec-jwt-dans-le-header-authorization)
  - [Tests](#tests)
  - [Sécurité](#sécurité)

## Vue d'ensemble

Ce microservice permet de :
- Créer, lire, mettre à jour et supprimer des produits.
- Effectuer des recherches et filtrages avancés.
- Gérer des fonctionnalités spécifiques (trending, nouveaux arrivages, produits vedettes, produits liés, résumé, importation en masse).
- Gérer les avis et notations sur les produits.

## Architecture

L’architecture est basée sur .NET 7 et MongoDB et s’appuie sur Docker pour l’orchestration. Le microservice propose des endpoints publics pour la consultation et des endpoints protégés (via JWT) pour les opérations sensibles (création, mise à jour, suppression).

Le microservice peut être déployé seul ou intégré à une API Gateway pour centraliser et sécuriser l’accès aux différents microservices.

## Technologies Utilisées

- **.NET 7**
- **MongoDB**
- **Docker & Docker Compose**
- **JWT (JSON Web Tokens) pour la sécurité**
- **Swagger** pour la documentation de l’API
- **Newtonsoft.Json** pour le support des JSON Patch

## Structure du Projet

```plaintext
ms-product-service/
├── Controllers/
│   ├── ProductsController.cs   # Endpoints pour la gestion des produits
│   └── ReviewsController.cs    # Endpoints pour la gestion des avis sur les produits
├── Data/
│   └── MongoDbSettings.cs      # Configuration de la connexion MongoDB
├── Models/
│   ├── Product.cs              # Modèle représentant un produit
│   ├── ProductFilter.cs        # Critères de recherche et filtrage
│   ├── Review.cs               # Modèle pour les avis des produits
│   └── StatusUpdateModel.cs    # Modèle pour la mise à jour du statut d'un produit
├── Services/
│   ├── ProductService.cs       # Logique métier pour la gestion des produits
│   └── ReviewService.cs        # Logique métier pour la gestion des avis
├── appsettings.json            # Configuration de base (sera surchargée par les variables d'environnement)
├── Dockerfile                  # Fichier de build Docker
├── ms-product-service.csproj   # Fichier projet .NET
└── Program.cs                  # Point d'entrée de l'application et configuration globale
```

## Configuration

La configuration s’appuie sur le fichier `appsettings.json` ainsi que sur des variables d’environnement injectées via un fichier `.env`.

Exemple de fichier `.env` à la racine :

```dotenv
MongoDbSettings__ConnectionString=mongodb://mongo:27017
MongoDbSettings__DatabaseName=productsdb
MongoDbSettings__ProductsCollectionName=products

JWT__Key=YourStrong!SecretKeyHere
JWT__Issuer=yourdomain.com
JWT__Audience=yourdomain.com

ASPNETCORE_ENVIRONMENT=Production
```

Les doubles underscores (`__`) permettent à .NET de mapper correctement ces variables aux clés de configuration, par exemple `MongoDbSettings:ConnectionString` et `Jwt:Key`.

## Exécution et Déploiement

### Utilisation avec Docker Compose

Pour lancer le microservice ainsi que ses dépendances (MongoDB, API Gateway), utilisez Docker Compose :

```bash
docker-compose up --build
```

Le fichier `docker-compose.yml` doit ressembler à ceci :

```yaml
version: '3.8'
services:
  api-gateway:
    build:
      context: ./ApiGateway
      dockerfile: Dockerfile
    ports:
      - "5000:80"
    depends_on:
      - ms-product-service
    env_file: .env

  ms-product-service:
    build:
      context: ./ms-product-service
      dockerfile: Dockerfile
    ports:
      - "5001:80"
    depends_on:
      - mongo
    env_file: .env

  mongo:
    build:
      context: ./db
      dockerfile: Dockerfile
    ports:
      - "27017:27017"
    environment:
      - MONGO_INITDB_DATABASE=productsdb
```

### Exécution Locale

Vous pouvez également exécuter le microservice localement via Visual Studio ou en utilisant la CLI :

```bash
dotnet run
```

Assurez-vous que les variables d'environnement sont correctement définies (par exemple via un fichier `.env` chargé manuellement).

## Endpoints

### Endpoints Publics (Accès sans JWT)

- **GET** `/api/products`  
  Récupère la liste complète des produits.

- **GET** `/api/products/{id}`  
  Récupère le détail d'un produit par son identifiant.

- **GET** `/api/products/search?nomProduit=Pollock&minPrix=1000&categories=Art&tags=abstrait`  
  Recherche et filtre les produits.

- **GET** `/api/products/trending`  
  Récupère les produits trending (top 10 par date de mise en vente).

- **GET** `/api/products/new-arrivals`  
  Récupère les nouveaux arrivages (produits créés dans les 30 derniers jours).

- **GET** `/api/products/featured`  
  Récupère les produits mis en avant.

- **GET** `/api/products/related/{id}`  
  Récupère des produits liés par catégorie.

- **GET** `/api/products/summary`  
  Retourne des statistiques (nombre total, prix moyen, etc.).

- **GET** `/api/products/{productId}/reviews`  
  Récupère les avis pour un produit.

- **GET** `/api/products/{productId}/reviews/average`  
  Récupère la note moyenne d'un produit.

### Endpoints Protégés (Accès avec JWT dans le header Authorization)

- **POST** `/api/products`  
  Crée un nouveau produit.

- **PUT** `/api/products/{id}`  
  Met à jour complètement un produit existant.

- **PATCH** `/api/products/{id}`  
  Met à jour partiellement un produit (JSON Patch).

- **DELETE** `/api/products/{id}`  
  Supprime un produit.

- **POST** `/api/products/bulk-import`  
  Importe un lot de produits.

- **PUT** `/api/products/{id}/status`  
  Met à jour le statut de disponibilité d'un produit.

- **POST** `/api/products/{productId}/reviews`  
  Ajoute un nouvel avis pour un produit.

> **Note :** Pour les endpoints protégés, ajoutez dans Postman l'en-tête :  
> `Authorization: Bearer {votre_token}`

## Tests

- **Swagger :**  
  En environnement développement, accédez à la documentation interactive à l'URL :  
  `http://localhost:5001/swagger`  
  ou via l'API Gateway : `http://localhost:5000/swagger`.

- **Postman :**  
  Testez les endpoints en utilisant les URLs fournies et en configurant l'en-tête `Authorization` pour les routes protégées.

## Sécurité

- **Authentification JWT :**  
  Seules les routes sensibles (création, mise à jour, suppression, bulk-import, etc.) nécessitent un token JWT valide.
- **Gestion des Secrets :**  
  Les clés et autres variables sensibles sont stockées dans le fichier `.env` et peuvent être gérées via un coffre-fort en production (ex. Azure Key Vault).
- **Communication Sécurisée :**  
  Il est recommandé d'utiliser TLS/HTTPS en production pour sécuriser les échanges. Dans ce projet, la redirection HTTPS est désactivée pour le déploiement en conteneur, mais doit être gérée via un load balancer ou un proxy inverse.

