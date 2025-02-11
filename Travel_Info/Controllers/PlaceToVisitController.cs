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

            var viewModel = desiredPlaces.Select(destination => new PlaceToVisitIndexViewModel
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

            var viewModel = new AddToWishlistViewModel
            {
                DestinationId = destination.Id,
                DestinationName = destination.Name,
                DestinationDescription = destination.Description,
                DestinationImageUrl = destination.Images.FirstOrDefault()?.Url ?? "/images/NoPhoto.jpg"
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToWishlistViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await placeToVisitService.AddToWishlistAsync(model.DestinationId, userId))
            {
                TempData["SuccessMessage"] = "The destination was successfully added to your wishlist!";
                return RedirectToAction("Index", "PlaceToVisit");
            }
            else
            {
                TempData["ErrorMessage"] = "This destination is already in your wishlist.";
                return RedirectToAction("Index", "Destination");
            }
        }
    }
}
