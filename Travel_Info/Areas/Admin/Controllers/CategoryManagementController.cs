using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;

namespace Travel_Info.Areas.Admin.Controllers
{
    using static Common.ApplicationConstants;

    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class CategoryManagementController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryManagementController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            this.categoryService = categoryService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllAsync();
            var viewModels = categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    NameBg = c.NameBg,
                    NameEn = c.NameEn
                }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddCategoryViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                await categoryService.AddAsync(model, webRootPath);

                return RedirectToAction(nameof(Index), "CategoryManagement", new { area = "Admin" });
            }
            catch (IOException ioEx)
            {
                ModelState.AddModelError(string.Empty, $"Error working with the file system: {ioEx.Message}");
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while adding a category.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool exists = await categoryService.ExistByIdAsync(id);
            if (!exists)
            {
                return RedirectToAction(nameof(Index), "CategoryManagement", new { area = "Admin" });
            }

            CategoryViewModel model = await categoryService.GetCategoryForEditAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool exists = await categoryService.ExistByIdAsync(id);
            if (!exists)
            {
                TempData["ErrorMessage"] = "The category you are trying to edit no longer exists.";
                return RedirectToAction(nameof(Index), "CategoryManagement", new { area = "Admin" });
            }

            try
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                await categoryService.UpdateAsync(model.Id, model, webRootPath);

                return RedirectToAction(nameof(Index), "CategoryManagement", new { area = "Admin" });
            }
            catch (IOException ioEx)
            {
                ModelState.AddModelError(string.Empty, $"Error working with the file system: {ioEx.Message}");
                return View(model);
            }
            catch (InvalidOperationException invOpEx)
            {
                ModelState.AddModelError(string.Empty, invOpEx.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while editing a category.");
                return View(model);
            }
        }
    }
}
