﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;
using Travel_Info.Web.ViewModels.Destination;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Controllers
{
    [Authorize]
    public class DestinationController : Controller
    {
        private readonly IDestinationService destinationService;
        private readonly ICategoryService categoryService;

        public DestinationController(IDestinationService destinationService, ICategoryService categoryService)
        {
            this.destinationService = destinationService;
            this.categoryService = categoryService;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddDestinationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryService.GetByIdAsync(model.CategoryId);
                var categoryFolder = category.NameEn;

                var imageUrls = new List<string>();

                foreach (var image in model.Images)
                {
                    if (image.Length > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", categoryFolder);

                        Directory.CreateDirectory(folderPath);
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        imageUrls.Add($"/images/{categoryFolder}/{fileName}");
                    }
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await destinationService.CreateAsync(model, imageUrls, userId);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
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
                ImageUrls = destination.ImageUrls.ToList(),
                Reviews = destination.Reviews
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    User = r.User
                }).ToList()
            };

            return View(viewModel);
        }
    }
}