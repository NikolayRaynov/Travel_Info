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

        public async Task CreateAsync(CreateDestinationViewModel destinationModel)
        {
            var destination = new Destination
            {
                Name = destinationModel.Name,
                Description = destinationModel.Description,
                Images = new List<Image>
                {
                    new Image
                    {
                        Url = destinationModel.ImageUrl
                    }
                }
            };

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
                    ImageUrl = d.Images.FirstOrDefault().Url ?? "/images/NoPhoto.jpg"
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
                return null;
            }

            return new DestinationIndexViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                ImageUrl = destination.Images.FirstOrDefault()?.Url ?? "/images/NoPhoto.jpg",
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