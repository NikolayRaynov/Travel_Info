using Ganss.Xss;
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
        private readonly IHtmlSanitizer htmlSanitizer;

        public ReviewService(IRepository repository, IHtmlSanitizer htmlSanitizer)
        {
            this.repository = repository;
            this.htmlSanitizer = htmlSanitizer;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllReviewsByDestinationIdAsync(int destinationId)
        {
            var reviews = await repository.All<Review>()
                .Include(d => d.Destination)
                .Include(u => u.User)
                .Where(r => r.DestinationId == destinationId)
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
                throw new InvalidOperationException("Review not found");
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
            var destinationExists = await repository.All<Destination>()
                    .AnyAsync(d => d.Id == model.DestinationId);

            if (!destinationExists)
            {
                throw new InvalidOperationException("The destination does not exist.");
            }

            model.Comment = htmlSanitizer.Sanitize(model.Comment);

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
            model.Comment = htmlSanitizer.Sanitize(model.Comment);

            var review = await repository.GetByIdAsync<Review>(model.Id);

            if (review != null && review.UserId == userId)
            {
                review.Rating = model.Rating;
                review.Comment = model.Comment;

                repository.Update(review);
                await repository.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Review is not found or you are not allowed to edit it");
            }
        }

        public async Task DeleteReviewAsync(DeleteReviewViewModel model, string userId)
        {
            var review = await repository.GetByIdAsync<Review>(model.Id);
            if (review != null && review.UserId == userId)
            {
                repository.Delete(review);
                await repository.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Review is not found or you are not allowed to edit it");
            }
        }
    }
}