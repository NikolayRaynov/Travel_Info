using Travel_Info.Web.ViewModels.Destination;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IFavoritePlaceService
    {
        Task<bool> AddToFavoritesAsync(int destinationId, string userId);
        Task RemoveFromFavoritesAsync(int destinationId, string userId);
        Task<bool> IsInFavoriteAsync(int destinationId, string userId);
        Task<IEnumerable<DestinationIndexViewModel>> GetAllFavoritesAsync(string userId);
    }
}
