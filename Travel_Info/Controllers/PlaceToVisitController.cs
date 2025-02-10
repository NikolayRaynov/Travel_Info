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
    }
}
