using Microsoft.AspNetCore.Mvc;
using Travel_Info.Services.Data;
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
            var model = reviews.Select(r => new ReviewViewModel
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                User = r.User.UserName
            }).ToList();

            return View(model);
        }
    }
}
