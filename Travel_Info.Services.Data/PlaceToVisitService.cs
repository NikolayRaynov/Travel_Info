using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;

namespace Travel_Info.Services.Data
{
    public class PlaceToVisitService : IPlaceToVisitService
    {
        private readonly IRepository repository;

        public PlaceToVisitService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddToWishlistAsync(int destinationId, string userId)
        {
            var existingDesiredPlace = await repository.All<PlaceToVisit>()
                .Include(pv => pv.Destinations)
                .FirstOrDefaultAsync(pv => pv.UserId == userId && pv.Destinations.Any(d => d.Id == destinationId));

            if (existingDesiredPlace != null)
            {
                return false;
            }

            var userFavorites = await repository.All<PlaceToVisit>()
                .FirstOrDefaultAsync(pv => pv.UserId == userId);

            if (userFavorites == null)
            {
                userFavorites = new PlaceToVisit
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

        public async Task<IEnumerable<Destination>> GetAllDesiredPlacesAsync(string userId)
        {
            return await repository
                .All<PlaceToVisit>()
                .Where(pv => pv.UserId == userId && !pv.IsDeleted)
                .Include(pv => pv.Destinations)
                .ThenInclude(i => i.Images)
                .SelectMany(pv => pv.Destinations)
                .ToListAsync();

        }

        public async Task<bool> IsInWishlistAsync(int destinationId, string userId)
        {
            return await repository
                .All<PlaceToVisit>()
                .AnyAsync(w => w.UserId == userId && w.Destinations.Any(d => d.Id == destinationId) && !w.IsDeleted);
        }

        public async Task RemoveFromWishlistAsync(int destinationId, string userId)
        {
            var desiredPlaces = await repository
                .All<PlaceToVisit>()
                .Include(pv => pv.Destinations)
                .FirstOrDefaultAsync(pv => pv.UserId == userId && !pv.IsDeleted);

            if (desiredPlaces == null)
            {
                throw new InvalidOperationException("This destination is not in your wishlist");
            }

            var destinationToRemove = desiredPlaces.Destinations.FirstOrDefault(d => d.Id == destinationId);
            if (destinationToRemove != null)
            {
                desiredPlaces.Destinations.Remove(destinationToRemove);
            }

            if (!desiredPlaces.Destinations.Any())
            {
                desiredPlaces.IsDeleted = true;
            }

            repository.Update(desiredPlaces);
            await repository.SaveChangesAsync();
        }
    }
}
