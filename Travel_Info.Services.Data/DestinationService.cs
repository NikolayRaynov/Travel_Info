using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Destination;
using Travel_Info.Web.ViewModels.Review;

using static Travel_Info.Common.ApplicationConstants;

namespace Travel_Info.Services.Data
{
    public class DestinationService : IDestinationService
    {
        private readonly IRepository repository;
        private readonly ICategoryService categoryService;
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly UserManager<ApplicationUser> userManager;

        public DestinationService(IRepository repository, ICategoryService categoryService, IHtmlSanitizer htmlSanitizer, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.categoryService = categoryService;
            this.htmlSanitizer = htmlSanitizer;
            this.userManager = userManager;
        }

        public async Task CreateAsync(AddDestinationViewModel destinationModel, List<IFormFile> images, string userId)
        {
            ValidateImages(images);

            var destination = new Destination
            {
                Name = htmlSanitizer.Sanitize(destinationModel.Name),
                Description = htmlSanitizer.Sanitize(destinationModel.Description),
                CategoryId = destinationModel.CategoryId,
                UserId = userId
            };

            var imageUrls = await SaveImages(images, destinationModel.CategoryId);

            foreach (var imageUrl in imageUrls)
            {
                destination.Images.Add(new Image { Url = imageUrl });
            }

            await repository.AddAsync(destination);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(int destinationId, string imageUrl, string userId)
        {
            var destination = await repository.All<Destination>()
                .Include(d => d.Images)
                .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination != null)
            {
                if (destination.UserId != userId)
                {
                    throw new UnauthorizedAccessException("You are not allowed to delete images from this destination.");
                }

                var imageToDelete = destination.Images.FirstOrDefault(i => i.Url == imageUrl);

                if (imageToDelete != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), DefaultFolder, imageUrl.TrimStart('/'));
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

        public async Task UpdateAsync(EditDestinationViewModel destinationModel, List<IFormFile> newImages, string userId)
        {
            var destination = await repository.GetByIdAsync<Destination>(destinationModel.Id);
            if (destination == null || destination.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not allowed to edit this destination.");
            }

            destination.Name = htmlSanitizer.Sanitize(destinationModel.Name);
            destination.Description = htmlSanitizer.Sanitize(destinationModel.Description);

            if (newImages != null && newImages.Count > 0)
            {
                var imageUrls = await SaveImages(newImages, destination.CategoryId);
                foreach (var imageUrl in imageUrls)
                {
                    destination.Images.Add(new Image { Url = imageUrl });
                }
            }

            repository.Update(destination);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteDestinationAsync(int destinationId, string userId)
        {
            var destination = await repository
                .All<Destination>()
                .Include(d => d.Images)
                .Include(d => d.Reviews)
                .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination != null)
            {
                if (destination.UserId != userId)
                {
                    throw new UnauthorizedAccessException("You are not allowed to delete images from this destination.");
                }

                repository.DeleteRange<Review>(r => r.DestinationId == destinationId);
                await repository.SaveChangesAsync();

                var category = await categoryService.GetByIdAsync(destination.CategoryId);
                var categoryFolder = category.NameEn;

                foreach (var image in destination.Images)
                {
                    var fileName = Path.GetFileName(image.Url);
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), UrlPath, categoryFolder).ToLower();
                    var filePath = Path.Combine(folderPath, fileName).ToLower();

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                repository.Delete(destination);
                await repository.SaveChangesAsync();
            }
        }

        private async Task<List<string>> SaveImages(List<IFormFile> images, int categoryId)
        {
            var category = await categoryService.GetByIdAsync(categoryId);
            var categoryFolder = category.NameEn;
            var imageUrls = new List<string>();

            foreach (var image in images)
            {
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), UrlPath, categoryFolder);
                    Directory.CreateDirectory(folderPath);
                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    imageUrls.Add($"/images/{categoryFolder}/{fileName}");
                }
                else
                {
                    throw new InvalidOperationException("Invalid image. Please upload a .jpg, .jpeg or .png file.");
                }
            }

            return imageUrls;
        }

        private void ValidateImages(List<IFormFile> images)
        {
            foreach (var image in images)
            {
                if (image.Length > 5 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Please upload image up to 5MB.");
                }
            }
        }
    }
}