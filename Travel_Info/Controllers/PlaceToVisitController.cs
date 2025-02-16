using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel_Info.Data.Models;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.PlaceToVisit;

namespace Travel_Info.Controllers
{
    public class PlaceToVisitController : Controller
    {
        private readonly IPlaceToVisitService placeToVisitService;
        private readonly IDestinationService destinationService;
        private readonly UserManager<ApplicationUser> userManager;

        public PlaceToVisitController(IPlaceToVisitService placeToVisitService, IDestinationService destinationService, UserManager<ApplicationUser> userManager)
        {
            this.placeToVisitService = placeToVisitService;
            this.destinationService = destinationService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var desiredPlaces = await placeToVisitService.GetAllDesiredPlacesAsync(userId);

            return View(desiredPlaces);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var destination = await destinationService.GetByIdAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await placeToVisitService.IsInWishlistAsync(id, userId))
            {
                TempData["ErrorMessage"] = "This destination is already in your favorites.";
                return RedirectToAction("Index", "Destination");
            }

            var viewModel = new AddToWishlistViewModel
            {
                DestinationId = destination.Id,
                DestinationName = destination.Name,
                DestinationDescription = destination.Description,
                DestinationImageUrl = destination.ImageUrl ?? "/images/NoPhoto.jpg"
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToWishlistViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await placeToVisitService.AddToWishlistAsync(model.DestinationId, userId))
            {
                return RedirectToAction("Index", "PlaceToVisit");
            }
            else
            {
                TempData["ErrorMessage"] = "This destination is already in your wishlist.";
                return RedirectToAction("Index", "Destination");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var destination = await destinationService.GetByIdAsync(id);
            if (destination == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteFromWishlistViewModel
            {
                DestinationId = destination.Id,
                DestinationName = destination.Name,
                DestinationDescription = destination.Description,
                DestinationImageUrl = destination.ImageUrl ?? "/images/NoPhoto.jpg"
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteFromWishlistViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await placeToVisitService.RemoveFromWishlistAsync(model.DestinationId, userId);
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
