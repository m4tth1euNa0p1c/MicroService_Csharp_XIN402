using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ms_product_service.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("productId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = string.Empty;
        
        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;
        
        [BsonElement("rating")]
        public int Rating { get; set; }
        
        [BsonElement("comment")]
        public string Comment { get; set; } = string.Empty;
        
        [BsonElement("dateCreation")]
        public DateTime DateCreation { get; set; }
    }
}
