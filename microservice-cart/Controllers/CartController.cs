using microservice_cart.Models;
using microservice_cart.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservice_cart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItem(string userId, [FromBody] CartItem item)
        {
            var updatedCart = await _cartService.AddItemToCartAsync(userId, item);
            return Ok(updatedCart);
        }

        [HttpPut("{userId}/items/{productId}")]
        public async Task<IActionResult> UpdateItemQuantity(string userId, string productId, [FromBody] int newQuantity)
        {
            if (newQuantity <= 0)
                return BadRequest("La quantité doit être supérieure à 0.");

            var updatedCart = await _cartService.UpdateItemQuantityAsync(userId, productId, newQuantity);
            return Ok(updatedCart);
        }

        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveItem(string userId, string productId)
        {
            var updatedCart = await _cartService.RemoveItemFromCartAsync(userId, productId);
            return Ok(updatedCart);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            var updatedCart = await _cartService.ClearCartAsync(userId);
            return Ok(updatedCart);
        }
    }
}
