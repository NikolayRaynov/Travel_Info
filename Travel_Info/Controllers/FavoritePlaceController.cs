﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel_Info.Data.Models;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.FavoritePlace;

namespace Travel_Info.Web.Controllers
{
    [Authorize]
    public class FavoritePlaceController : Controller
    {
        private readonly IFavoritePlaceService favoritePlaceService;
        private readonly IDestinationService destinationService;
        private readonly UserManager<ApplicationUser> userManager;

        public FavoritePlaceController(IFavoritePlaceService favoritePlaceService, IDestinationService destinationService, UserManager<ApplicationUser> userManager)
        {
            this.favoritePlaceService = favoritePlaceService;
            this.destinationService = destinationService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favoriteDestinations = await favoritePlaceService.GetAllFavoritesAsync(userId);

            var viewModel = favoriteDestinations.Select(destination => new FavoritePlaceIndexViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                ImageUrl = destination.Images.FirstOrDefault()?.Url ?? "/images/NoPhoto.jpg"
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var destination = await destinationService.GetByIdAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            var viewModel = new AddToFavoriteViewModel
            {
                DestinationId = destination.Id,
                DestinationName = destination.Name,
                DestinationDescription = destination.Description,
                DestinationImageUrl = destination.Images.FirstOrDefault()?.Url ?? "/images/NoPhoto.jpg"
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddToFavoriteViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await favoritePlaceService.AddToFavoritesAsync(model.DestinationId, userId))
            {
                TempData["SuccessMessage"] = "The destination was successfully added to your favorites!";
                return RedirectToAction("Index", "FavoritePlace");
            }
            else
            {
                TempData["ErrorMessage"] = "This destination is already in your favorites.";
                return RedirectToAction("Index", "Destination");
            }
        }
    }
}
