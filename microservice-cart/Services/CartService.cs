using microservice_cart.Data;
using microservice_cart.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace microservice_cart.Services
{
    public class CartService
    {
        private readonly IMongoCollection<Cart> _cartsCollection;

        public CartService(IOptions<CartDbSettings> cartDbSettings)
        {
            var mongoClient = new MongoClient(cartDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(cartDbSettings.Value.DatabaseName);
            _cartsCollection = mongoDatabase.GetCollection<Cart>(cartDbSettings.Value.CollectionName);
        }

        public async Task<Cart?> GetOrCreateCartAsync(string userId)
        {
            var cart = await _cartsCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                await _cartsCollection.InsertOneAsync(cart);
            }
            return cart;
        }

        public async Task<Cart> AddItemToCartAsync(string userId, CartItem newItem)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == newItem.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += newItem.Quantity;
            }
            else
            {
                cart.Items.Add(newItem);
            }
            await _cartsCollection.ReplaceOneAsync(c => c.Id == cart.Id, cart);
            return cart;
        }

        public async Task<Cart> UpdateItemQuantityAsync(string userId, string productId, int newQuantity)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = newQuantity;
                await _cartsCollection.ReplaceOneAsync(c => c.Id == cart.Id, cart);
            }
            return cart;
        }

        public async Task<Cart> RemoveItemFromCartAsync(string userId, string productId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            cart.Items.RemoveAll(i => i.ProductId == productId);
            await _cartsCollection.ReplaceOneAsync(c => c.Id == cart.Id, cart);
            return cart;
        }

        public async Task<Cart> ClearCartAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            cart.Items.Clear();
            await _cartsCollection.ReplaceOneAsync(c => c.Id == cart.Id, cart);
            return cart;
        }

        public async Task<Cart> GetCartAsync(string userId)
        {
            var cart = await _cartsCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return cart ?? new Cart { UserId = userId, Items = new List<CartItem>() };
        }
    }
}
