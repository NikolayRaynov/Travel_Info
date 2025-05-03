using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel_Info.Data.Models;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.PlaceToVisit;

namespace Travel_Info.Controllers
{
    [Authorize]
    public class PlaceToVisitController : Controller
    {
        private readonly IPlaceToVisitService placeToVisitService;
        private readonly IDestinationService destinationService;
        private readonly UserManager<ApplicationUser> userManager;

        public PlaceToVisitController(IPlaceToVisitService placeToVisitService, 
                                      IDestinationService destinationService, 
                                      UserManager<ApplicationUser> userManager)
        {
            this.placeToVisitService = placeToVisitService;
            this.destinationService = destinationService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var desiredPlaces = await placeToVisitService.GetAllDesiredPlacesAsync(userId);

                return View(desiredPlaces);
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

                if (await placeToVisitService.IsInWishlistAsync(id, userId))
                {
                    TempData["ErrorMessage"] = "This destination is already in your favorites.";
                    return RedirectToAction(nameof(Index), "Destination");
                }

                var viewModel = new AddToWishlistViewModel
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
        public async Task<IActionResult> Add(AddToWishlistViewModel model)
        {
            if (model.DestinationId <= 0)
            {
                TempData["ErrorMessage"] = "Invalid destination.";
                return RedirectToAction(nameof(Index), "Destination");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var success = await placeToVisitService.AddToWishlistAsync(model.DestinationId, userId);

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "This destination is already in your wishlist.";
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

                var viewModel = new DeleteFromWishlistViewModel
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
        public async Task<IActionResult> Delete(DeleteFromWishlistViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await placeToVisitService.RemoveFromWishlistAsync(model.DestinationId, userId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }
    }
}
