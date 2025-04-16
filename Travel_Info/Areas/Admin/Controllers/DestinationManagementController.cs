using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public DestinationManagementController(IDestinationService destinationService, ICategoryService categoryService, 
            IHtmlSanitizer htmlSanitizer)
        {
            this.destinationService = destinationService;
            this.categoryService = categoryService;
            this.htmlSanitizer = htmlSanitizer;
        }

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
                    UserId = d.UserId
                }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
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
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var destination = await destinationService.GetByIdAsync(id);
            if (destination == null)
            {
                return NotFound();
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

        [HttpPost]
        public async Task<IActionResult> Edit(EditDestinationViewModel model, List<IFormFile> newImages)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await destinationService.UpdateAsync(model, newImages, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
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

            var viewModel = new DeleteDestinationViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                ImageUrls = destination.ImageUrls
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteDestinationViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await destinationService.DeleteDestinationAsync(model.Id, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
