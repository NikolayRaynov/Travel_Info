using AngleSharp;
using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MockQueryable;
using Moq;
using System.Reflection;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;
using Travel_Info.Web.ViewModels.Destination;
using static Travel_Info.Common.ApplicationConstants;

namespace Travel_Info.Services.Tests
{
    [TestFixture]
    public class DestinationServiceTests
    {
        private Mock<IRepository> mockRepository;
        private Mock<ICategoryService> mockCategoryService;
        private Mock<IHtmlSanitizer> mockHtmlSanitizer;
        private Mock<UserManager<ApplicationUser>> mockUserManager;
        private Mock<IFileService> mockFileService;
        private DestinationService destinationService;

        private string baseTestPath;
        private string wwwRootPath;
        private string originalCurrentDirectory;

        [SetUp]
        public void Setup()
        {
            this.mockRepository = new Mock<IRepository>();
            this.mockCategoryService = new Mock<ICategoryService>();
            this.mockHtmlSanitizer = new Mock<IHtmlSanitizer>();
            this.mockUserManager = MockUserManager<ApplicationUser>();
            this.mockFileService = new Mock<IFileService>();

            this.destinationService = new DestinationService(
                this.mockRepository.Object,
                this.mockCategoryService.Object,
                this.mockHtmlSanitizer.Object,
                this.mockUserManager.Object,
                this.mockFileService.Object
            );

            this.originalCurrentDirectory = Directory.GetCurrentDirectory();
            this.baseTestPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "TestRunData",
                Guid.NewGuid().ToString()
            );

            Directory.CreateDirectory(this.baseTestPath);
            Directory.SetCurrentDirectory(this.baseTestPath);

            this.wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), DefaultFolder);
            Directory.CreateDirectory(this.wwwRootPath);

            this.mockHtmlSanitizer.Setup(s => s.Sanitize(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IMarkupFormatter?>()))
                                   .Returns((string input, string baseUrl, IMarkupFormatter? outputFormatter) => input ?? string.Empty);

            this.mockFileService.Setup(fs => fs.SanitizeFolderName(It.IsAny<string>()))
                                 .Returns((string input) => input.Replace(" ", "_"));
        }

        [TearDown]
        public void Teardown()
        {
            Directory.SetCurrentDirectory(this.originalCurrentDirectory);
            if (Directory.Exists(this.baseTestPath))
            {
                try
                {
                    Directory.Delete(this.baseTestPath, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting test directory {this.baseTestPath}: {ex.Message}");
                }
            }
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var pwdValidators = new List<IPasswordValidator<TUser>>();
            var userManager = new Mock<UserManager<TUser>>(store.Object, options.Object,
                new Mock<IPasswordHasher<TUser>>().Object,
                userValidators, pwdValidators,
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            return userManager;
        }

        public static Mock<IFormFile> CreateMockFormFile(string fileName, long length,
            string contentType = "image/jpeg", byte[] contentBytes = null)
        {
            var mockFile = new Mock<IFormFile>();
            var fileContent = contentBytes ?? new byte[length];
            var ms = new MemoryStream(fileContent);

            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.Length).Returns(length);
            mockFile.Setup(f => f.ContentType).Returns(contentType);
            mockFile.Setup(f => f.OpenReadStream()).Returns(ms);
            mockFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Callback<Stream, CancellationToken>((stream, token) =>
                {
                    ms.Position = 0;
                    ms.CopyTo(stream);
                })
                .Returns(Task.CompletedTask);

            return mockFile;
        }

        [Test]
        public async Task CreateAsync_ValidInput_ShouldCreateDestinationAndSaveImages()
        {
            var userId = "test-user-id";
            var categoryId = 1;
            var categoryNameEn = "Beaches";
            var sanitizedCategoryFolder = "Beaches";

            var model = new AddDestinationViewModel
            {
                Name = "Sunny Beach",
                Description = "A beautiful beach destination.",
                CategoryId = categoryId
            };
            var imageFile1 = CreateMockFormFile("beach1.jpg", 1024).Object;
            var images = new List<IFormFile> { imageFile1 };

            this.mockCategoryService.Setup(cs => cs.GetByIdAsync(categoryId))
                                     .ReturnsAsync(new CategoryViewModel { Id = categoryId, NameEn = categoryNameEn });
            this.mockFileService.Setup(fs => fs.SanitizeFolderName(categoryNameEn)).Returns(sanitizedCategoryFolder);

            Destination capturedDestination = null;
            this.mockRepository.Setup(r => r.AddAsync(It.IsAny<Destination>()))
                                     .Callback<Destination>(d => capturedDestination = d)
                                     .Returns(Task.CompletedTask);
            this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.destinationService.CreateAsync(model, images, userId);

            this.mockHtmlSanitizer.Verify(s => s.Sanitize(model.Name, It.IsAny<string>(), It.IsAny<IMarkupFormatter?>()), Times.Once);
            this.mockHtmlSanitizer.Verify(s => s.Sanitize(model.Description, It.IsAny<string>(), It.IsAny<IMarkupFormatter?>()), Times.Once);
            this.mockCategoryService.Verify(cs => cs.GetByIdAsync(categoryId), Times.Once);
            this.mockFileService.Verify(fs => fs.SanitizeFolderName(categoryNameEn), Times.Once);

            Assert.IsNotNull(capturedDestination);
            Assert.AreEqual(model.Name, capturedDestination.Name);
            Assert.AreEqual(model.Description, capturedDestination.Description);
            Assert.AreEqual(categoryId, capturedDestination.CategoryId);
            Assert.AreEqual(userId, capturedDestination.UserId);
            Assert.AreEqual(1, capturedDestination.Images.Count);
            StringAssert.EndsWith(".jpg", capturedDestination.Images.First().Url);
            StringAssert.Contains($"/{sanitizedCategoryFolder}/", capturedDestination.Images.First().Url);

            this.mockRepository.Verify(r => r.AddAsync(capturedDestination), Times.Once);
            this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

            var expectedImageDir = Path.Combine(Directory.GetCurrentDirectory(), UrlPath, sanitizedCategoryFolder);

            Assert.IsTrue(Directory.Exists(expectedImageDir), $"Expected image directory should exist at: {expectedImageDir}");
            Assert.AreEqual(1, Directory.GetFiles(expectedImageDir).Length, "Expected 1 image file in the directory.");

            var fileName = Path.GetFileName(capturedDestination.Images.First().Url);
            var expectedFilePath = Path.Combine(expectedImageDir, fileName);

            Assert.IsTrue(File.Exists(expectedFilePath), $"The created image file should exist at: {expectedFilePath}");
        }

        [Test]
        public void CreateAsync_ImageTooLarge_ShouldThrowInvalidOperationException()
        {
            var model = new AddDestinationViewModel { Name = "Test", CategoryId = 1 };
            var largeImage = CreateMockFormFile("large.jpg", 6 * 1024 * 1024).Object;
            var images = new List<IFormFile> { largeImage };
            var userId = "user1";

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                             destinationService.CreateAsync(model, images, userId));
            Assert.AreEqual("Please upload image up to 5MB.", ex.Message);
        }

        [Test]
        public void CreateAsync_CategoryNotFoundInSaveImages_ShouldThrowInvalidOperationException()
        {
            var model = new AddDestinationViewModel { Name = "Test", CategoryId = 1 };
            var imageFile = CreateMockFormFile("test.jpg", 1024).Object;
            var images = new List<IFormFile> { imageFile };
            var userId = "user1";

            this.mockCategoryService
                .Setup(cs => cs.GetByIdAsync(model.CategoryId))
                .ReturnsAsync((CategoryViewModel)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                             destinationService.CreateAsync(model, images, userId));
            Assert.AreEqual("The category is not found.", ex.Message);
        }

        [Test]
        public async Task GetAllAsync_NoFilter_ReturnsPaginatedDestinations()
        {
            var pageNumber = 1;
            var pageSize = 2;
            var destinationsData = new List<Destination>
            {
                new Destination
                {
                    Id = 1,
                    Name = "Dest1",
                    Description = "Desc1",
                    CategoryId = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "/img/1.jpg"
                        }
                    }
                },
                new Destination
                {
                    Id = 2,
                    Name = "Dest2",
                    Description = "Desc2",
                    CategoryId = 2,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "/img/2.jpg"
                        }
                    }
                },
                new Destination
                {
                    Id = 3,
                    Name = "Dest3",
                    Description = "Desc3",
                    CategoryId = 1,
                    Images = new List<Image>
                    {
                        new Image
                        {
                            Url = "/img/3.jpg"
                        }
                    }
                },
            }
            .AsQueryable()
            .BuildMock();

            this.mockRepository.Setup(r => r.All<Destination>()).Returns(destinationsData);

            var result = await destinationService.GetAllAsync(pageNumber, pageSize);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.TotalCount);
            Assert.AreEqual(pageNumber, result.PageNumber);
            Assert.AreEqual(2, result.Destinations.Count());
            Assert.AreEqual("Dest3", result.Destinations.First().Name);
            Assert.AreEqual("Dest2", result.Destinations.Skip(1).First().Name);
            Assert.AreEqual(2, result.TotalPages);
        }

        [Test]
        public async Task GetAllAsync_WithCategoryFilter_ReturnsFilteredPaginatedDestinations()
        {
            var pageNumber = 1;
            var pageSize = 10;
            var categoryIdToFilter = 1;
            var destinationsData = new List<Destination>
            {
                new Destination
                {
                    Id = 1,
                    Name = "Dest1",
                    Description = "Desc1",
                    CategoryId = 1,
                    Images = new List<Image>{}
                },
                new Destination
                {
                    Id = 2,
                    Name = "Dest2",
                    Description = "Desc2",
                    CategoryId = 2,
                    Images = new List<Image>{}
                },
                new Destination
                {
                    Id = 3,
                    Name = "Dest3",
                    Description = "Desc3",
                    CategoryId = 1,
                    Images = new List<Image>{}
                },
            }
            .AsQueryable()
            .BuildMock();

            this.mockRepository.Setup(r => r.All<Destination>()).Returns(destinationsData);

            var result = await this.destinationService
                .GetAllAsync(pageNumber, pageSize, categoryIdToFilter);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.TotalCount);
            Assert.AreEqual(2, result.Destinations.Count());
            Assert.IsTrue(result.Destinations.All(d => d.Id == 1 || d.Id == 3));
            Assert.AreEqual(1, result.TotalPages);
        }

        [Test]
        public async Task DeleteImageAsync_UserIsOwner_DeletesImageFromDbAndFs()
        {
            var destinationId = 1;
            var userId = "owner-user-id";
            var imageUrlToDelete = "/images/TestCat/image1.jpg";
            var imageName = "image1.jpg";
            var categoryFolder = "TestCat";

            var physicalFilePath = Path.Combine(this.wwwRootPath, imageUrlToDelete.TrimStart('/'));
            Directory.CreateDirectory(Path.GetDirectoryName(physicalFilePath));
            File.WriteAllText(physicalFilePath, "dummy image content");

            var imageEntity = new Image { Id = 10, Url = imageUrlToDelete, DestinationId = destinationId };
            var destination = new Destination
            {
                Id = destinationId,
                UserId = userId,
                Images = new List<Image> { imageEntity }
            };
            var destinationsData = new List<Destination> { destination }.AsQueryable().BuildMock();

            this.mockRepository.Setup(r => r.All<Destination>()).Returns(destinationsData);
            this.mockRepository.Setup(r => r.Delete(It.IsAny<Image>())).Callback(() => { });
            this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.destinationService.DeleteImageAsync(destinationId, imageUrlToDelete, userId);

            this.mockRepository.Verify(r => r.All<Destination>(), Times.Once);
            this.mockRepository.Verify(r => r.Delete(imageEntity), Times.Once);
            this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

            Assert.IsFalse(File.Exists(physicalFilePath), "Physical file should have been deleted.");
        }

        [Test]
        public void DeleteImageAsync_UserNotOwnerNorAdmin_ThrowsUnauthorizedAccessException()
        {
            var destinationId = 1;
            var ownerId = "owner-id";
            var attackerId = "attacker-id";
            var imageUrlToDelete = "/images/cat/img.jpg";

            var destination = new Destination { Id = destinationId, UserId = ownerId, Images = new List<Image>() };
            var destinationsData = new List<Destination> { destination }.AsQueryable().BuildMock();
            this.mockRepository.Setup(r => r.All<Destination>()).Returns(destinationsData);

            var attackerUser = new ApplicationUser { Id = attackerId };
            this.mockUserManager.Setup(um => um.FindByIdAsync(attackerId)).ReturnsAsync(attackerUser);
            this.mockUserManager.Setup(um => um.IsInRoleAsync(attackerUser, AdminRoleName)).ReturnsAsync(false);

            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                this.destinationService.DeleteImageAsync(destinationId, imageUrlToDelete, attackerId));
            Assert.AreEqual("You are not allowed to delete images from this destination.", ex.Message);
        }
    }
}