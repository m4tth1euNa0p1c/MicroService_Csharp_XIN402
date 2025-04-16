using ms_product_service.Data;
using ms_product_service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ms_product_service.Services
{
    public class ReviewService
    {
        private readonly IMongoCollection<Review> _reviewsCollection;

        public ReviewService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _reviewsCollection = mongoDatabase.GetCollection<Review>("reviews");
        }

        public async Task<List<Review>> GetReviewsByProductIdAsync(string productId)
        {
            var filter = Builders<Review>.Filter.Eq(r => r.ProductId, productId);
            return await _reviewsCollection.Find(filter).ToListAsync();
        }

        public async Task CreateReviewAsync(Review review) =>
            await _reviewsCollection.InsertOneAsync(review);

        public async Task<double> GetAverageRatingAsync(string productId)
        {
            var aggregate = await _reviewsCollection.Aggregate()
                .Match(r => r.ProductId == productId)
                .Group(new BsonDocument 
                { 
                    { "_id", "$productId" }, 
                    { "avgRating", new BsonDocument("$avg", "$rating") }
                })
                .FirstOrDefaultAsync();

            return aggregate?["avgRating"]?.ToDouble() ?? 0;
        }
    }
}
