using Travel_Info.Data.Models;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IDestinationService
    {
        Task<IEnumerable<Destination>> GetAllAsync();
        Task<Destination?> GetByIdAsync(int id);
        Task CreateAsync(Destination destination);
        Task UpdateAsync(Destination destination);
        Task SoftDeleteAsync(Destination destination);
    }
}
