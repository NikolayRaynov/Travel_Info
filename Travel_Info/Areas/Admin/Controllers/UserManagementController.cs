using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Travel_Info.Data.Models;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Admin.UserManagement;

namespace Travel_Info.Areas.Admin.Controllers
{
	using static Travel_Info.Common.ApplicationConstants;

	[Area(AdminRoleName)]
	[Authorize(Roles = AdminRoleName)]
	public class UserManagementController : Controller
	{
		private readonly IUserService userService;
		private readonly UserManager<ApplicationUser> userManager;

		public UserManagementController(IUserService userService, UserManager<ApplicationUser> userManager)
		{
			this.userService = userService;
			this.userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<AllUsersViewModel> allUsers = await this.userService
				.GetAllUsersAsync();

			return this.View(allUsers);
		}

		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string role)
		{
			bool assignResult = await this.userService
				.AssignUserToRoleAsync(userId, role);

			if (!assignResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> RemoveRole(string userId, string role)
		{
			bool assignResult = await this.userService
				.RemoveUserRoleAsync(userId, role);

			if (!assignResult)
			{
				return this.RedirectToAction(nameof(Index));
			}

			return this.RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string userId)
		{
            var result = await this.userService.DeleteUserAsync(userId);

            if (!result)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return this.RedirectToAction(nameof(Index));
        }
	}
}
