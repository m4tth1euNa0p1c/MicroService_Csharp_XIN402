using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice_cart.Models
{
    public class CartItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = null!;
        public string CodeProduit { get; set; } = null!;
        public string NomProduit { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public decimal PrixVente { get; set; }
        public string Devise { get; set; } = null!;
        public string AnglePrincipale { get; set; } = null!;
        public int Quantity { get; set; } = 1;
    }
}
