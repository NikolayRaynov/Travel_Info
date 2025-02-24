using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Destination;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Services.Data
{
    public class DestinationService : IDestinationService
    {
        private readonly IRepository repository;

        public DestinationService(IRepository repo)
        {
            repository = repo;
        }

        public async Task CreateAsync(AddDestinationViewModel destinationModel, List<string> imageUrls, string userId)
        {
            var destination = new Destination
            {
                Name = destinationModel.Name,
                Description = destinationModel.Description,
                CategoryId = destinationModel.CategoryId,
                UserId = userId
            };

            foreach (var imageUrl in imageUrls)
            {
                destination.Images.Add(new Image { Url = imageUrl });
            }

            await repository.AddAsync(destination);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DestinationIndexViewModel>> GetAllAsync()
        {
            return await repository.All<Destination>()
                .Include(d => d.Images)
                .Select(d => new DestinationIndexViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    ImageUrls = d.Images.Select(i => i.Url).ToList(),
                    UserId = d.UserId
                })
                .ToListAsync();
        }

        public async Task<DestinationIndexViewModel?> GetByIdAsync(int id)
        {
            var destination = await repository.All<Destination>()
                .Include(d => d.Images)
                .Include(d => d.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (destination == null)
            {
                throw new InvalidOperationException("Destination not found");
            }

            return new DestinationIndexViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                ImageUrls = destination.Images
                    .Select(i => i.Url).ToList(),
                Reviews = destination.Reviews
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    User = r.User.UserName
                }).ToList()
            };
        }

        public async Task SoftDeleteAsync(int id)
        {
            var destination = await repository.GetByIdAsync<Destination>(id);
            if (destination != null)
            {
                destination.IsDeleted = true;
                repository.Update(destination);
                await repository.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(EditDestinationViewModel destinationModel)
        {
            var destination = await repository.GetByIdAsync<Destination>(destinationModel.Id);
            if (destination != null)
            {
                destination.Name = destinationModel.Name;
                destination.Description = destinationModel.Description;

                if (!string.IsNullOrEmpty(destinationModel.ImageUrl))
                {
                    if (destination.Images.Any())
                    {
                        destination.Images.First().Url = destinationModel.ImageUrl;
                    }
                    else
                    {
                        destination.Images.Add(new Image { Url = destinationModel.ImageUrl });
                    }
                }

                repository.Update(destination);
                await repository.SaveChangesAsync();
            }
        }
    }
}