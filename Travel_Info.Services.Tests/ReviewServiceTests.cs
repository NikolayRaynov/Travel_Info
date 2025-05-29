using AngleSharp;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MockQueryable;
using Moq;
using Travel_Info.Common;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;
using Travel_Info.Web.ViewModels.Review;

namespace Travel_Info.Services.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private Mock<IRepository> repository;
        private Mock<IHtmlSanitizer> htmlSanitizer;
        private Mock<UserManager<ApplicationUser>> userManager;
        private ReviewService reviewService;

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

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository>();
            this.htmlSanitizer = new Mock<IHtmlSanitizer>();
            this.userManager = MockUserManager<ApplicationUser>();

            this.reviewService = new ReviewService(
                this.repository.Object,
                this.htmlSanitizer.Object,
                this.userManager.Object
            );

            this.htmlSanitizer.Setup(s =>
            s.Sanitize(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<IMarkupFormatter>()

            ))
            .Returns((string htmlInput, string p2, IMarkupFormatter p3) => htmlInput ?? string.Empty);
        }

        [Test]
        public async Task GetAllReviewsByDestinationIdAsync_ShouldReturnEmptyList_WhenNoReviewsMatch()
        {
            int destinationIdWithNoReviews = 99;
            var allReviewsData = new List<Review>
            {
                new Review 
                { 
                    Id = 1, 
                    DestinationId = 1, 
                    Rating = 5, 
                    Comment = "Test", 
                    User = new ApplicationUser
                    {
                        UserName = "UserA" 
                    } 
                }
            };
            var mockReviewsQueryable = allReviewsData.AsQueryable().BuildMock();

            this.repository.Setup(r => r.AllReadonly<Review>()).Returns(mockReviewsQueryable);

            var result = await this.reviewService.GetAllReviewsByDestinationIdAsync(destinationIdWithNoReviews);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllReviewsByDestinationIdAsync_ShouldReturnMappedReviews_WhenReviewsMatch()
        {
            int targetDestinationId = 1;
            var userOne = new ApplicationUser 
            { 
                Id = "user1", 
                UserName = "UserOneName" 
            };
            var reviewsData = new List<Review>
            {
                new Review 
                { 
                    Id = 10, 
                    DestinationId = targetDestinationId, 
                    Rating = 5, 
                    Comment = "Fantastic", 
                    UserId = "user1", 
                    User = userOne, 
                    CreatedAt = DateTime.UtcNow 
                },
                new Review 
                { 
                    Id = 11, 
                    DestinationId = 2, 
                    Rating = 4, 
                    Comment = "Good", 
                    UserId = "user2", 
                    User = new ApplicationUser
                    { 
                        UserName = "UserTwoName"
                    }, 
                    CreatedAt = DateTime.UtcNow }
            };

            var mockReviewsQueryable = reviewsData.AsQueryable().BuildMock();
            this.repository.Setup(r => r.AllReadonly<Review>()).Returns(mockReviewsQueryable);

            var result = (await this.reviewService.GetAllReviewsByDestinationIdAsync(targetDestinationId)).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(10, result.First().Id);
            Assert.AreEqual("Fantastic", result.First().Comment);
            Assert.AreEqual("UserOneName", result.First().User);
        }

        [Test]
        public async Task GetReviewByIdAsync_ShouldReturnNull_WhenReviewNotFound()
        {
            int nonExistentReviewId = 999;
            var emptyList = new List<Review>().AsQueryable().BuildMock();

            this.repository.Setup(r => r.AllReadonly<Review>()).Returns(emptyList);

            var result = await this.reviewService.GetReviewByIdAsync(nonExistentReviewId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetReviewByIdAsync_ShouldReturnReview_WhenReviewExists()
        {
            int existingReviewId = 1;
            var reviewUser = new ApplicationUser 
            { 
                Id = "user-abc", 
                UserName = "Reviewer" 
            };
            var reviewData =
                new Review 
                { 
                    Id = existingReviewId, 
                    Comment = "My Review", 
                    Rating = 4, 
                    DestinationId = 10, 
                    UserId = reviewUser.Id, 
                    User = reviewUser, 
                    CreatedAt = DateTime.UtcNow 
                };
            var listWithReview = new List<Review> { reviewData }.AsQueryable().BuildMock();

            this.repository
                .Setup(r => r.AllReadonly<Review>())
                .Returns(listWithReview);

            var result = await this.reviewService
                .GetReviewByIdAsync(existingReviewId);

            Assert.IsNotNull(result);
            Assert.AreEqual(existingReviewId, result.Id);
            Assert.AreEqual("My Review", result.Comment);
            Assert.AreEqual(reviewUser.Id, result.User);
        }

        [Test]
        public async Task AddReviewAsync_ValidInput_ShouldAddAndSaveChanges()
        {
            var model = new AddReviewViewModel 
            { 
                DestinationId = 1, 
                Rating = 5, 
                Comment = "New review" 
            };
            string userId = "current-user-id";
            var userForDb = new ApplicationUser { Id = userId };
            var destinationForDb = new Destination { Id = model.DestinationId };

            this.repository.Setup(r => r.AllReadonly<Destination>())
                .Returns(new List<Destination> { destinationForDb }.AsQueryable().BuildMock());
            this.userManager.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(userForDb);
            this.htmlSanitizer.Setup(s => s.Sanitize(model.Comment,
                It.IsAny<string>(), It.IsAny<IMarkupFormatter>())).Returns(model.Comment);

            Review capturedReview = null;

            this.repository.Setup(r => r.AddAsync(It.IsAny<Review>()))
                .Callback<Review>(r => capturedReview = r)
                .Returns(Task.CompletedTask);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.reviewService.AddReviewAsync(model, userId);

            this.repository.Verify(r => r.AddAsync(It.IsAny<Review>()), Times.Once());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());

            Assert.IsNotNull(capturedReview);
            Assert.AreEqual(model.Rating, capturedReview.Rating);
            Assert.AreEqual(model.Comment, capturedReview.Comment);
            Assert.AreEqual(userId, capturedReview.UserId);
            Assert.AreEqual(model.DestinationId, capturedReview.DestinationId);
        }

        [Test]
        public void AddReviewAsync_DestinationNotFound_ShouldThrowInvalidOperationException()
        {
            var model = new AddReviewViewModel 
            { 
                DestinationId = 99, 
                Comment = "No dest" 
            };
            string userId = "user1";

            this.repository.Setup(r => r.AllReadonly<Destination>())
                .Returns(new List<Destination>()
                .AsQueryable()
                .BuildMock());
            this.userManager
                .Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(new ApplicationUser { Id = userId });

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
            this.reviewService.AddReviewAsync(model, userId));

            Assert.AreEqual("The destination does not exist.", ex.Message);
        }

        [Test]
        public async Task UpdateReviewAsync_UserIsOwner_ShouldUpdateAndSaveChanges()
        {
            string ownerUserId = "owner1";
            int reviewId = 1;
            var model = new EditReviewViewModel 
            { 
                Id = reviewId, 
                Rating = 3, 
                Comment = "Updated!" 
            };
            var existingReview = new Review 
            { 
                Id = reviewId, 
                UserId = ownerUserId, 
                Rating = 5, 
                Comment = "Old" 
            };

            this.repository.Setup(r => r.All<Review>())
                .Returns(new List<Review> { existingReview }.AsQueryable().BuildMock());
            this.htmlSanitizer.Setup(s => s.Sanitize(model.Comment, It.IsAny<string>(), It.IsAny<IMarkupFormatter>()))
                .Returns(model.Comment);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.reviewService.UpdateReviewAsync(model, ownerUserId);

            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());

            Assert.AreEqual(3, existingReview.Rating);
            Assert.AreEqual("Updated!", existingReview.Comment);
        }

        [Test]
        public void UpdateReviewAsync_UserNotOwnerNorAdmin_ShouldThrowInvalidOperationException()
        {
            string ownerUserId = "owner1";
            string attackerUserId = "attacker1";
            int reviewId = 1;
            var model = new EditReviewViewModel 
            { 
                Id = reviewId, 
                Comment = "Trying to update" 
            };
            var existingReview = new Review { Id = reviewId, UserId = ownerUserId };
            var attackerUser = new ApplicationUser { Id = attackerUserId };

            this.repository.Setup(r => r.All<Review>())
                .Returns(new List<Review> { existingReview }.AsQueryable().BuildMock());
            this.userManager.Setup(um => um.FindByIdAsync(attackerUserId)).ReturnsAsync(attackerUser);
            this.userManager.Setup(um => um.IsInRoleAsync(attackerUser, ApplicationConstants.AdminRoleName)).ReturnsAsync(false);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
            this.reviewService.UpdateReviewAsync(model, attackerUserId));
            Assert.AreEqual("You are not allowed to edit this review.", ex.Message);
        }

        [Test]
        public async Task DeleteReviewAsync_UserIsAdmin_ShouldDeleteAndSaveChanges()
        {
            string adminUserId = "adminUser";
            string ownerUserId = "someOwner";
            int reviewId = 1;
            var model = new DeleteReviewViewModel 
            { 
                Id = reviewId 
            };
            var reviewToDelete = new Review 
            { 
                Id = reviewId, 
                UserId = ownerUserId 
            };
            var adminAppUser = new ApplicationUser 
            { 
                Id = adminUserId 
            };

            this.repository.Setup(r => r.All<Review>())
                .Returns(new List<Review> { reviewToDelete }
                .AsQueryable()
                .BuildMock());
            this.userManager.Setup(um => um.FindByIdAsync(adminUserId)).ReturnsAsync(adminAppUser);
            this.userManager.Setup(um => um.IsInRoleAsync(adminAppUser, ApplicationConstants.AdminRoleName)).ReturnsAsync(true);
            this.repository.Setup(r => r.Delete(reviewToDelete));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.reviewService.DeleteReviewAsync(model, adminUserId);

            this.repository.Verify(r => r.Delete(reviewToDelete), Times.Once());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public void DeleteReviewAsync_UserNotOwnerNorAdmin_ShouldThrowUnauthorizedAccessException()
        {
            string ownerUserId = "owner1";
            string attackerUserId = "attacker1";
            int reviewId = 1;
            var model = new DeleteReviewViewModel
            { 
                Id = reviewId 
            };
            var existingReview = new Review 
            { 
                Id = reviewId, 
                UserId = ownerUserId 
            };
            var attackerUser = new ApplicationUser 
            { 
                Id = attackerUserId 
            };

            this.repository.Setup(r => r.All<Review>())
                .Returns(new List<Review> { existingReview }.AsQueryable().BuildMock());
            this.userManager.Setup(um => um.FindByIdAsync(attackerUserId)).ReturnsAsync(attackerUser);
            this.userManager.Setup(um => um.IsInRoleAsync(attackerUser, ApplicationConstants.AdminRoleName)).ReturnsAsync(false);

            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            this.reviewService.DeleteReviewAsync(model, attackerUserId));
            Assert.AreEqual("You are not allowed to delete this review.", ex.Message);
        }
    }
}