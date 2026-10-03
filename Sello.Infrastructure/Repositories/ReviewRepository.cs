using Microsoft.EntityFrameworkCore;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _db;

        public ReviewRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddReview(Review Review)
        {
            await _db.Reviews.AddAsync(Review);
            await Save();
        }

        public async Task DeleteReview(Review review)
        {
            _db.Reviews.Remove(review);
            await Save();
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            var reviews = await _db.Reviews.AsNoTracking().ToListAsync();
            return reviews;
        }

        public async Task<Review?> GetUserProductReview(Guid userId, Guid productId)
        {
            var review = await _db.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == productId);
            return review;
        }

        public async Task<Review?> GetUserReview(Guid reviewId, Guid? userId)
        {
            var review = _db.Reviews.Where(r => r.Id == reviewId);
            if(userId.HasValue)
            {
                review = review.Where(r => r.UserId == userId);
            }

            return await review.FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
