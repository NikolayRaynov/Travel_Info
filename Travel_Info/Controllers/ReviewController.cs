using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Controllers
{
    using static Travel_Info.Common.ApplicationConstants;

    [Authorize]
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
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                try
                {
                    await reviewService.AddReviewAsync(model, userId);
                }
                catch (InvalidOperationException)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
                }
            }

            return RedirectToAction("Details", "Destination", new { id = model.DestinationId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var review = await reviewService.GetReviewByIdAsync(id);
                if (review == null)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool isOwner = review.User == currentUserId;
                bool isAdmin = User.IsInRole(AdminRoleName);

                if (!isOwner && !isAdmin)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
                }

                var model = new EditReviewViewModel
                {
                    Id = review.Id,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    DestinationId = review.DestinationId
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                try
                {
                    await reviewService.UpdateReviewAsync(model, userId);
                }
                catch (UnauthorizedAccessException)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
                }
                catch (InvalidOperationException)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
                }
            }

            return RedirectToAction("Details", "Destination", new { id = model.DestinationId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var review = await reviewService.GetReviewByIdAsync(id);
                if (review == null)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                bool isOwner = review.User == currentUserId;
                bool isAdmin = User.IsInRole(AdminRoleName);

                if (!isOwner && !isAdmin)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
                }

                var model = new DeleteReviewViewModel
                {
                    Id = review.Id,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    DestinationId = review.DestinationId
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteReviewViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await reviewService.DeleteReviewAsync(model, userId);
                return RedirectToAction("Details", "Destination", new { id = model.DestinationId });
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }
    }
}
