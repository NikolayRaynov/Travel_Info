using Travel_Info.Web.ViewModels.Category;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel> GetByIdAsync(int id);
    }
}
