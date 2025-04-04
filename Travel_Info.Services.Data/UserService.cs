using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travel_Info.Data.Models;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Admin.UserManagement;

namespace Travel_Info.Services.Data
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
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

			ApplicationUser? user = await userManager
				.FindByIdAsync(userId);

			if (user == null)
			{
				return false;
			}

			IdentityResult? result = await this.userManager
				.DeleteAsync(user);
			if (!result.Succeeded)
			{
				return false;
			}

			return true;
		}
	}
}