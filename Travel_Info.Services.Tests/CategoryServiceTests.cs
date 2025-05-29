using Microsoft.AspNetCore.Hosting;
using MockQueryable;
using Moq;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;
using Travel_Info.Services.Data.Interfaces;
using Travel_Info.Web.ViewModels.Category;

namespace Travel_Info.Services.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IRepository> mockRepository;
        private Mock<IFileService> mockFileService;
        private Mock<IWebHostEnvironment> mockWebHostEnvironment;
        private CategoryService categoryService;
        private string testWebRootPath;

        [SetUp]
        public void Setup()
        {
            this.mockRepository = new Mock<IRepository>();
            this.mockFileService = new Mock<IFileService>();
            this.mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

            this.testWebRootPath = Path.Combine(Path.GetTempPath(), "test_wwwroot_category_service");

            Directory.CreateDirectory(testWebRootPath);

            this.categoryService = new CategoryService(
                this.mockRepository.Object,
                this.mockWebHostEnvironment.Object,
                this.mockFileService.Object
            );
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                if (Directory.Exists(testWebRootPath))
                {
                    Directory.Delete(testWebRootPath, true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during cleanup: {ex.Message}");
            }
        }

        [Test]
        public async Task AddAsync_ShouldAddCategoryAndAttemptToCreateDirectory()
        {
            var model = new AddCategoryViewModel { NameBg = "Тест Категория", NameEn = "Test Category" };
            var sanitizedNameEn = "Test_Category";
            var expectedFolderPath = Path.Combine(testWebRootPath, "images", sanitizedNameEn);

            this.mockFileService.Setup(fs => fs.SanitizeFolderName(model.NameEn)).Returns(sanitizedNameEn);
            this.mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
            this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            Category capturedCategory = null;

            this.mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>()))
                           .Callback<Category>(c => capturedCategory = c)
                           .Returns(Task.CompletedTask);

            await categoryService.AddAsync(model, this.testWebRootPath);

            this.mockFileService.Verify(fs => fs.SanitizeFolderName(model.NameEn), Times.Once);
            this.mockRepository.Verify(r => r.AddAsync(It.Is<Category>(c =>
                c.NameBg == model.NameBg &&
                c.NameEn == model.NameEn
            )), Times.Once);
            this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

            Assert.IsNotNull(capturedCategory);
            Assert.AreEqual(model.NameBg, capturedCategory.NameBg);
            Assert.AreEqual(model.NameEn, capturedCategory.NameEn);
        }

        [Test]
        public async Task ExistByIdAsync_ShouldReturnTrue_WhenCategoryExists()
        {
            var categoryId = 1;
            var categories = new List<Category> 
            { 
                new Category 
                { 
                    Id = categoryId 
                }
            }
            .AsQueryable()
            .BuildMock();

            this.mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await categoryService.ExistByIdAsync(categoryId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistByIdAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
        {
            var categoryId = 1;
            var categories = new List<Category>().AsQueryable().BuildMock();

            this.mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await categoryService.ExistByIdAsync(categoryId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllCategoriesMappedToViewModel()
        {
            var dbCategories = new List<Category>
            {
                new Category { Id = 1, NameBg = "Природа", NameEn = "Nature" },
                new Category { Id = 2, NameBg = "Плаж", NameEn = "Beach" }
            };
            var mockDbCategories = dbCategories.AsQueryable().BuildMock();

            this.mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(mockDbCategories);

            var result = await categoryService.GetAllAsync();
            var resultList = result.ToList();

            Assert.AreEqual(2, resultList.Count);
            Assert.IsTrue(resultList.Any(c => c.Id == 1 && c.NameBg == "Природа" && c.NameEn == "Nature"));
            Assert.IsTrue(resultList.Any(c => c.Id == 2 && c.NameBg == "Плаж" && c.NameEn == "Beach"));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCategoryViewModel_WhenCategoryExists()
        {
            var categoryId = 1;
            var category = new Category { Id = categoryId, NameBg = "ТестБГ", NameEn = "TestEN" };
            var categories = new List<Category> { category }.AsQueryable().BuildMock();

            this.mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await categoryService.GetByIdAsync(categoryId);

            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result.Id);
            Assert.AreEqual(category.NameBg, result.NameBg);
            Assert.AreEqual(category.NameEn, result.NameEn);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            var categoryId = 1;
            var categories = new List<Category>().AsQueryable().BuildMock();

            this.mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await this.categoryService.GetByIdAsync(categoryId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetCategoryForEditAsync_ShouldReturnCategoryViewModel_WhenCategoryExists()
        {
            var categoryId = 1;
            var category = new Category { Id = categoryId, NameBg = "ТестБГ", NameEn = "TestEN" };
            var categories = new List<Category> { category }.AsQueryable().BuildMock();

            this.mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await this.categoryService.GetCategoryForEditAsync(categoryId);

            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result.Id);
            Assert.AreEqual(category.NameBg, result.NameBg);
            Assert.AreEqual(category.NameEn, result.NameEn);
        }

        [Test]
        public async Task GetCategoryNameEnByIdAsync_ShouldReturnNameEn_WhenCategoryExists()
        {
            var categoryId = 1;
            var categoryNameEn = "TestEN";
            var category = new Category { Id = categoryId, NameEn = categoryNameEn };
            var categories = new List<Category> { category }.AsQueryable().BuildMock();

            this.mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await this.categoryService.GetCategoryNameEnByIdAsync(categoryId);

            Assert.AreEqual(categoryNameEn, result);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateCategoryPropertiesAndSaveChanges()
        {
            var categoryId = 1;
            var existingCategory = new Category { Id = categoryId, NameBg = "СтароБГ", NameEn = "OldEN" };
            var model = new CategoryViewModel { Id = categoryId, NameBg = "НовоБГ", NameEn = "NewEN" };

            var sanitizedOldName = "OldEN_sanitized";
            var sanitizedNewName = "NewEN_sanitized";

            this.mockRepository.Setup(r => r.GetByIdAsync<Category>(categoryId)).ReturnsAsync(existingCategory);

            this.mockFileService.Setup(fs => fs.SanitizeFolderName("OldEN")).Returns(sanitizedOldName);
            this.mockFileService.Setup(fs => fs.SanitizeFolderName("NewEN")).Returns(sanitizedNewName);

            this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.categoryService.UpdateAsync(categoryId, model, testWebRootPath);
            this.mockRepository.Verify(r => r.GetByIdAsync<Category>(categoryId), Times.Once);

            Assert.AreEqual(model.NameBg, existingCategory.NameBg);
            Assert.AreEqual(sanitizedNewName.ToLower(), existingCategory.NameEn);

            this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldNotAttemptToRenameFolder_WhenSanitizedNameEnIsSame()
        {
            var categoryId = 1;
            var existingCategory = new Category { Id = categoryId, NameBg = "СтароБГ", NameEn = "SameOldEN" };
            var model = new CategoryViewModel { Id = categoryId, NameBg = "НовоБГ", NameEn = "SameOldEN" };
            var sanitizedName = "SameOldEN_sanitized";

            this.mockRepository.Setup(r => r.GetByIdAsync<Category>(categoryId)).ReturnsAsync(existingCategory);
            this.mockFileService.Setup(fs => fs.SanitizeFolderName(It.IsAny<string>())).Returns(sanitizedName);
            this.mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.categoryService.UpdateAsync(categoryId, model, testWebRootPath);

            this.mockRepository.Verify(r => r.GetByIdAsync<Category>(categoryId), Times.Once);

            Assert.AreEqual(model.NameBg, existingCategory.NameBg);
            Assert.AreEqual(sanitizedName.ToLower(), existingCategory.NameEn);

            this.mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void UpdateAsync_ShouldThrowInvalidOperationException_WhenCategoryNotFound()
        {
            var categoryId = 1;
            var model = new CategoryViewModel { Id = categoryId, NameBg = "НовоБГ", NameEn = "NewEN" };

            this.mockRepository.Setup(r => r.GetByIdAsync<Category>(categoryId)).ReturnsAsync((Category)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await categoryService.UpdateAsync(categoryId, model, testWebRootPath));

            Assert.AreEqual("The category is not found.", ex.Message);
        }
    }
}