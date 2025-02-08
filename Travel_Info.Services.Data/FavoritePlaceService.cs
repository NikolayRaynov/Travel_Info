using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;

namespace Travel_Info.Services.Data
{
    public class FavoritePlaceService : IFavoritePlaceService
    {
        private readonly IRepository repository;

        public FavoritePlaceService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task RemoveFromFavoritesAsync(int destinationId, string userId)
        {
            var favoritePlace = await repository
                .All<FavoritePlace>()
                .FirstOrDefaultAsync(f => f.UserId == userId && f.Destinations.Any(d => d.Id == destinationId) && !f.IsDeleted);

            if (favoritePlace == null)
            {
                throw new InvalidOperationException("This destination is not in your favorites.");
            }

            favoritePlace.IsDeleted = true;
            repository.Update(favoritePlace);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> IsInFavoriteAsync(int destinationId, string userId)
        {
            return await repository
                .All<FavoritePlace>()
                .AnyAsync(f => f.UserId == userId && f.Destinations.Any(d => d.Id == destinationId) && !f.IsDeleted);
        }

        public async Task<IEnumerable<Destination>> GetAllFavoritesAsync(string userId)
        {
            return await repository
                .All<FavoritePlace>()
                .Where(fp => fp.UserId == userId && !fp.IsDeleted)
                .Include(fp => fp.Destinations)
                .ThenInclude(i => i.Images)
                .SelectMany(fp => fp.Destinations)
                .ToListAsync();
        }

        public async Task<bool> AddToFavoritesAsync(int destinationId, string userId)
        {
            var existingFavorite = await repository.All<FavoritePlace>()
                .Include(fp => fp.Destinations)
                .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.Destinations.Any(d => d.Id == destinationId));

            if (existingFavorite != null)
            {
                return false;
            }

            var userFavorites = await repository.All<FavoritePlace>()
                .FirstOrDefaultAsync(fp => fp.UserId == userId);

            if (userFavorites == null)
            {
                userFavorites = new FavoritePlace
                {
                    UserId = userId,
                    Destinations = new List<Destination>()
                };

                await repository.AddAsync(userFavorites);
            }

            var destination = await repository.GetByIdAsync<Destination>(destinationId);
            if (destination != null)
            {
                userFavorites.Destinations.Add(destination);
            }

            await repository.SaveChangesAsync();

            return true;
        }
    }
}
