using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int destinationId)
        {
            var reviews = await reviewService.GetAllReviewsByDestinationIdAsync(destinationId);
            return View(reviews);
        }


        [HttpGet]
        public IActionResult Add(int destinationId)
        {
            var model = new AddReviewViewModel
            {
                DestinationId = destinationId,
                Rating = 0,
                Comment = string.Empty
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await reviewService.AddReviewAsync(model, userId);
            return RedirectToAction("Details", "Destination", new { id = model.DestinationId });
        }

    }
}
