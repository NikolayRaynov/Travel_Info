using Travel_Info.Web.ViewModels.Destination;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IPlaceToVisitService
    {
        Task<bool> AddToWishlistAsync(int destinationId, string userId);
        Task RemoveFromWishlistAsync(int destinationId, string userId);
        Task<bool> IsInWishlistAsync(int destinationId, string userId);
        Task<IEnumerable<DestinationIndexViewModel>> GetAllDesiredPlacesAsync(string userId);
    }
}
