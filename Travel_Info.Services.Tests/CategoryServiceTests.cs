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
        private Mock<IRepository> _mockRepository;
        private Mock<IFileService> _mockFileService;
        private Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private CategoryService _categoryService;
        private string _testWebRootPath;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository>();
            _mockFileService = new Mock<IFileService>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();

            _testWebRootPath = Path.Combine(Path.GetTempPath(), "test_wwwroot_category_service");

            Directory.CreateDirectory(_testWebRootPath);

            _categoryService = new CategoryService(
                _mockRepository.Object,
                _mockWebHostEnvironment.Object,
                _mockFileService.Object
            );
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                if (Directory.Exists(_testWebRootPath))
                {
                    Directory.Delete(_testWebRootPath, true);
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
            var expectedFolderPath = Path.Combine(_testWebRootPath, "images", sanitizedNameEn);

            _mockFileService.Setup(fs => fs.SanitizeFolderName(model.NameEn)).Returns(sanitizedNameEn);
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);
            _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            Category capturedCategory = null;
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>()))
                           .Callback<Category>(c => capturedCategory = c)
                           .Returns(Task.CompletedTask);

            await _categoryService.AddAsync(model, _testWebRootPath);

            _mockFileService.Verify(fs => fs.SanitizeFolderName(model.NameEn), Times.Once);
            _mockRepository.Verify(r => r.AddAsync(It.Is<Category>(c =>
                c.NameBg == model.NameBg &&
                c.NameEn == model.NameEn
            )), Times.Once);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);

            Assert.IsNotNull(capturedCategory);
            Assert.AreEqual(model.NameBg, capturedCategory.NameBg);
            Assert.AreEqual(model.NameEn, capturedCategory.NameEn);
        }

        [Test]
        public async Task ExistByIdAsync_ShouldReturnTrue_WhenCategoryExists()
        {
            var categoryId = 1;
            var categories = new List<Category> { new Category { Id = categoryId } }.AsQueryable().BuildMock();
            _mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await _categoryService.ExistByIdAsync(categoryId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistByIdAsync_ShouldReturnFalse_WhenCategoryDoesNotExist()
        {
            var categoryId = 1;
            var categories = new List<Category>().AsQueryable().BuildMock();
            _mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await _categoryService.ExistByIdAsync(categoryId);

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
            _mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(mockDbCategories);

            var result = await _categoryService.GetAllAsync();
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
            _mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await _categoryService.GetByIdAsync(categoryId);

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
            _mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await _categoryService.GetByIdAsync(categoryId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetCategoryForEditAsync_ShouldReturnCategoryViewModel_WhenCategoryExists()
        {
            var categoryId = 1;
            var category = new Category { Id = categoryId, NameBg = "ТестБГ", NameEn = "TestEN" };
            var categories = new List<Category> { category }.AsQueryable().BuildMock();
            _mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await _categoryService.GetCategoryForEditAsync(categoryId);

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
            _mockRepository.Setup(r => r.AllReadonly<Category>()).Returns(categories);

            var result = await _categoryService.GetCategoryNameEnByIdAsync(categoryId);

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

            _mockRepository.Setup(r => r.GetByIdAsync<Category>(categoryId)).ReturnsAsync(existingCategory);

            _mockFileService.Setup(fs => fs.SanitizeFolderName("OldEN")).Returns(sanitizedOldName);
            _mockFileService.Setup(fs => fs.SanitizeFolderName("NewEN")).Returns(sanitizedNewName);

            _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await _categoryService.UpdateAsync(categoryId, model, _testWebRootPath);
            _mockRepository.Verify(r => r.GetByIdAsync<Category>(categoryId), Times.Once);

            Assert.AreEqual(model.NameBg, existingCategory.NameBg);
            Assert.AreEqual(sanitizedNewName.ToLower(), existingCategory.NameEn);

            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldNotAttemptToRenameFolder_WhenSanitizedNameEnIsSame()
        {
            var categoryId = 1;
            var existingCategory = new Category { Id = categoryId, NameBg = "СтароБГ", NameEn = "SameOldEN" };
            var model = new CategoryViewModel { Id = categoryId, NameBg = "НовоБГ", NameEn = "SameOldEN" };
            var sanitizedName = "SameOldEN_sanitized";

            _mockRepository.Setup(r => r.GetByIdAsync<Category>(categoryId)).ReturnsAsync(existingCategory);
            _mockFileService.Setup(fs => fs.SanitizeFolderName(It.IsAny<string>())).Returns(sanitizedName);
            _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await _categoryService.UpdateAsync(categoryId, model, _testWebRootPath);

            _mockRepository.Verify(r => r.GetByIdAsync<Category>(categoryId), Times.Once);
            Assert.AreEqual(model.NameBg, existingCategory.NameBg);
            Assert.AreEqual(sanitizedName.ToLower(), existingCategory.NameEn);
            _mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void UpdateAsync_ShouldThrowInvalidOperationException_WhenCategoryNotFound()
        {
            var categoryId = 1;
            var model = new CategoryViewModel { Id = categoryId, NameBg = "НовоБГ", NameEn = "NewEN" };
            _mockRepository.Setup(r => r.GetByIdAsync<Category>(categoryId)).ReturnsAsync((Category)null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _categoryService.UpdateAsync(categoryId, model, _testWebRootPath));
            Assert.AreEqual("The category is not found.", ex.Message);
        }
    }
}