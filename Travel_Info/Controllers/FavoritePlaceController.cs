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

        public FavoritePlaceController(IFavoritePlaceService favoritePlaceService, 
                                        IDestinationService destinationService, 
                                        UserManager<ApplicationUser> userManager)
        {
            this.favoritePlaceService = favoritePlaceService;
            this.destinationService = destinationService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var favoriteDestinations = await favoritePlaceService.GetAllFavoritesAsync(userId);

                return View(favoriteDestinations);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            try
            {
                var destination = await destinationService.GetByIdAsync(id);

                if (destination == null)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (await favoritePlaceService.IsInFavoriteAsync(id, userId))
                {
                    TempData["ErrorMessage"] = "This destination is already in your favorites.";
                    return RedirectToAction("Index", "Destination");
                }

                var viewModel = new AddToFavoriteViewModel
                {
                    DestinationId = destination.Id,
                    DestinationName = destination.Name,
                    DestinationDescription = destination.Description,
                    DestinationImageUrls = destination.ImageUrls
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToFavoriteViewModel model)
        {
            if (model.DestinationId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid destination.";
                return RedirectToAction(nameof(Index), "Destination");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var success = await favoritePlaceService.AddToFavoritesAsync(model.DestinationId, userId);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the destination to your favorites.";
                    return RedirectToAction(nameof(Index), "Destination");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var destination = await destinationService.GetByIdAsync(id);
                if (destination == null)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
                }

                var viewModel = new DeleteFavoriteViewModel
                {
                    DestinationId = destination.Id,
                    DestinationName = destination.Name,
                    DestinationDescription = destination.Description,
                    DestinationImageUrls = destination.ImageUrls
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteFavoriteViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await favoritePlaceService.RemoveFromFavoritesAsync(model.DestinationId, userId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }
    }
}
