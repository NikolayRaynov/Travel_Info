using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAllReviewsByDestinationIdAsync(int destinationId);
        Task<ReviewViewModel?> GetReviewByIdAsync(int reviewId);
        Task AddReviewAsync(AddReviewViewModel model, string userId);
        Task UpdateReviewAsync(EditReviewViewModel model, string userId);
        Task DeleteReviewAsync(DeleteReviewViewModel model, string userId);
    }
}
