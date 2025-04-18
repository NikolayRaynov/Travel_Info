using Travel_Info.Web.ViewModels.Category;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel> GetByIdAsync(int id);
        Task<CategoryViewModel> GetCategoryForEditAsync(int id);
        Task AddAsync(AddCategoryViewModel model, string webRootPath);
        Task UpdateAsync(int id, CategoryViewModel model, string webRootPath);
        Task<bool> ExistByIdAsync(int id);
        Task<string?> GetCategoryNameEnByIdAsync(int id);
    }
}
