using Travel_Info.Data.Models;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IFavoritePlaceService
    {
        Task<bool> AddToFavoritesAsync(int destinationId, string userId);
        Task RemoveFromFavoritesAsync(int destinationId, string userId);
        Task<bool> IsInFavoriteAsync(int destinationId, string userId);
        Task<IEnumerable<Destination>> GetAllFavoritesAsync(string userId);
    }
}
