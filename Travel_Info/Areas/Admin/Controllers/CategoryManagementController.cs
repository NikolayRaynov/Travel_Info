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
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                bool exists = await categoryService.ExistByIdAsync(id);
                if (!exists)
                {
                    return RedirectToAction(nameof(Index), "CategoryManagement", new { area = "Admin" });
                }

                CategoryViewModel? model = await categoryService.GetCategoryForEditAsync(id);
                if (model == null)
                {
                    return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
                }

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 400 });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                await categoryService.UpdateAsync(model.Id, model, webRootPath);

                return RedirectToAction(nameof(Index), "CategoryManagement", new { area = "Admin" });
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 404 });
            }
            catch (IOException)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "", statusCode = 500 });
            }
        }
    }
}
