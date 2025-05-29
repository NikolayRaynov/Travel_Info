using MockQueryable;
using Moq;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;

namespace Travel_Info.Services.Tests
{
    [TestFixture]
    public class FavoritePlaceServiceTests
    {
        private Mock<IRepository> repository;
        private FavoritePlaceService favoritePlaceService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository>();
            this.favoritePlaceService = new FavoritePlaceService(this.repository.Object);
        }

        [Test]
        public async Task AddToFavoritesAsync_WhenItemAlreadyInFavorites_ShouldReturnFalse()
        {
            string userId = "userWithFavorite";
            int destinationId = 1;

            var existingDestination = new Destination { Id = destinationId };
            var existingFavoritePlace = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination> { existingDestination }
            };

            var favoritePlacesData = new List<FavoritePlace> { existingFavoritePlace }.AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(favoritePlacesData);

            bool result = await this.favoritePlaceService.AddToFavoritesAsync(destinationId, userId);

            Assert.IsFalse(result);
            this.repository.Verify(r => r.AddAsync(It.IsAny<FavoritePlace>()), Times.Never());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Never());
        }

        [Test]
        public async Task AddToFavoritesAsync_WhenUserHasNoFavoritesRecord_ShouldCreateRecordAndAddItem()
        {
            string userId = "newUserFavorites";
            int destinationId = 1;

            var destinationToAdd = new Destination
            {
                Id = destinationId,
                Name = "Paris",
                Description = "City of Love"
            };

            var emptyFavoritePlacesData = new List<FavoritePlace>().AsQueryable().BuildMock();
            FavoritePlace capturedFavoriteRecord = null;

            this.repository.SetupSequence(r => r.All<FavoritePlace>())
                           .Returns(emptyFavoritePlacesData)
                           .Returns(emptyFavoritePlacesData);

            this.repository.Setup(r => r.GetByIdAsync<Destination>(destinationId)).ReturnsAsync(destinationToAdd);
            this.repository.Setup(r => r.AddAsync(It.IsAny<FavoritePlace>()))
                           .Callback<FavoritePlace>(fp => capturedFavoriteRecord = fp)
                           .Returns(Task.CompletedTask);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            bool result = await this.favoritePlaceService.AddToFavoritesAsync(destinationId, userId);

            Assert.IsTrue(result);
            this.repository.Verify(r => r.AddAsync(It.IsAny<FavoritePlace>()), Times.Once());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.IsNotNull(capturedFavoriteRecord);
            Assert.AreEqual(userId, capturedFavoriteRecord.UserId);
            Assert.IsTrue(capturedFavoriteRecord.Destinations.Any(d => d.Id == destinationId));
        }

        [Test]
        public async Task AddToFavoritesAsync_WhenUserHasFavoritesRecord_ShouldAddItemToExistingRecord()
        {
            string userId = "userWithExistingFavorites";
            int destinationIdToAdd = 2;
            int alreadyFavoriteDestinationId = 1;

            var destinationToBeAdded = new Destination
            {
                Id = destinationIdToAdd,
                Name = "Rome"
            };

            var existingUserFavorites = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination>
                {
                    new Destination { Id = alreadyFavoriteDestinationId }
                }
            };

            var noMatchingFavoriteQuery = new List<FavoritePlace> {
                new FavoritePlace { UserId = userId, Destinations = 
                    new List<Destination> 
                    { 
                        new Destination 
                        {
                            Id = alreadyFavoriteDestinationId 
                        } 
                    } 
                }
            }
            .AsQueryable()
            .BuildMock();

            var userFavoritesQuery = new List<FavoritePlace> { existingUserFavorites }.AsQueryable().BuildMock();

            this.repository.SetupSequence(r => r.All<FavoritePlace>())
                           .Returns(noMatchingFavoriteQuery)
                           .Returns(userFavoritesQuery);

            this.repository.Setup(r => r.GetByIdAsync<Destination>(destinationIdToAdd)).ReturnsAsync(destinationToBeAdded);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            bool result = await this.favoritePlaceService.AddToFavoritesAsync(destinationIdToAdd, userId);

            Assert.IsTrue(result);
            this.repository.Verify(r => r.AddAsync(It.IsAny<FavoritePlace>()), Times.Never());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.AreEqual(2, existingUserFavorites.Destinations.Count);
            Assert.IsTrue(existingUserFavorites.Destinations.Any(d => d.Id == destinationIdToAdd));
        }

        [Test]
        public async Task AddToFavoritesAsync_WhenDestinationToAddIsNotFound_ShouldNotAddAndReturnTrue()
        {
            string userId = "user1";
            int nonExistentDestinationId = 99;

            var emptyFavoritesForExistingCheck = new List<FavoritePlace>().AsQueryable().BuildMock();
            var userFavoritesRecord = new FavoritePlace { UserId = userId, Destinations = new List<Destination>() };
            var favoritesForUserRecordGet = new List<FavoritePlace> { userFavoritesRecord }.AsQueryable().BuildMock();

            this.repository.SetupSequence(r => r.All<FavoritePlace>())
                           .Returns(emptyFavoritesForExistingCheck)
                           .Returns(favoritesForUserRecordGet);

            this.repository.Setup(r => r.GetByIdAsync<Destination>(nonExistentDestinationId)).ReturnsAsync((Destination)null);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            bool result = await this.favoritePlaceService.AddToFavoritesAsync(nonExistentDestinationId, userId);

            Assert.IsTrue(result);
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.IsFalse(userFavoritesRecord.Destinations.Any(d => d.Id == nonExistentDestinationId));
        }

        [Test]
        public async Task GetAllFavoritesAsync_ShouldReturnMappedDestinations_WhenFavoritesExist()
        {
            string userId = "userWithFavorites";
            var imageForDest1 = new Image { Url = "/images/eiffel.jpg" };
            var imageForDest2 = new Image { Url = "/images/colosseum.jpg" };

            var destination1 = new Destination
            {
                Id = 1,
                Name = "Eiffel Tower",
                Description = "Iconic tower in Paris.",
                Images = new List<Image> { imageForDest1 }
            };
            var destination2 = new Destination
            {
                Id = 2,
                Name = "Colosseum",
                Description = "Ancient amphitheater in Rome.",
                Images = new List<Image> { imageForDest2 }
            };

            var userFavoriteRecord = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination> { destination1, destination2 }
            };

            var favoritePlacesData = new List<FavoritePlace> { userFavoriteRecord }.AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(favoritePlacesData);

            var result = (await this.favoritePlaceService.GetAllFavoritesAsync(userId)).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var eiffelVm = result.FirstOrDefault(d => d.Id == 1);
            Assert.IsNotNull(eiffelVm);
            Assert.AreEqual("Eiffel Tower", eiffelVm.Name);
            Assert.AreEqual("Iconic tower in Paris.", eiffelVm.Description);
            Assert.AreEqual(1, eiffelVm.ImageUrls.Count);
            Assert.AreEqual("/images/eiffel.jpg", eiffelVm.ImageUrls.First());

            var colosseumVm = result.FirstOrDefault(d => d.Id == 2);
            Assert.IsNotNull(colosseumVm);
            Assert.AreEqual("Colosseum", colosseumVm.Name);
        }

        [Test]
        public async Task GetAllFavoritesAsync_ShouldReturnEmptyList_WhenUserHasNoFavorites()
        {
            string userId = "userWithoutFavorites";
            var emptyFavoritePlacesData = new List<FavoritePlace>().AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(emptyFavoritePlacesData);

            var result = await this.favoritePlaceService.GetAllFavoritesAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task IsInFavoriteAsync_ShouldReturnTrue_WhenDestinationIsInFavorites()
        {
            string userId = "user1";
            int destinationId = 1;
            var favoriteDestination = new Destination { Id = destinationId };
            var favoriteRecord = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination> { favoriteDestination }
            };
            var data = new List<FavoritePlace> { favoriteRecord }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(data);

            bool result = await this.favoritePlaceService.IsInFavoriteAsync(destinationId, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsInFavoriteAsync_ShouldReturnFalse_WhenDestinationIsNotInFavorites()
        {
            string userId = "user1";
            int targetDestinationId = 1;
            int actualFavoriteDestinationId = 2;
            var actualFavoriteDest = new Destination { Id = actualFavoriteDestinationId };
            var favoriteRecord = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination> { actualFavoriteDest }
            };
            var data = new List<FavoritePlace> { favoriteRecord }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(data);

            bool result = await this.favoritePlaceService.IsInFavoriteAsync(targetDestinationId, userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsInFavoriteAsync_ShouldReturnFalse_WhenUserHasNoFavoritesRecord()
        {
            string userId = "userNoRecords";
            int destinationId = 1;
            var data = new List<FavoritePlace>().AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(data);

            bool result = await this.favoritePlaceService.IsInFavoriteAsync(destinationId, userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task RemoveFromFavoritesAsync_WhenItemExistsAndOthersRemain_ShouldUpdateRecord()
        {
            string userId = "user1";
            int destinationIdToRemove = 1;
            int otherDestinationId = 2;

            var destinationToRemove = new Destination
            { 
                Id = destinationIdToRemove 
            };
            var otherDestination = new Destination
            { 
                Id = otherDestinationId 
            };
            var userFavoriteRecord = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination> { destinationToRemove, otherDestination }
            };
            var data = new List<FavoritePlace> { userFavoriteRecord }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(data);
            this.repository.Setup(r => r.Update(userFavoriteRecord));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.favoritePlaceService.RemoveFromFavoritesAsync(destinationIdToRemove, userId);

            this.repository.Verify(r => r.Update(userFavoriteRecord), Times.Once());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.AreEqual(1, userFavoriteRecord.Destinations.Count);
            Assert.IsFalse(userFavoriteRecord.Destinations.Any(d => d.Id == destinationIdToRemove));
            Assert.IsTrue(userFavoriteRecord.Destinations.Any(d => d.Id == otherDestinationId));
        }

        [Test]
        public async Task RemoveFromFavoritesAsync_WhenLastItemRemoved_ShouldDeleteRecord()
        {
            string userId = "user1";
            int destinationIdToRemove = 1;

            var destinationToRemove =
                new Destination 
                { 
                    Id = destinationIdToRemove 
                };
            var userFavoriteRecord = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination> { destinationToRemove }
            };
            var data = new List<FavoritePlace> { userFavoriteRecord }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(data);
            this.repository.Setup(r => r.Delete(userFavoriteRecord));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.favoritePlaceService.RemoveFromFavoritesAsync(destinationIdToRemove, userId);

            this.repository.Verify(r => r.Delete(userFavoriteRecord), Times.Once());
            this.repository.Verify(r => r.Update(It.IsAny<FavoritePlace>()), Times.Never());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.IsEmpty(userFavoriteRecord.Destinations);
        }

        [Test]
        public void RemoveFromFavoritesAsync_WhenUserHasNoFavoritesRecord_ShouldThrowInvalidOperationException()
        {
            string userId = "userNoRecord";
            int destinationId = 1;
            var data = new List<FavoritePlace>().AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(data);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                this.favoritePlaceService.RemoveFromFavoritesAsync(destinationId, userId));
            Assert.AreEqual("This destination is not in your favorites.", ex.Message);
        }

        [Test]
        public async Task RemoveFromFavoritesAsync_WhenDestinationNotInRecord_ShouldNotChangeListButStillUpdateAndSave()
        {
            string userId = "user1";
            int destinationIdToRemove = 99;
            int existingDestinationId = 1;

            var existingDestination = new Destination { Id = existingDestinationId };
            var userFavoriteRecord = new FavoritePlace
            {
                UserId = userId,
                Destinations = new List<Destination> { existingDestination }
            };
            var data = new List<FavoritePlace> { userFavoriteRecord }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<FavoritePlace>()).Returns(data);
            this.repository.Setup(r => r.Update(userFavoriteRecord));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.favoritePlaceService.RemoveFromFavoritesAsync(destinationIdToRemove, userId);

            this.repository.Verify(r => r.Update(userFavoriteRecord), Times.Once());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.AreEqual(1, userFavoriteRecord.Destinations.Count);
            Assert.IsTrue(userFavoriteRecord.Destinations.Any(d => d.Id == existingDestinationId));
        }
    }
}