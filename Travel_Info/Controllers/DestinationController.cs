using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;
using Travel_Info.Web.ViewModels.Destination;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Controllers
{
    using static Common.ApplicationConstants;

    [Authorize]
    public class DestinationController : Controller
    {
        private readonly IDestinationService destinationService;
        private readonly ICategoryService categoryService;
        private readonly IHtmlSanitizer htmlSanitizer;

        public DestinationController(IDestinationService destinationService, 
                                     ICategoryService categoryService, 
                                     IHtmlSanitizer htmlSanitizer)
        {
            this.destinationService = destinationService;
            this.categoryService = categoryService;
            this.htmlSanitizer = htmlSanitizer;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var destinations = await destinationService.GetAllAsync();
            var viewModels = destinations
            .Select(d => new DestinationIndexViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageUrls = d.ImageUrls.ToList(),
                UserId = d.UserId,
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryService.GetAllAsync();
            var model = new AddDestinationViewModel
            {
                Categories = categories
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        NameBg = c.NameBg
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDestinationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await destinationService.CreateAsync(model, model.Images.ToList(), userId);

                    if (User.IsInRole(AdminRoleName))
                    {
                        return RedirectToAction(nameof(Index), "DestinationManagement", new { area = AdminRoleName });
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (InvalidOperationException)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 500 });
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var destination = await destinationService.GetByIdAsync(id);
                if (destination == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }

                var viewModel = new EditDestinationViewModel
                {
                    Id = destination.Id,
                    Name = destination.Name,
                    Description = destination.Description,
                    ImageUrls = destination.ImageUrls.ToList()
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDestinationViewModel model, List<IFormFile> newImages)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await destinationService.UpdateAsync(model, newImages, userId);
                }
                catch (UnauthorizedAccessException)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 403 });
                }
                catch (InvalidOperationException)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 500 });
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var destination = await destinationService.GetByIdAsync(id);
                if (destination == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }

                var viewModel = new DeleteDestinationViewModel
                {
                    Id = destination.Id,
                    Name = destination.Name,
                    Description = destination.Description,
                    ImageUrls = destination.ImageUrls
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDestinationViewModel model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await destinationService.DeleteDestinationAsync(model.Id, userId);
                if (User.IsInRole(AdminRoleName))
                {
                    return RedirectToAction(nameof(Index), "DestinationManagement", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 403 });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, string imageUrl)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await destinationService.DeleteImageAsync(id, imageUrl, userId);
                return RedirectToAction("Edit", new { id });
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 403 });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 500 });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var destination = await destinationService.GetByIdAsync(id);

                if (destination == null)
                {
                    return RedirectToAction("Error", "Home", new { statusCode = 404 });
                }

                var viewModel = new DestinationDetailsViewModel
                {
                    Id = destination.Id,
                    Name = destination.Name,
                    Description = destination.Description,
                    ImageUrls = destination.ImageUrls,
                    Reviews = destination.Reviews
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 500 });
            }
        }
    }
}