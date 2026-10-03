using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sello.Application.DTO;
using Sello.Application.Exceptions;
using Sello.Application.ServiceContracts;
using Sello.Domain.Entities;
using Sello.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sello.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ReviewResponseDto?> CreateReview(Guid productId, CreateOrUpdateReviewDto createReviewDto)
        {
            var existingProduct = await _productRepository.GetProductById(productId);
            if (existingProduct == null)
                throw new NotFoundException("Product not found.");

            var userId = GetUserId();
            var existingReview = await _reviewRepository.GetUserProductReview(userId, productId);
            if (existingReview is not null)
                throw new BadRequestException("Review for this user is already exists.");

            var review = _mapper.Map<Review>(createReviewDto);
            review.ProductId = productId;
            review.UserId = userId;
            await _reviewRepository.AddReview(review);

            return _mapper.Map<ReviewResponseDto>(review);
        }

        public async Task<bool> DeleteReview(Guid reviewId) 
        {
            var review = await _reviewRepository.GetUserReview(reviewId, null);
            if (review == null)
                throw new NotFoundException("Review does not exists.");

            await _reviewRepository.DeleteReview(review);
            return true;
        }

        public async Task<IEnumerable<ReviewResponseDto>> GetAllReviews()
        {
            var reviews = await _reviewRepository.GetAllReviews();
            return _mapper.Map<IEnumerable<ReviewResponseDto>>(reviews);
        }

        public async Task<ReviewResponseDto?> UpdateReview(Guid reviewId, CreateOrUpdateReviewDto updateReviewDto)
        {
            var userId = GetUserId();
            var existingReview = await _reviewRepository.GetUserReview(reviewId, userId);
            if (existingReview == null)
                throw new NotFoundException("Review with this id does not exists.");

            _mapper.Map(updateReviewDto, existingReview);
            await _reviewRepository.Save();

            return _mapper.Map<ReviewResponseDto>(existingReview);
        }



        private Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }
    }
}
