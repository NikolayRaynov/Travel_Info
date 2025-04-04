using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travel_Info.Data.Models;

namespace Travel_Info.Web.Infrastructure.Configuration
{
	using static Travel_Info.Common.ApplicationConstants;
	public static class RolesSeeder
	{
		public static void SeedRoles(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			string[] roles = { "Admin", "Manager", "User" };

			foreach (var role in roles)
			{
				var roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
				if (!roleExists)
				{
					var result = roleManager.CreateAsync(new IdentityRole { Name = role }).GetAwaiter().GetResult();
					if (!result.Succeeded)
					{
						throw new Exception($"Failed to create role: {role}");
					}
				}
			}
		}

		public static void AssignAdminRole(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();

			string adminEmail = configuration["Administrator:Username"]!;
			string adminPassword = configuration["Administrator:Password"]!;

			var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
			if (adminUser == null)
			{
				adminUser = new ApplicationUser
				{
					UserName = adminEmail,
					Email = adminEmail
				};
				var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
				if (!createUserResult.Succeeded)
				{
					throw new Exception($"Failed to create admin user: {adminEmail}");
				}
			}

			var isInRole = userManager.IsInRoleAsync(adminUser, AdminRoleName).GetAwaiter().GetResult();
			if (!isInRole)
			{
				var addRoleResult = userManager.AddToRoleAsync(adminUser, AdminRoleName).GetAwaiter().GetResult();
				if (!addRoleResult.Succeeded)
				{
					throw new Exception($"Failed to assign admin role to user: {adminEmail}");
				}
			}
		}
	}
}
