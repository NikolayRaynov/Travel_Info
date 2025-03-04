﻿using Microsoft.AspNetCore.Http;
using Travel_Info.Web.ViewModels.Destination;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationIndexViewModel>> GetAllAsync();
        Task<DestinationIndexViewModel?> GetByIdAsync(int id);
        Task CreateAsync(AddDestinationViewModel destinationModel, List<string> imageUrls, string userId);
        Task UpdateAsync(EditDestinationViewModel destination, List<IFormFile> newImages);
        Task SoftDeleteAsync(int id);
        Task DeleteImageAsync(int destinationId, string imageUrl);
        Task DeleteDestinationAsync(int destinationId);
    }
}
