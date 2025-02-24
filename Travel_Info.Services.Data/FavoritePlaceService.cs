using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Destination;

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
                .Include(fp => fp.Destinations)
                .FirstOrDefaultAsync(fp => fp.UserId == userId && !fp.IsDeleted);

            if (favoritePlace == null)
            {
                throw new InvalidOperationException("This destination is not in your favorites.");
            }

            var destinationToRemove = favoritePlace.Destinations
                .FirstOrDefault(d => d.Id == destinationId);

            if (destinationToRemove != null)
            {
                favoritePlace.Destinations.Remove(destinationToRemove);
            }

            if (!favoritePlace.Destinations.Any())
            {
                favoritePlace.IsDeleted = true;
            }

            repository.Update(favoritePlace);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> IsInFavoriteAsync(int destinationId, string userId)
        {
            return await repository
                .All<FavoritePlace>()
                .AnyAsync(f => f.UserId == userId && f.Destinations
                .Any(d => d.Id == destinationId) && !f.IsDeleted);
        }

        public async Task<IEnumerable<DestinationIndexViewModel>> GetAllFavoritesAsync(string userId)
        {
            return await repository
                .All<FavoritePlace>()
                .Where(pv => pv.UserId == userId && !pv.IsDeleted)
                .Include(pv => pv.Destinations)
                .ThenInclude(d => d.Images)
                .SelectMany(pv => pv.Destinations)
                .Select(d => new DestinationIndexViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    ImageUrls = d.Images
                        .Select(i => i.Url)
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> AddToFavoritesAsync(int destinationId, string userId)
        {
            var existingFavorite = await repository.All<FavoritePlace>()
                .Include(fp => fp.Destinations)
                .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.Destinations
                .Any(d => d.Id == destinationId));

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

            var destination = await repository
                .GetByIdAsync<Destination>(destinationId);

            if (destination != null)
            {
                userFavorites.Destinations.Add(destination);
            }

            await repository.SaveChangesAsync();

            return true;
        }
    }
}
