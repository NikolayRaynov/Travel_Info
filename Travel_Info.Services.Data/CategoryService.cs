using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;

namespace Travel_Info.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;
        private readonly IFileService fileService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryService(IRepository repository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            this.repository = repository;
            this.webHostEnvironment = webHostEnvironment;
            this.fileService = fileService;
        }

        public async Task AddAsync(AddCategoryViewModel model, string webRootPath)
        {
            string sanitizedNameEn = fileService.SanitizeFolderName(model.NameEn);
            string categoryFolderPath = Path.Combine(webRootPath, "images", sanitizedNameEn);

            try
            {
                if (!Directory.Exists(categoryFolderPath))
                {
                    Directory.CreateDirectory(categoryFolderPath);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to create a folder: {categoryFolderPath}", ex);
            }

            Category category = new Category()
            {
                NameBg = model.NameBg,
                NameEn = model.NameEn
            };

            await repository.AddAsync(category);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            return await repository.AllReadonly<Category>().AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            return await repository.AllReadonly<Category>()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    NameBg = c.NameBg,
                    NameEn = c.NameEn
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel?> GetByIdAsync(int id)
        {
            var category = await repository
                .AllReadonly<Category>()
                .Where(c => c.Id == id)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    NameBg = c.NameBg,
                    NameEn = c.NameEn
                })
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<CategoryViewModel?> GetCategoryForEditAsync(int id)
        {
            var category = await repository
                .AllReadonly<Category>()
                .Where(c => c.Id == id)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    NameBg = c.NameBg,
                    NameEn = c.NameEn
                })
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<string?> GetCategoryNameEnByIdAsync(int id)
        {
            return await repository.AllReadonly<Category>()
                                   .Where(c => c.Id == id)
                                   .Select(c => c.NameEn)
                                   .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, CategoryViewModel model, string webRootPath)
        {
            var category = await repository.GetByIdAsync<Category>(id);

            if (category == null)
            {
                throw new InvalidOperationException("The category is not found.");
            }

            string oldNameEn = fileService.SanitizeFolderName(category.NameEn);
            string newNameEn = fileService.SanitizeFolderName(model.NameEn);

            if (oldNameEn != newNameEn)
            {
                string oldFolderPath = Path.Combine(webRootPath, "images", oldNameEn);
                string newFolderPath = Path.Combine(webRootPath, "images", newNameEn);

                try
                {
                    if (Directory.Exists(oldFolderPath))
                    {
                        if (Directory.Exists(newFolderPath))
                        {
                            throw new IOException($"The target folder '{newFolderPath}' already exists. Renaming is impossible.");
                        }

                        Directory.Move(oldFolderPath, newFolderPath);
                    }
                }
                catch (Exception ex)
                {
                    throw new IOException($"Failed to rename/create folder from '{oldFolderPath}' to '{newFolderPath}'", ex);
                }
            }

            category.NameEn = newNameEn.ToLower();
            category.NameBg = model.NameBg;

            await repository.SaveChangesAsync();
        }
    }
}
