db = db.getSiblingDB("productsdb");

// Supprimer les collections existantes pour repartir de zéro (optionnel)
// db.products.drop();
// db.productStocks.drop();

// Insérer des documents dans la collection "products"
db.products.insertMany([
  {
    _id: ObjectId("6610a07a8b3a91c72d5e1f0a"), // Ajout d'un ObjectId pour simuler un id unique
    code_produit: "POLLOCK-COMP-001",
    code_barre: "1234567890123",
    reference_fabricant: "JP-COMP-1954",
    nom_produit: "Composition de Jackson Pollock",
    description_courte: "Peinture abstraite de Jackson Pollock (1954).",
    description_longue: "Jackson Pollock a peint la lumière blanche en temps de développement de l'expressionnisme abstrait. Jackson Pollock est motivé par Picasso pendant cette période. Grâce à l'expérience du dadaïsme, le développement surréaliste, Jackson Pollock a créé le style de la peinture unique qui concerne l'engagement de tout le corps dans la démonstration de la peinture. Jackson Pollock a dépassé la tradition et produit des images avec un sentiment insubordonné et agnostique..White Light a une place réelle dans l'histoire car c'est la dernière peinture complète créée par le peintre nord-américain Jackson Pollock. Pollock termina la lumière blanche en 1954, peu avant sa mort.",
    marque: "Jackson Pollock",
    modele: null,
    couleur: "Multicolore",
    taille: "75 x 90 x 1.5 cm",
    poids: 1.5,
    unite_poids: "kg",
    dimensions: "75x90x1.5",
    materiaux: ["peinture aérosol", "acrylique", "bois", "feutres"],
    pays_origine: "USA",
    id_categorie: ObjectId("6610a07a8b3a91c72d5e1f0d"), // Clé étrangère vers la catégorie "Art"
    sous_categorie: null,
    tags: ["abstrait", "art moderne", "peinture", "huile", "Expressionnisme"],
    prix_vente: 4088.31,
    devise: "EUR",
    prix_achat: 2000.00,
    taux_tva: 0.20,
    prix_promotion: null,
    date_debut_promotion: null,
    date_fin_promotion: null,
    quantite_en_stock: 10,
    seuil_alerte_stock: 3,
    emplacement_stock: "Entrepôt A, Section 2, Étagère 5",
    angle_principale: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP1%2FPollock_Principale.png?alt=media&token=1012ce66-5d2d-45b8-bda7-afdbc47a3b99",
    angle_1: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP1%2FPollock_A1.png?alt=media&token=0f2e858d-0717-4dd8-8773-6e6429e59be3",
    angle_2: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP1%2FPollock_A2.png?alt=media&token=2636fe71-0ca7-43a7-a693-b62f1c69a678",
    angle_3: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP1%2FPollock_A3.png?alt=media&token=61a46d4a-ab9d-45d1-ab85-0f1de68e26cf",
    url_video: null,
    est_nouveaute: false,
    est_meilleure_vente: false,
    est_disponible: true,
    date_mise_en_vente: new Date("2023-03-15"),
    Frais_de_port: 0.00,
    Frais_de_port_international: 50.00,
    Frais_de_port_UE: 25.00,
    categories: ["Art", "Peinture", "Pollock", "Abstrait", "Expressionnisme"],
    date_creation: new Date(),
    date_modification: new Date(),
    utilisateur_creation: ObjectId("6610a07a8b3a91c72d5e1f0e"), // ID de l'utilisateur ayant créé
    utilisateur_modification: ObjectId("6610a07a8b3a91c72d5e1f0f") // ID de l'utilisateur ayant modifié
  },
  {
    _id: ObjectId("6610a07a8b3a91c72d5e1f10"),
    code_produit: "POLLOCK-CONV-002",
    code_barre: "9876543210987",
    reference_fabricant: "JP-CONV-1952",
    nom_produit: "Convergence de Jackson Pollock",
    description_courte: "Œuvre monumentale \"Convergence\" de Jackson Pollock (1952).",
    description_longue: "Jackson Pollock, maître de l'expressionnisme abstrait, a créé \"Convergence\" en 1952, une œuvre monumentale. Utilisant sa technique révolutionnaire du \"dripping\", Pollock a projeté la peinture sur la toile, créant un chaos contrôlé de couleurs et de formes. Cette œuvre, symbole de liberté et de spontanéité, témoigne de la rupture radicale de Pollock avec les conventions artistiques. \"Convergence\" est une pièce maîtresse de l'art abstrait, reflétant l'énergie et l'innovation de Pollock.",
    marque: "Jackson Pollock",
    modele: null,
    couleur: "Multicolore",
    taille: "2210 x 2800 x 1.9 cm",
    poids: 5.0,
    unite_poids: "kg",
    dimensions: "2210x2800x1.9",
    materiaux: ["peinture aérosol", "acrylique", "bois", "feutres"],
    pays_origine: "USA",
    id_categorie: ObjectId("6610a07a8b3a91c72d5e1f0d"), // Clé étrangère vers la catégorie "Art"
    sous_categorie: null,
    tags: ["abstrait", "art moderne", "peinture", "huile", "Expressionnisme"],
    prix_vente: 12408.71,
    devise: "EUR",
    prix_achat: 6000.00,
    taux_tva: 0.20,
    prix_promotion: 11000.00,
    date_debut_promotion: new Date("2025-04-10"),
    date_fin_promotion: new Date("2025-05-10"),
    quantite_en_stock: 5,
    seuil_alerte_stock: 2,
    emplacement_stock: "Entrepôt B, Section 1, Étagère 2",
    angle_principale: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP2%2FPollock_Principale.png?alt=media&token=c8d42294-7d2d-4951-ba66-ba5843532e00",
    angle_1: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP2%2FPollock_A1.png?alt=media&token=e91f4a01-acfa-42e9-9de1-40c0eb80d86f",
    angle_2: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP2%2FPollock_A2.png?alt=media&token=c26cf704-21d8-4a69-a939-969ed1dd8496",
    angle_3: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP2%2FPollock_A3.png?alt=media&token=0cc711b3-2847-4b5f-bccd-cf1825ac7037",
    url_video: null,
    est_nouveaute: false,
    est_meilleure_vente: true,
    est_disponible: true,
    date_mise_en_vente: new Date("2022-11-20"),
    Frais_de_port: 0.00,
    Frais_de_port_international: 100.00,
    Frais_de_port_UE: 50.00,
    categories: ["Art", "Peinture", "Pollock", "Expressionnisme", "Abstrait"],
    date_creation: new Date(),
    date_modification: new Date(),
    utilisateur_creation: ObjectId("6610a07a8b3a91c72d5e1f0e"),
    utilisateur_modification: ObjectId("6610a07a8b3a91c72d5e1f0f")
  },
  {
    _id: ObjectId("6610a07a8b3a91c72d5e1f11"),
    code_produit: "POLLOCK-RECH-003",
    code_barre: null,
    reference_fabricant: "JP-RECH-1954",
    nom_produit: "Recherche de Jackson Pollock",
    description_courte: "Peinture \"White Light\" de Jackson Pollock (1954).",
    description_longue: "Jackson Pollock, figure clé de l'expressionnisme abstrait, a créé White Light en 1954, peu avant sa mort. Inspiré par Picasso, Pollock a développé un style unique, le dripping, où le corps entier participe à la création. Cette œuvre, ultime aboutissement de son art, témoigne de sa volonté de dépasser les conventions et d'exprimer une liberté artistique radicale. White Light est un jalon dans l'histoire de l'art, symbole de l'audace et de l'innovation de Pollock.",
    marque: "Jackson Pollock",
    modele: null,
    couleur: "Blanc",
    taille: "1245 x 900 x 2.5 cm",
    poids: 2.0,
    unite_poids: "kg",
    dimensions: "1245x900x2.5",
    materiaux: ["peinture huile", "acrylique", "bois", "feutres"],
    pays_origine: "USA",
    id_categorie: ObjectId("6610a07a8b3a91c72d5e1f0d"), // Clé étrangère vers la catégorie "Art"
    sous_categorie: null,
    tags: ["abstrait", "art moderne", "peinture"],
    prix_vente: 66010.00,
    devise: "EUR",
    prix_achat: 30000.00,
    taux_tva: 0.20,
    prix_promotion: null,
    date_debut_promotion: null,
    date_fin_promotion: null,
    quantite_en_stock: 2,
    seuil_alerte_stock: 1,
    emplacement_stock: "Entrepôt A, Section 3, Coffre-fort",
    angle_principale: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP3%2FPollock_Principale.png?alt=media&token=f16a94ed-47a1-45fd-911a-185fb2391c05",
    angle_1: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP3%2FPollock_A1.png?alt=media&token=f094c242-79da-40f0-b54c-c4c5e4709f2a",
    angle_2: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP3%2FPollock_A2.png?alt=media&token=cceca35a-10d1-4877-8b3d-5f5248d43cf8",
    angle_3: "https://firebasestorage.googleapis.com/v0/b/tp-microservice-web.firebasestorage.app/o/product-service%2Fobject%2Fpaint%2FP3%2FPollock_A3.png?alt=media&token=54ded589-e3df-4255-b67e-9f794d94b699",
    url_video: null,
    est_nouveaute: false,
    est_meilleure_vente: false,
    est_disponible: true,
    date_mise_en_vente: new Date("2024-05-01"),
    Frais_de_port: 0.00,
    Frais_de_port_international: 150.00,
    Frais_de_port_UE: 75.00,
    categories: ["Art", "Peinture", "Pollock", "Abstrait"],
    date_creation: new Date(),
    date_modification: new Date(),
    utilisateur_creation: ObjectId("6610a07a8b3a91c72d5e1f0e"),
    utilisateur_modification: ObjectId("6610a07a8b3a91c72d5e1f0f")
  }
]);

// Insérer des documents dans la collection "productStocks"
db.productStocks.insertMany([
  {
    productId: ObjectId("6610a07a8b3a91c72d5e1f0a"),
    quantity: 10,
    warehouseLocation: "Entrepôt A",
    lastUpdated: new Date()
  },
  {
    productId: ObjectId("6610a07a8b3a91c72d5e1f10"),
    quantity: 5,
    warehouseLocation: "Entrepôt B",
    lastUpdated: new Date()
  },
  {
    productId: ObjectId("6610a07a8b3a91c72d5e1f11"),
    quantity: 2,
    warehouseLocation: "Entrepôt A",
    lastUpdated: new Date()
  }
]);

// Insertion dans une collection de catégories (à créer si elle n'existe pas)
db.categories.insertMany([
  { _id: ObjectId("6610a07a8b3a91c72d5e1f0d"), nom: "Art" },
  { _id: ObjectId(), nom: "Peinture" },
  { _id: ObjectId(), nom: "Art Moderne" },
  { _id: ObjectId(), nom: "Abstrait" },
  { _id: ObjectId(), nom: "Expressionnisme Abstrait" }
]);

// Insertion dans une collection d'utilisateurs (à créer si elle n'existe pas)
db.utilisateurs.insertMany([
  { _id: ObjectId("6610a07a8b3a91c72d5e1f0e"), nom: "Admin", email: "admin@example.com" },
  { _id: ObjectId("6610a07a8b3a91c72d5e1f0f"), nom: "Editeur", email: "editor@example.com" }
]);

// Note: Les champs `id_categorie`, `utilisateur_creation`, et `utilisateur_modification`
// sont maintenant des clés étrangères qui référencent des documents existants dans les
// collections `categories` et `utilisateurs` grâce aux `ObjectId()` spécifiques.
// Dans une application réelle, vous auriez besoin de récupérer les IDs existants
// lors de la création des produits.