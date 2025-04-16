using ms_product_service.Data;
using ms_product_service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ms_product_service.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _productsCollection = mongoDatabase.GetCollection<Product>(settings.Value.ProductsCollectionName);
        }

        public async Task<List<Product>> GetAsync() =>
            await _productsCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _productsCollection.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product updatedProduct) =>
            await _productsCollection.ReplaceOneAsync(p => p.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await _productsCollection.DeleteOneAsync(p => p.Id == id);

        public async Task<List<Product>> GetFilteredAsync(ProductFilter filter)
        {
            var builder = Builders<Product>.Filter;
            FilterDefinition<Product> filterDefinition = builder.Empty;

            if (!string.IsNullOrEmpty(filter.NomProduit))
            {
                filterDefinition &= builder.Regex("nomProduit", new BsonRegularExpression(filter.NomProduit, "i"));
            }

            if (filter.Categories != null && filter.Categories.Any())
            {
                var catFilters = filter.Categories.Select(catValue =>
                    builder.Regex("categories.0", new BsonRegularExpression(catValue, "i"))
                );
                filterDefinition &= builder.Or(catFilters);
            }

            if (filter.Tags != null && filter.Tags.Any())
            {
                filterDefinition &= builder.In("tags", filter.Tags);
            }

            return await _productsCollection.Find(filterDefinition).ToListAsync();
        }

        public async Task<List<Product>> GetTrendingAsync() =>
            await _productsCollection.Find(_ => true)
                .SortByDescending(p => p.DateMiseEnVente)
                .Limit(10)
                .ToListAsync();

        public async Task<List<Product>> GetNewArrivalsAsync()
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            var filter = Builders<Product>.Filter.Gte(p => p.DateCreation, thirtyDaysAgo);
            return await _productsCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Product>> GetFeaturedAsync()
        {
            var filter = Builders<Product>.Filter.Eq(p => p.EstMeilleureVente, true);
            return await _productsCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Product>> GetRelatedAsync(string id)
        {
            var product = await GetAsync(id);
            if (product == null)
                return new List<Product>();

            var filter = Builders<Product>.Filter.And(
                Builders<Product>.Filter.Ne(p => p.Id, id),
                Builders<Product>.Filter.AnyIn(p => p.Categories, product.Categories)
            );
            return await _productsCollection.Find(filter).Limit(5).ToListAsync();
        }

        public async Task<dynamic> GetSummaryAsync()
        {
            var totalCount = await _productsCollection.CountDocumentsAsync(_ => true);
            var aggregate = await _productsCollection.Aggregate()
                .Group(new BsonDocument
                {
                    { "_id", BsonNull.Value },
                    { "avgPrice", new BsonDocument("$avg", "$prix_vente") }
                })
                .FirstOrDefaultAsync();

            return new { totalCount, averagePrice = aggregate?["avgPrice"]?.ToDouble() ?? 0 };
        }

        public async Task BulkImportAsync(List<Product> products) =>
            await _productsCollection.InsertManyAsync(products);
    }
}
