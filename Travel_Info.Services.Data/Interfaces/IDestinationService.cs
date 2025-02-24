using Travel_Info.Web.ViewModels.Destination;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationIndexViewModel>> GetAllAsync();
        Task<DestinationIndexViewModel?> GetByIdAsync(int id);
        Task CreateAsync(AddDestinationViewModel destinationModel, List<string> imageUrls, string userId);
        Task UpdateAsync(EditDestinationViewModel destination);
        Task SoftDeleteAsync(int id);
    }
}
