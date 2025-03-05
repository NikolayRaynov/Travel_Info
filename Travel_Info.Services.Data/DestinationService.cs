using Microsoft.AspNetCore.Http;
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
        private readonly ICategoryService categoryService;

        public DestinationService(IRepository repository, ICategoryService categoryService)
        {
            this.repository = repository;
            this.categoryService = categoryService;
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

        public async Task DeleteImageAsync(int destinationId, string imageUrl)
        {
            var destination = await repository.All<Destination>()
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination != null)
            {
                var imageToDelete = destination.Images.FirstOrDefault(i => i.Url == imageUrl);
                if (imageToDelete != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    repository.Delete(imageToDelete);
                    await repository.SaveChangesAsync();
                }
            }
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

        public async Task UpdateAsync(EditDestinationViewModel destinationModel, List<IFormFile> newImages)
        {
            var destination = await repository.GetByIdAsync<Destination>(destinationModel.Id);
            if (destination != null)
            {
                destination.Name = destinationModel.Name;
                destination.Description = destinationModel.Description;

                if (newImages != null && newImages.Count > 0)
                {
                    var category = await categoryService.GetByIdAsync(destination.CategoryId);
                    var categoryFolder = category.NameEn;

                    foreach (var image in newImages)
                    {
                        if (image.Length > 0)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", categoryFolder).ToLower();

                            Directory.CreateDirectory(folderPath);
                            var filePath = Path.Combine(folderPath, fileName).ToLower();

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }

                            destination.Images.Add(new Image { Url = $"/images/{categoryFolder}/{fileName}" });
                        }
                    }
                }

                repository.Update(destination);
                await repository.SaveChangesAsync();

                repository.Update(destination);
                await repository.SaveChangesAsync();
            }
        }

        public async Task DeleteDestinationAsync(int destinationId)
        {
            var destination = await repository
                .All<Destination>()
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination != null)
            {
                var category = await categoryService
                    .GetByIdAsync(destination.CategoryId);

                var categoryFolder = category.NameEn;

                foreach (var image in destination.Images)
                {
                    var fileName = Path.GetFileName(image.Url);
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", categoryFolder).ToLower();
                    var filePath = Path.Combine(folderPath, fileName).ToLower();

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                repository.DeleteRange<Image>(i => i.DestinationId == destinationId);
                await repository.SaveChangesAsync();

                repository.DeleteRange<Review>(r => r.DestinationId == destinationId);
                await repository.SaveChangesAsync();

                repository.Delete(destination);
                await repository.SaveChangesAsync();
            }
        }
    }
}