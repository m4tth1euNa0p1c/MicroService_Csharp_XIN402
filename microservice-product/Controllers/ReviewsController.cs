using Microsoft.AspNetCore.Mvc;
using ms_product_service.Models;
using ms_product_service.Services;
using Microsoft.AspNetCore.Authorization;

namespace ms_product_service.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        
        public ReviewsController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Review>>> GetReviews(string productId) =>
            Ok(await _reviewService.GetReviewsByProductIdAsync(productId));

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(string productId, [FromBody] Review review)
        {
            if (review == null)
                return BadRequest("Review data is null.");
            review.ProductId = productId;
            review.DateCreation = DateTime.UtcNow;
            await _reviewService.CreateReviewAsync(review);
            return Ok(review);
        }

        [HttpGet("average")]
        [AllowAnonymous]
        public async Task<ActionResult<double>> GetAverageRating(string productId) =>
            Ok(await _reviewService.GetAverageRatingAsync(productId));
    }
}
