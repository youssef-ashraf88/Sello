using Sello.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface IReviewRepository
    {
        Task AddReview(Review review);
        Task<Review?> GetUserReview(Guid reviewId, Guid? userId);
        Task<Review?> GetUserProductReview(Guid userId, Guid productId);
        Task<IEnumerable<Review>> GetAllReviews();
        Task DeleteReview(Review review);
        Task Save();
    }
}
