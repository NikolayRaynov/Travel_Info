using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository repository;

        public ReviewService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllReviewsByDestinationIdAsync(int destinationId)
        {
            var reviews = await repository.All<Review>()
                .Where(r => r.DestinationId == destinationId && !r.IsDeleted)
                .ToListAsync();

            return reviews.Select(r => new ReviewViewModel
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                User = r.User.UserName
            }).ToList();
        }

        public async Task<ReviewViewModel> GetReviewByIdAsync(int reviewId)
        {
            var review = await repository.GetByIdAsync<Review>(reviewId);
            if (review == null)
            {
                return null;
            }

            return new ReviewViewModel
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                User = review.UserId,
                DestinationId = review.DestinationId
            };
        }

        public async Task AddReviewAsync(AddReviewViewModel model, string userId)
        {
            var review = new Review
            {
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                DestinationId = model.DestinationId
            };

            await repository.AddAsync(review);
            await repository.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(EditReviewViewModel model, string userId)
        {
            var review = await repository.GetByIdAsync<Review>(model.Id);
            if (review != null && review.UserId == userId)
            {
                review.Rating = model.Rating;
                review.Comment = model.Comment;

                repository.Update(review);
                await repository.SaveChangesAsync();
            }
        }

        public async Task DeleteReviewAsync(DeleteReviewViewModel model, string userId)
        {
            var review = await repository.GetByIdAsync<Review>(model.Id);
            if (review != null && review.UserId == userId)
            {
                review.IsDeleted = true;
                repository.Update(review);
                await repository.SaveChangesAsync();
            }
        }
    }
}