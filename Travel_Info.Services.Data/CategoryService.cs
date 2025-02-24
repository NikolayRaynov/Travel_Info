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

        public CategoryService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            return await repository.All<Category>()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    NameBg = c.NameBg,
                    NameEn = c.NameEn
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel> GetByIdAsync(int id)
        {
            var category = await repository.GetByIdAsync<Category>(id);
            if (category == null)
            {
                throw new InvalidOperationException("Category not found.");
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                NameBg = category.NameBg,
                NameEn = category.NameEn
            };

            return viewModel;
        }
    }
}
