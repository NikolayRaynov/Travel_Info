using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Destination;

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
            var existingDesiredPlace = await repository
                .All<PlaceToVisit>()
                .Include(pv => pv.Destinations)
                .FirstOrDefaultAsync(pv => pv.UserId == userId && pv.Destinations
                .Any(d => d.Id == destinationId));

            if (existingDesiredPlace != null)
            {
                return false;
            }

            var userWishlist = await repository
                .All<PlaceToVisit>()
                .FirstOrDefaultAsync(pv => pv.UserId == userId);

            if (userWishlist == null)
            {
                userWishlist = new PlaceToVisit
                {
                    UserId = userId,
                    Destinations = new List<Destination>()
                };

                await repository.AddAsync(userWishlist);
            }

            var destination = await repository
                .GetByIdAsync<Destination>(destinationId);

            if (destination != null)
            {
                userWishlist.Destinations.Add(destination);
            }

            await repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DestinationIndexViewModel>> GetAllDesiredPlacesAsync(string userId)
        {
            return await repository
                .All<PlaceToVisit>()
                .Where(pv => pv.UserId == userId)
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

        public async Task<bool> IsInWishlistAsync(int destinationId, string userId)
        {
            return await repository
                .All<PlaceToVisit>()
                .AnyAsync(w => w.UserId == userId && w.Destinations
                .Any(d => d.Id == destinationId));
        }

        public async Task RemoveFromWishlistAsync(int destinationId, string userId)
        {
            var desiredPlaces = await repository
                .All<PlaceToVisit>()
                .Include(pv => pv.Destinations)
                .FirstOrDefaultAsync(pv => pv.UserId == userId);

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
                repository.Delete(desiredPlaces);
            }
            else
            {
                repository.Update(desiredPlaces);
            }

            repository.Update(desiredPlaces);
            await repository.SaveChangesAsync();
        }
    }
}
