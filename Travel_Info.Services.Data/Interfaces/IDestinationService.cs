﻿using Microsoft.AspNetCore.Http;
using Travel_Info.Web.ViewModels.Destination;

namespace Travel_Info.Services.Data.Interfaces
{
    public interface IDestinationService
    {
        Task<DestinationPageViewModel> GetAllAsync(int pageNumber, int pageSize, int? categoryId = null);
        Task<DestinationIndexViewModel?> GetByIdAsync(int id);
        Task CreateAsync(AddDestinationViewModel destinationModel, List<IFormFile> images, string userId);
        Task UpdateAsync(EditDestinationViewModel destination, List<IFormFile> newImages, string userId);
        Task DeleteImageAsync(int destinationId, string imageUrl, string userId);
        Task DeleteDestinationAsync(int destinationId, string userId);
    }
}
