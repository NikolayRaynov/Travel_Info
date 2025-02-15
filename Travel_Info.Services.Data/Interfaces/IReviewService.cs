using Travel_Info.Data.Models;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllReviewsByDestinationIdAsync(int destinationId);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task AddReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(int reviewId, string userId);
    }
}
