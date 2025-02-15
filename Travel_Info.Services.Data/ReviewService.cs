using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;

namespace Travel_Info.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository repository;

        public ReviewService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddReviewAsync(Review review)
        {
            await repository.AddAsync(review);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int reviewId, string userId)
        {
            var review = await GetReviewByIdAsync(reviewId);
            if (review != null && review.UserId == userId)
            {
                review.IsDeleted = true;
                repository.Update(review);
                await repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetAllReviewsByDestinationIdAsync(int destinationId)
        {
            return await repository.All<Review>()
                .Where(r => r.DestinationId == destinationId && !r.IsDeleted)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await repository.GetByIdAsync<Review>(reviewId);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            repository.Update(review);
            await repository.SaveChangesAsync();
        }
    }
}
