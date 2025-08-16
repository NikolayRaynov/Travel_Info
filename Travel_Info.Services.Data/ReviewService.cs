using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Services.Data
{
    using static Travel_Info.Common.ApplicationConstants;
    public class ReviewService : IReviewService
    {
        private readonly IRepository repository;
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly UserManager<ApplicationUser> userManager;
        public ReviewService(IRepository repository, IHtmlSanitizer htmlSanitizer, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.htmlSanitizer = htmlSanitizer;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllReviewsByDestinationIdAsync(int destinationId)
        {
            var reviews = await repository
                .AllReadonly<Review>()
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
                User = r.User.UserName,
                DestinationId = r.DestinationId
            }).ToList();
        }

        public async Task<ReviewViewModel?> GetReviewByIdAsync(int reviewId)
        {
            var review = await repository.AllReadonly<Review>()
                                         .Include(r => r.User)
                                         .FirstOrDefaultAsync(r => r.Id == reviewId);
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
                User = review.User.UserName,
                DestinationId = review.DestinationId
            };
        }

        public async Task AddReviewAsync(AddReviewViewModel model, string userId)
        {
            var destinationExists = await repository
                .AllReadonly<Destination>()
                .AnyAsync(d => d.Id == model.DestinationId);

            if (!destinationExists)
            {
                throw new InvalidOperationException("The destination does not exist.");
            }

            var userExists = await userManager.FindByIdAsync(userId) != null;
            if (!userExists)
            {
                throw new InvalidOperationException("The user does not exist.");
            }

            var review = new Review
            {
                Rating = model.Rating,
                Comment = htmlSanitizer.Sanitize(model.Comment),
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                DestinationId = model.DestinationId
            };

            await repository.AddAsync(review);
            await repository.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(EditReviewViewModel model, string userId)
        {
            var review = await repository
                .All<Review>()
                .FirstOrDefaultAsync(r => r.Id == model.Id);

            if (review == null)
            {
                throw new InvalidOperationException("Review is not found.");
            }

            bool hasPermission = review.UserId == userId || await IsUserAdmin(userId);

            if (hasPermission)
            {
                review.Rating = model.Rating;
                review.Comment = htmlSanitizer.Sanitize(model.Comment!);

                await repository.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("You are not allowed to edit this review.");
            }
        }

        public async Task DeleteReviewAsync(DeleteReviewViewModel model, string userId)
        {
            var review = await repository
                .All<Review>()
                .FirstOrDefaultAsync(r => r.Id == model.Id);

            if (review == null)
            {
                throw new InvalidOperationException("Review is not found.");
            }

            bool hasPermission = review.UserId == userId || await IsUserAdmin(userId);

            if (hasPermission)
            {
                repository.Delete(review);
                await repository.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedAccessException("You are not allowed to delete this review.");
            }
        }

        private async Task<bool> IsUserAdmin(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user != null && await userManager.IsInRoleAsync(user, AdminRoleName);
        }
    }
}