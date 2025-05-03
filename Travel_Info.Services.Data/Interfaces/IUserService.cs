using Travel_Info.Web.ViewModels.Admin.UserManagement;

namespace Travel_Info.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();
		Task<bool> UserExistsByIdAsync(string userId);
		Task<bool> AssignUserToRoleAsync(string userId, string roleName);
		Task<bool> RemoveUserRoleAsync(string userId, string roleName);
		Task DeleteUserAsync(string userId);
	}
}