using MockQueryable;
using Moq;
using Travel_Info.Data.Models;
using Travel_Info.Data.Repository.Interfaces;
using Travel_Info.Services.Data;

namespace Travel_Info.Services.Tests
{
    [TestFixture]
    public class PlaceToVisitServiceTests
    {
        private Mock<IRepository> repository;
        private PlaceToVisitService placeToVisitService;

        [SetUp]
        public void Setup()
        {
            this.repository = new Mock<IRepository>();
            this.placeToVisitService = new PlaceToVisitService(this.repository.Object);
        }

        [Test]
        public async Task AddToWishlistAsync_WhenItemAlreadyInWishlist_ShouldReturnFalse()
        {
            string userId = "user123";
            int destinationId = 1;

            var existingDestination = new Destination { Id = destinationId };
            var existingPlaceToVisit = new PlaceToVisit
            {
                UserId = userId,
                Destinations = new List<Destination> { existingDestination }
            };

            var placesToVisitData = new List<PlaceToVisit> { existingPlaceToVisit }.AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(placesToVisitData);

            bool result = await this.placeToVisitService.AddToWishlistAsync(destinationId, userId);

            Assert.IsFalse(result);
            this.repository.Verify(r => r.AddAsync(It.IsAny<PlaceToVisit>()), Times.Never());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Never());
        }

        [Test]
        public async Task AddToWishlistAsync_WhenUserHasNoWishlist_ShouldCreateWishlistAndAddItem()
        {
            string userId = "user-new-wishlist";
            int destinationId = 1;

            var destinationToAdd = new Destination
            {
                Id = destinationId,
                Name = "Beach",
                Description = "Sunny beach"
            };

            var emptyPlacesToVisitData = new List<PlaceToVisit>().AsQueryable().BuildMock();
            PlaceToVisit capturedWishlist = null;

            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(emptyPlacesToVisitData);
            this.repository.Setup(r => r.GetByIdAsync<Destination>(destinationId)).ReturnsAsync(destinationToAdd);
            this.repository.Setup(r => r.AddAsync(It.IsAny<PlaceToVisit>()))
                           .Callback<PlaceToVisit>(ptv => capturedWishlist = ptv)
                           .Returns(Task.CompletedTask);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            bool result = await this.placeToVisitService.AddToWishlistAsync(destinationId, userId);

            Assert.IsTrue(result);
            this.repository.Verify(r => r.AddAsync(It.IsAny<PlaceToVisit>()), Times.Once());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.IsNotNull(capturedWishlist);
            Assert.AreEqual(userId, capturedWishlist.UserId);
            Assert.IsTrue(capturedWishlist.Destinations.Any(d => d.Id == destinationId));
        }

        [Test]
        public async Task AddToWishlistAsync_WhenUserHasWishlist_ShouldAddItemToExistingWishlist()
        {
            string userId = "user-existing-wishlist";
            int destinationIdToAdd = 2;
            int existingDestinationId = 1;

            var destinationToAddToWishlist = new Destination
            {
                Id = destinationIdToAdd,
                Name = "Mountain",
                Description = "High mountain"
            };

            var existingWishlist = new PlaceToVisit
            {
                UserId = userId,
                Destinations = new List<Destination>
                {
                    new Destination 
                    { 
                        Id = existingDestinationId 
                    }
                }
            };

            var placesToVisitData = new List<PlaceToVisit> { existingWishlist }.AsQueryable().BuildMock();

            this.repository.SetupSequence(r => r.All<PlaceToVisit>())
                           .Returns(new List<PlaceToVisit> {
                                new PlaceToVisit 
                                { 
                                    UserId = userId, 
                                    Destinations = new List<Destination> 
                                    { 
                                        new Destination 
                                        { 
                                            Id = existingDestinationId 
                                        } 
                                    } 
                                }
                           }
                           .AsQueryable()
                           .BuildMock())
                           .Returns(new List<PlaceToVisit> { existingWishlist }
                           .AsQueryable()
                           .BuildMock());

            this.repository.Setup(r => r.GetByIdAsync<Destination>(destinationIdToAdd))
                .ReturnsAsync(destinationToAddToWishlist);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            bool result = await this.placeToVisitService.AddToWishlistAsync(destinationIdToAdd, userId);

            Assert.IsTrue(result);
            this.repository.Verify(r => r.AddAsync(It.IsAny<PlaceToVisit>()), Times.Never());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.AreEqual(2, existingWishlist.Destinations.Count);
            Assert.IsTrue(existingWishlist.Destinations.Any(d => d.Id == destinationIdToAdd));
        }

        [Test]
        public async Task AddToWishlistAsync_WhenDestinationNotFound_ShouldNotAddItemAndReturnTrue()
        {
            string userId = "user1";
            int nonExistentDestinationId = 99;

            var emptyPlacesToVisitForCheck = new List<PlaceToVisit>().AsQueryable().BuildMock();
            var userWishlist = new PlaceToVisit 
            { 
                UserId = userId, 
                Destinations = new List<Destination>() 
            };
            var placesToVisitForGet = new List<PlaceToVisit> { userWishlist }.AsQueryable().BuildMock();

            this.repository.SetupSequence(r => r.All<PlaceToVisit>())
                .Returns(emptyPlacesToVisitForCheck)
                .Returns(placesToVisitForGet);

            this.repository.Setup(r => r.GetByIdAsync<Destination>(nonExistentDestinationId))
                .ReturnsAsync((Destination)null);
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            bool result = await this.placeToVisitService.AddToWishlistAsync(nonExistentDestinationId, userId);

            Assert.IsTrue(result);
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());
            Assert.IsFalse(userWishlist.Destinations.Any(d => d.Id == nonExistentDestinationId));
        }


        [Test]
        public async Task GetAllDesiredPlacesAsync_ShouldReturnMappedDestinations_WhenWishlistExists()
        {
            string userId = "userWithWishlist";
            var image1 = new Image { Url = "/img/dest1.jpg" };
            var image2 = new Image { Url = "/img/dest2.png" };

            var destination1 = new Destination
            {
                Id = 1,
                Name = "Paris",
                Description = "City of Lights",
                Images = new List<Image> { image1 }
            };
            var destination2 = new Destination
            {
                Id = 2,
                Name = "Rome",
                Description = "Eternal City",
                Images = new List<Image> { image2 }
            };

            var userWishlist = new PlaceToVisit
            {
                UserId = userId,
                Destinations = new List<Destination> { destination1, destination2 }
            };

            var placesToVisitData = new List<PlaceToVisit> { userWishlist }.AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(placesToVisitData);

            var result = (await this.placeToVisitService.GetAllDesiredPlacesAsync(userId)).ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var parisVm = result.FirstOrDefault(d => d.Id == 1);
            Assert.IsNotNull(parisVm);
            Assert.AreEqual("Paris", parisVm.Name);
            Assert.AreEqual("City of Lights", parisVm.Description);
            Assert.AreEqual(1, parisVm.ImageUrls.Count);
            Assert.AreEqual("/img/dest1.jpg", parisVm.ImageUrls.First());

            var romeVm = result.FirstOrDefault(d => d.Id == 2);
            Assert.IsNotNull(romeVm);
            Assert.AreEqual("Rome", romeVm.Name);
        }

        [Test]
        public async Task GetAllDesiredPlacesAsync_ShouldReturnEmptyList_WhenWishlistIsEmptyOrNotFound()
        {
            string userId = "userWithoutWishlist";
            var emptyPlacesToVisitData = new List<PlaceToVisit>().AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(emptyPlacesToVisitData);

            var result = await this.placeToVisitService.GetAllDesiredPlacesAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task IsInWishlistAsync_ShouldReturnTrue_WhenDestinationIsInWishlist()
        {
            string userId = "user1";
            int destinationId = 1;
            var destination = new Destination { Id = destinationId };
            var wishlist = 
                new PlaceToVisit 
                { 
                    UserId = userId, 
                    Destinations = new List<Destination>{ destination } 
                };

            var data = new List<PlaceToVisit> { wishlist }.AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(data);

            bool result = await this.placeToVisitService.IsInWishlistAsync(destinationId, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsInWishlistAsync_ShouldReturnFalse_WhenDestinationIsNotInWishlist()
        {
            string userId = "user1";
            int destinationId = 1;
            int otherDestinationId = 2;
            var otherDestination = new Destination { Id = otherDestinationId };
            var wishlist = 
                new PlaceToVisit 
                { 
                    UserId = userId, 
                    Destinations = new List<Destination> { otherDestination } 
                };

            var data = new List<PlaceToVisit> { wishlist }.AsQueryable().BuildMock();
            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(data);

            bool result = await this.placeToVisitService.IsInWishlistAsync(destinationId, userId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsInWishlistAsync_ShouldReturnFalse_WhenUserHasNoWishlist()
        {
            string userId = "userWithoutWishlist";
            int destinationId = 1;
            var data = new List<PlaceToVisit>().AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(data);

            bool result = await this.placeToVisitService.IsInWishlistAsync(destinationId, userId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task RemoveFromWishlistAsync_WhenItemExists_ShouldRemoveIt()
        {
            string userId = "user1";
            int destinationIdToRemove = 1;
            int otherDestinationId = 2;

            var destinationToRemove = new Destination { Id = destinationIdToRemove };
            var otherDestination = new Destination { Id = otherDestinationId };
            var userWishlist = new PlaceToVisit
            {
                UserId = userId,
                Destinations = new List<Destination> { destinationToRemove, otherDestination }
            };
            var data = new List<PlaceToVisit> { userWishlist }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(data);
            this.repository.Setup(r => r.Update(userWishlist));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.placeToVisitService.RemoveFromWishlistAsync(destinationIdToRemove, userId);

            this.repository.Verify(r => r.Update(userWishlist), Times.Exactly(2));
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());

            Assert.AreEqual(1, userWishlist.Destinations.Count);
            Assert.IsFalse(userWishlist.Destinations.Any(d => d.Id == destinationIdToRemove));
            Assert.IsTrue(userWishlist.Destinations.Any(d => d.Id == otherDestinationId));
        }

        [Test]
        public async Task RemoveFromWishlistAsync_WhenLastItemRemoved_ShouldDeleteWishlistRecord()
        {
            string userId = "user1";
            int destinationIdToRemove = 1;

            var destinationToRemove = new Destination { Id = destinationIdToRemove };
            var userWishlist = new PlaceToVisit
            {
                UserId = userId,
                Destinations = new List<Destination> { destinationToRemove }
            };
            var data = new List<PlaceToVisit> { userWishlist }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(data);
            this.repository.Setup(r => r.Delete(userWishlist));
            this.repository.Setup(r => r.Update(userWishlist));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);


            await this.placeToVisitService.RemoveFromWishlistAsync(destinationIdToRemove, userId);

            this.repository.Verify(r => r.Delete(userWishlist), Times.Once());
            this.repository.Verify(r => r.Update(userWishlist), Times.Once());
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());

            Assert.IsEmpty(userWishlist.Destinations);
        }

        [Test]
        public void RemoveFromWishlistAsync_WhenWishlistNotFound_ShouldThrowInvalidOperationException()
        {
            string userId = "user-no-wishlist";
            int destinationId = 1;
            var data = new List<PlaceToVisit>().AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(data);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() =>
                this.placeToVisitService.RemoveFromWishlistAsync(destinationId, userId));

            Assert.AreEqual("This destination is not in your wishlist", ex.Message);
        }

        [Test]
        public async Task RemoveFromWishlistAsync_WhenDestinationNotInSpecificWishlist_ShouldNotChangeListAndSaveChanges()
        {
            string userId = "user1";
            int destinationIdNotInList = 99;
            int existingDestinationId = 1;

            var existingDestination = new Destination { Id = existingDestinationId };
            var userWishlist = new PlaceToVisit
            {
                UserId = userId,
                Destinations = new List<Destination> { existingDestination }
            };
            var data = new List<PlaceToVisit> { userWishlist }.AsQueryable().BuildMock();

            this.repository.Setup(r => r.All<PlaceToVisit>()).Returns(data);
            this.repository.Setup(r => r.Update(userWishlist));
            this.repository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            await this.placeToVisitService.RemoveFromWishlistAsync(destinationIdNotInList, userId);

            this.repository.Verify(r => r.Update(userWishlist), Times.Exactly(2));
            this.repository.Verify(r => r.SaveChangesAsync(), Times.Once());

            Assert.AreEqual(1, userWishlist.Destinations.Count);
            Assert.IsTrue(userWishlist.Destinations.Any(d => d.Id == existingDestinationId));
        }
    }
}