﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Admin.UserManagement;

namespace Travel_Info.Services.Data
{
	using static Travel_Info.Common.ApplicationConstants;
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IRepository repository;
		private readonly ICategoryService categoryService;

		public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IRepository repository, ICategoryService categoryService)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.repository = repository;
			this.categoryService = categoryService;
		}

		public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
		{
			IEnumerable<ApplicationUser> allUsers = await this.userManager.Users
				.ToArrayAsync();
			ICollection<AllUsersViewModel> allUsersViewModel = new List<AllUsersViewModel>();

			foreach (ApplicationUser user in allUsers)
			{
				IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);

				allUsersViewModel.Add(new AllUsersViewModel()
				{
					Id = user.Id.ToString(),
					Email = user.Email,
					Roles = roles
				});
			}

			return allUsersViewModel;
		}

		public async Task<bool> UserExistsByIdAsync(string userId)
		{
			ApplicationUser? user = await this.userManager
				.FindByIdAsync(userId);

			return user != null;
		}

		public async Task<bool> AssignUserToRoleAsync(string userId, string roleName)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return false;
			}

			ApplicationUser? user = await userManager
				.FindByIdAsync(userId);
			bool roleExists = await this.roleManager.RoleExistsAsync(roleName);

			if (user == null || !roleExists)
			{
				return false;
			}

			bool alreadyInRole = await this.userManager.IsInRoleAsync(user, roleName);
			if (!alreadyInRole)
			{
				IdentityResult? result = await this.userManager
					.AddToRoleAsync(user, roleName);

				if (!result.Succeeded)
				{
					return false;
				}
			}

			return true;
		}

		public async Task<bool> RemoveUserRoleAsync(string userId, string roleName)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return false;
			}

			ApplicationUser? user = await userManager
				.FindByIdAsync(userId);
			bool roleExists = await this.roleManager.RoleExistsAsync(roleName);

			if (user == null || !roleExists)
			{
				return false;
			}

			bool alreadyInRole = await this.userManager.IsInRoleAsync(user, roleName);
			if (alreadyInRole)
			{
				IdentityResult? result = await this.userManager
					.RemoveFromRoleAsync(user, roleName);

				if (!result.Succeeded)
				{
					return false;
				}
			}

			return true;
		}

		public async Task<bool> DeleteUserAsync(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return false;
			}

			ApplicationUser? user = await userManager.FindByIdAsync(userId);

			if (user == null)
			{
				return false;
			}

			var userDestinations = await this.repository
				.All<Destination>()
				.Include(d => d.Images)
				.Include(d => d.Reviews)
				.Where(d => d.UserId == userId)
				.ToListAsync();

			foreach (var destination in userDestinations)
			{
				repository.DeleteRange<Review>(r => r.DestinationId == destination.Id);

				var category = await categoryService.GetByIdAsync(destination.CategoryId);
				var categoryFolder = category.NameEn;

				foreach (var image in destination.Images)
				{
					var fileName = Path.GetFileName(image.Url);
					var folderPath = Path.Combine(Directory.GetCurrentDirectory(), UrlPath, categoryFolder).ToLower();
					var filePath = Path.Combine(folderPath, fileName).ToLower();

					if (File.Exists(filePath))
					{
						File.Delete(filePath);
					}
				}

				repository.Delete(destination);
			}

			await repository.SaveChangesAsync();

			var externalLogins = await this.userManager.GetLoginsAsync(user);
			foreach (var externalLogin in externalLogins)
			{
				await this.userManager.RemoveLoginAsync(user, externalLogin.LoginProvider, externalLogin.ProviderKey);
			}

			var result = await this.userManager.DeleteAsync(user);

			return result.Succeeded;
		}
	}
}
	