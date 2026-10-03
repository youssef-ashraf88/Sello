using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;

namespace Sello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("products/{id}")]
        public async Task<ActionResult<ReviewResponseDto>> CreateReview(Guid id, CreateOrUpdateReviewDto createReviewDto)
        {
            var newReview = await _reviewService.CreateReview(id, createReviewDto);

            return Ok(newReview);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewResponseDto>> UpdateReview(Guid id, CreateOrUpdateReviewDto updateReviewDto)
        {
            var updatedReview = await _reviewService.UpdateReview(id, updateReviewDto);

            return Ok(updatedReview);
        }
    }
}
