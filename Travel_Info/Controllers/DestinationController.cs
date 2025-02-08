using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Destination;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Controllers
{
    [Authorize]
    public class DestinationController : Controller
    {
        private readonly IDestinationService destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            this.destinationService = destinationService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var destinations = await destinationService.GetAllAsync();
            var viewModels = destinations.Select(d => new DestinationIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageUrl = d.Images.FirstOrDefault()?.Url ?? string.Empty
            }).ToList();

            return View(viewModels);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var destination = await destinationService.GetByIdAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            var viewModel = new DestinationDetailsViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                ImageUrl = destination.Images.FirstOrDefault()?.Url ?? "/images/noPhoto.jpg",
                Reviews = destination.Reviews.Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    User = r.User.UserName
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
