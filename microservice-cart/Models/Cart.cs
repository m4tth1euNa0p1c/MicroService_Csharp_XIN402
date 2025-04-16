using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace microservice_cart.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; } = null!;

        [BsonElement("items")]
        public List<CartItem> Items { get; set; } = new();
    }
}
