using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MockQueryable;
using Moq;
using System.Linq.Expressions;
using Travel_Info.Common;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;

namespace Travel_Info.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManager;
        private Mock<RoleManager<IdentityRole>> roleManager;
        private Mock<IRepository> repository;
        private Mock<ICategoryService> categoryService;
        private UserService userService;
        private string testFilesRootPathForUserService;

        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var pwdValidators = new List<IPasswordValidator<TUser>>();
            return new Mock<UserManager<TUser>>(store, options.Object, new Mock<IPasswordHasher<TUser>>().Object,
                userValidators, pwdValidators, new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object, new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);
        }

        public static Mock<RoleManager<TRole>> MockRoleManager<TRole>() where TRole : class
        {
            var store = new Mock<IRoleStore<TRole>>().Object;
            var roleValidators = new List<IRoleValidator<TRole>>();
            return new Mock<RoleManager<TRole>>(store, roleValidators, new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object, new Mock<ILogger<RoleManager<TRole>>>().Object);
        }


        [SetUp]
        public void Setup()
        {
            this.userManager = MockUserManager<ApplicationUser>();
            this.roleManager = MockRoleManager<IdentityRole>();
            this.repository = new Mock<IRepository>();
            this.categoryService = new Mock<ICategoryService>();

            this.userService = new UserService(
                this.userManager.Object,
                this.roleManager.Object,
                this.repository.Object,
                this.categoryService.Object
            );
            var uniqueSubFolder = "UserServiceTestFiles_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            this.testFilesRootPathForUserService = 
                Path.Combine(Directory.GetCurrentDirectory(), ApplicationConstants.UrlPath, uniqueSubFolder);
        }

        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(this.testFilesRootPathForUserService))
            {
                try
                {
                    Directory.Delete(this.testFilesRootPathForUserService, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting test directory '{this.testFilesRootPathForUserService}': {ex.Message}");
                }
            }
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            var usersData = new List<ApplicationUser>();
            var mockUsersQueryable = usersData.AsQueryable().BuildMock();
            this.userManager.Setup(um => um.Users).Returns(mockUsersQueryable);

            var result = await this.userService.GetAllUsersAsync();

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnMappedUsers_WhenUsersExist()
        {
            var user1Id = Guid.NewGuid().ToString();
            var user2Id = Guid.NewGuid().ToString();
            var usersData = new List<ApplicationUser>
            {
                new ApplicationUser 
                { 
                    Id = user1Id, 
                    Email = "test1@example.com" 
                },
                new ApplicationUser 
                { 
                    Id = user2Id, 
                    Email = "test2@example.com" 
                }
            };
            var mockUsersQueryable = usersData.AsQueryable().BuildMock();

            this.userManager.Setup(um => um.Users).Returns(mockUsersQueryable);

            this.userManager.Setup(um => um.GetRolesAsync(It.Is<ApplicationUser>(u => u.Id == user1Id)))
                .ReturnsAsync(new List<string> { "User", "Editor" });
            this.userManager.Setup(um => um.GetRolesAsync(It.Is<ApplicationUser>(u => u.Id == user2Id)))
                .ReturnsAsync(new List<string> { "User" });

            var result = (await this.userService.GetAllUsersAsync()).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var user1ViewModel = result.FirstOrDefault(u => u.Id == user1Id);
            Assert.IsNotNull(user1ViewModel);
            Assert.AreEqual("test1@example.com", user1ViewModel.Email);
            CollectionAssert.AreEquivalent(new[] { "User", "Editor" }, user1ViewModel.Roles);

            var user2ViewModel = result.FirstOrDefault(u => u.Id == user2Id);
            Assert.IsNotNull(user2ViewModel);
            Assert.AreEqual("test2@example.com", user2ViewModel.Email);
            CollectionAssert.AreEquivalent(new[] { "User" }, user2ViewModel.Roles);
        }

        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnTrue_WhenUserExists()
        {
            string userId = "existing-user-id";
            this.userManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(new ApplicationUser { Id = userId });

            bool exists = await this.userService.UserExistsByIdAsync(userId);

            Assert.IsTrue(exists);
        }

        [Test]
        public async Task UserExistsByIdAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            string userId = "non-existing-user-id";
            this.userManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);

            bool exists = await this.userService.UserExistsByIdAsync(userId);

            Assert.IsFalse(exists);
        }

        [Test]
        public async Task AssignUserToRoleAsync_SuccessfulAssignment_ReturnsTrue()
        {
            string userId = "user-id";
            string roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            this.roleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);
            this.userManager.Setup(um => um.IsInRoleAsync(user, roleName)).ReturnsAsync(false);
            this.userManager.Setup(um => um.AddToRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            bool result = await this.userService.AssignUserToRoleAsync(userId, roleName);

            Assert.IsTrue(result);
            this.userManager.Verify(um => um.AddToRoleAsync(user, roleName), Times.Once);
        }

        [Test]
        public async Task AssignUserToRoleAsync_UserAlreadyInRole_ReturnsTrueAndDoesNotAddAgain()
        {
            string userId = "user-id";
            string roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            this.roleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);
            this.userManager.Setup(um => um.IsInRoleAsync(user, roleName)).ReturnsAsync(true);

            bool result = await this.userService.AssignUserToRoleAsync(userId, roleName);

            Assert.IsTrue(result);
            this.userManager.Verify(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task AssignUserToRoleAsync_UserNotFound_ReturnsFalse()
        {
            string userId = "non-existent-user";
            string roleName = "Admin";

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);
            this.roleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);

            bool result = await this.userService.AssignUserToRoleAsync(userId, roleName);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task AssignUserToRoleAsync_RoleNotFound_ReturnsFalse()
        {
            string userId = "user-id";
            string roleName = "non-existent-role";
            var user = new ApplicationUser { Id = userId };

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            this.roleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(false);

            bool result = await this.userService.AssignUserToRoleAsync(userId, roleName);

            Assert.IsFalse(result);
        }


        [Test]
        public async Task AssignUserToRoleAsync_AddToRoleFails_ReturnsFalse()
        {
            string userId = "user-id";
            string roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            this.roleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);
            this.userManager.Setup(um => um.IsInRoleAsync(user, roleName)).ReturnsAsync(false);
            this.userManager.Setup(um => um.AddToRoleAsync(user, roleName))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Simulated failure" }));

            bool result = await this.userService.AssignUserToRoleAsync(userId, roleName);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task RemoveUserRoleAsync_SuccessfulRemoval_ReturnsTrue()
        {
            string userId = "user-id";
            string roleName = "Admin";
            var user = new ApplicationUser { Id = userId };

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            this.roleManager.Setup(rm => rm.RoleExistsAsync(roleName)).ReturnsAsync(true);
            this.userManager.Setup(um => um.IsInRoleAsync(user, roleName)).ReturnsAsync(true);
            this.userManager.Setup(um => um.RemoveFromRoleAsync(user, roleName)).ReturnsAsync(IdentityResult.Success);

            bool result = await this.userService.RemoveUserRoleAsync(userId, roleName);

            Assert.IsTrue(result);
            this.userManager.Verify(um => um.RemoveFromRoleAsync(user, roleName), Times.Once);
        }

        [Test]
        public void DeleteUserAsync_UserIdIsNull_ThrowsArgumentException()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(() => this.userService
                .DeleteUserAsync(null));
            StringAssert.Contains("User ID cannot be null or empty.", ex.Message);
        }

        [Test]
        public void DeleteUserAsync_UserNotFound_ThrowsInvalidOperationException()
        {
            string userId = "non-existent-user";
            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => this.userService.DeleteUserAsync(userId));
            Assert.AreEqual("The user was not found.", ex.Message);
        }

        [Test]
        public async Task DeleteUserAsync_UserExistsWithNoData_DeletesUserIdentitySuccessfully()
        {
            string userId = "user-to-delete";
            var user = new ApplicationUser { Id = userId };

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            var emptyDestinationsList = new List<Destination>().AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<Destination>()).Returns(emptyDestinationsList);

            this.userManager.Setup(um => um.GetLoginsAsync(user)).ReturnsAsync(new List<UserLoginInfo>());
            this.userManager.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.userService.DeleteUserAsync(userId);

            this.userManager.Verify(um => um.DeleteAsync(user), Times.Once);
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once);
            this.repository.Verify(r => r.DeleteRange<Review>(It.IsAny<Expression<Func<Review, bool>>>()), Times.Never);
            this.repository.Verify(r => r.DeleteRange<Image>(It.IsAny<Expression<Func<Image, bool>>>()), Times.Never);
            this.repository.Verify(r => r.Delete(It.IsAny<Destination>()), Times.Never);
        }

        [Test]
        public async Task DeleteUserAsync_UserExistsWithData_DeletesDataAndUserIdentity()
        {
            string userId = "user-with-data";
            var user = new ApplicationUser { Id = userId };
            int categoryId = 1;
            string categoryNameEn = "Beaches";
            string imageName = "beach.jpg";

            string categoryFolderForFile = categoryNameEn;
            string fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), ApplicationConstants.UrlPath, categoryFolderForFile);
            string filePath = Path.Combine(fileDirectory, imageName);

            Directory.CreateDirectory(fileDirectory);
            File.WriteAllText(filePath, "dummy image data");

            var image = new Image { Url = $"/{ApplicationConstants.UrlPath}/{categoryFolderForFile}/{imageName}", DestinationId = 10 };
            var destination = new Destination
            {
                Id = 10,
                UserId = userId,
                CategoryId = categoryId,
                Images = new List<Image> { image },
                Reviews = new List<Review> 
                { 
                    new Review 
                    { 
                        Id = 1,
                        DestinationId = 10 
                    } 
                }
            };

            var destinationsData = new List<Destination> { destination };
            this.repository.Setup(r => r.All<Destination>())
                           .Returns(destinationsData.AsQueryable().BuildMock());

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            this.categoryService.Setup(cs => cs.GetByIdAsync(destination.CategoryId))
                .ReturnsAsync(new CategoryViewModel { Id = categoryId, NameEn = categoryNameEn });

            this.userManager.Setup(um => um.GetLoginsAsync(user)).ReturnsAsync(new List<UserLoginInfo>());
            this.userManager.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.userService.DeleteUserAsync(userId);

            this.repository.Verify(r => r.DeleteRange<Review>(It.IsAny<Expression<Func<Review, bool>>>()), Times.Once);
            this.repository.Verify(r => r.DeleteRange<Image>(It.IsAny<Expression<Func<Image, bool>>>()), Times.Once);
            this.repository.Verify(r => r.Delete(destination), Times.Once);
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once);
            this.userManager.Verify(um => um.DeleteAsync(user), Times.Once);

            Assert.IsFalse(File.Exists(filePath), $"File '{filePath}' should have been deleted by the service.");

            if (Directory.Exists(fileDirectory)) Directory.Delete(fileDirectory, true);
        }

        [Test]
        public void DeleteUserAsync_UserManagerDeleteFails_ThrowsInvalidOperationExceptionWithMessage()
        {
            string userId = "user-delete-fails";
            var user = new ApplicationUser { Id = userId };
            var identityErrors = new[] { new IdentityError { Code = "Err1", Description = "First error." } };

            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            this.repository.Setup(r => r.All<Destination>())
                           .Returns(new List<Destination>().AsQueryable().BuildMock());
            this.userManager.Setup(um => um.GetLoginsAsync(user)).ReturnsAsync(new List<UserLoginInfo>());
            this.userManager.Setup(um => um.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Failed(identityErrors));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => this.userService.DeleteUserAsync(userId));
            StringAssert.Contains("Failed to delete user Identity: First error.", ex.Message);
        }
    }
}