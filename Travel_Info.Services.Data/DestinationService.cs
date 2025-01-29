using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;

namespace Travel_Info.Services.Data
{
    public class DestinationService : IDestinationService
    {
        private readonly IRepository repository;

        public DestinationService(IRepository repo)
        {
            repository = repo;
        }
        public async Task CreateAsync(Destination destination)
        {
            await repository.AddAsync(destination);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await repository.All<Destination>().ToListAsync();
        }

        public async Task<Destination?> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync<Destination>(id);
        }

        public async Task SoftDeleteAsync(Destination destination)
        {
            destination.IsDeleted = true;
            repository.Update(destination);
            await repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destination destination)
        {
            repository.Update(destination);
            await repository.SaveChangesAsync();
        }
    }
}
