using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ms_product_service.Models
{
    [BsonIgnoreExtraElements] // ← ignorer les champs non définis dans la classe voire : __v.
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("code_produit")]
        public string CodeProduit { get; set; } = string.Empty;

        [BsonElement("code_barre")]
        public string? CodeBarre { get; set; }

        [BsonElement("reference_fabricant")]
        public string ReferenceFabricant { get; set; } = string.Empty;

        [BsonElement("nom_produit")]
        public string NomProduit { get; set; } = string.Empty;

        [BsonElement("description_courte")]
        public string DescriptionCourte { get; set; } = string.Empty;

        [BsonElement("description_longue")]
        public string DescriptionLongue { get; set; } = string.Empty;

        [BsonElement("marque")]
        public string Marque { get; set; } = string.Empty;

        [BsonElement("modele")]
        public string? Modele { get; set; }

        [BsonElement("couleur")]
        public string Couleur { get; set; } = string.Empty;

        [BsonElement("taille")]
        public string Taille { get; set; } = string.Empty;

        [BsonElement("poids")]
        public double Poids { get; set; }

        [BsonElement("unite_poids")]
        public string UnitePoids { get; set; } = string.Empty;

        [BsonElement("dimensions")]
        public string Dimensions { get; set; } = string.Empty;

        [BsonElement("materiaux")]
        public List<string> Materiaux { get; set; } = new();

        [BsonElement("pays_origine")]
        public string PaysOrigine { get; set; } = string.Empty;

        [BsonElement("id_categorie")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdCategorie { get; set; } = string.Empty;

        [BsonElement("sous_categorie")]
        public string? SousCategorie { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = new();

        [BsonElement("prix_vente")]
        public double PrixVente { get; set; }

        [BsonElement("devise")]
        public string Devise { get; set; } = string.Empty;

        [BsonElement("prix_achat")]
        public double PrixAchat { get; set; }

        [BsonElement("taux_tva")]
        public double TauxTVA { get; set; }

        [BsonElement("prix_promotion")]
        public double? PrixPromotion { get; set; }

        [BsonElement("date_debut_promotion")]
        public DateTime? DateDebutPromotion { get; set; }

        [BsonElement("date_fin_promotion")]
        public DateTime? DateFinPromotion { get; set; }

        [BsonElement("quantite_en_stock")]
        public int QuantiteEnStock { get; set; }

        [BsonElement("seuil_alerte_stock")]
        public int SeuilAlerteStock { get; set; }

        [BsonElement("emplacement_stock")]
        public string EmplacementStock { get; set; } = string.Empty;

        [BsonElement("angle_principale")]
        public string AnglePrincipale { get; set; } = string.Empty;

        [BsonElement("angle_1")]
        public string Angle1 { get; set; } = string.Empty;

        [BsonElement("angle_2")]
        public string Angle2 { get; set; } = string.Empty;

        [BsonElement("angle_3")]
        public string Angle3 { get; set; } = string.Empty;

        [BsonElement("url_video")]
        public string? UrlVideo { get; set; }

        [BsonElement("est_nouveaute")]
        public bool EstNouveaute { get; set; }

        [BsonElement("est_meilleure_vente")]
        public bool EstMeilleureVente { get; set; }

        [BsonElement("est_disponible")]
        public bool EstDisponible { get; set; }

        [BsonElement("date_mise_en_vente")]
        public DateTime DateMiseEnVente { get; set; }

        [BsonElement("Frais_de_port")]
        public double FraisDePort { get; set; }

        [BsonElement("Frais_de_port_international")]
        public double FraisDePortInternational { get; set; }

        [BsonElement("Frais_de_port_UE")]
        public double FraisDePortUE { get; set; }

        [BsonElement("categories")]
        public List<string> Categories { get; set; } = new();

        [BsonElement("date_creation")]
        public DateTime DateCreation { get; set; }

        [BsonElement("date_modification")]
        public DateTime DateModification { get; set; }

        [BsonElement("utilisateur_creation")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UtilisateurCreation { get; set; } = string.Empty;

        [BsonElement("utilisateur_modification")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UtilisateurModification { get; set; } = string.Empty;
    }
}
