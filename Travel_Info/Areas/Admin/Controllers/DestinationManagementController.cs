using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;
using Travel_Info.Web.ViewModels.Destination;

namespace Travel_Info.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class DestinationManagementController : Controller
    {
        private readonly IDestinationService destinationService;
        private readonly ICategoryService categoryService;
        private readonly IHtmlSanitizer htmlSanitizer;

        public DestinationManagementController(IDestinationService destinationService, 
                                               ICategoryService categoryService, 
                                               IHtmlSanitizer htmlSanitizer)
        {
            this.destinationService = destinationService;
            this.categoryService = categoryService;
            this.htmlSanitizer = htmlSanitizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int pageNumber = DefaultPageNumber)
        {
            try
            {
                var allCategories = await categoryService.GetAllAsync();

                ViewBag.Categories = new SelectList(allCategories, "Id", "NameBg", categoryId);
                ViewBag.SelectedCategoryId = categoryId;

                var pageViewModel = await destinationService.GetAllAsync(pageNumber, DefaultPageSize, categoryId);
                return View(pageViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var categories = await categoryService.GetAllAsync();
                var models = new AddDestinationViewModel
                {
                    Categories = categories
                        .Select(c => new CategoryViewModel
                        {
                            Id = c.Id,
                            NameBg = c.NameBg
                        }).ToList()
                };

                return View(models);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
            
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
                    return RedirectToAction(nameof(Index));
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
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
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
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
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
                    return RedirectToAction(nameof(Index));
                }
                catch (UnauthorizedAccessException)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
                }
            }

            return View(model);
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

                var viewModel = new DeleteDestinationViewModel
                {
                    Id = destination.Id,
                    Name = destination.Name,
                    Description = destination.Description,
                    ImageUrls = destination.ImageUrls
                };

                return View(viewModel);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDestinationViewModel model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await destinationService.DeleteDestinationAsync(model.Id, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 403 });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }
    }
}
