using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Destination;

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
    }
}
