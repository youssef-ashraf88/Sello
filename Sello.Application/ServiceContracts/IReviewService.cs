using Sello.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Application.ServiceContracts
{
    public interface IReviewService
    {
        Task<ReviewResponseDto?> CreateReview(Guid productId, CreateOrUpdateReviewDto createReviewDto);
        Task<ReviewResponseDto?> UpdateReview(Guid reviewId, CreateOrUpdateReviewDto updateReviewDto);
        Task<IEnumerable<ReviewResponseDto>> GetAllReviews();
        Task<bool> DeleteReview(Guid reviewId);
    }
}
