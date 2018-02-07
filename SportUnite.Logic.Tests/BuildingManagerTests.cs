//using Moq;
//using SportUnite.Domain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using SportUnite.Data.Abstract;
//using Xunit;

//namespace SportUnite.Logic.Tests {
//    public class BuildingManagerTests
//    {
//        [Fact]
//        public void GetAllBuildingsMethodFromManagerReturnsListWithBuildings()
//        {
//            //Arrange
//            var buildings = BuildingsList();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            mockBuildingRepo.Setup(r => r.Buildings()).Returns(buildings);

//            //Act
//            var buildingsFromManager = mockManager.Object.Buildings();

//            //Assert
//            Assert.Equal(buildingsFromManager, buildings);
//        }

//        [Fact]
//        public void GetAllAddressesMethodFromManagerReturnsListWithAddresses()
//        {
//            //Arrange
//            var addresses = AddressesList();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            mockBuildingRepo.Setup(r => r.Addresses()).Returns(addresses);

//            //Act
//            var addressesFromManager = mockManager.Object.Addresses();

//            //Assert
//            Assert.Equal(addressesFromManager, addresses);
//        }

//        [Fact]
//        public void CreateBuildingMethodFromManagerReturnsTrueIfNewEntry() {
//            //Arrange
//            var buildingToTest = BuildingToTest();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(m => m.CreateBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = mockManager.Object.CreateBuilding(buildingToTest);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void CreateBuildingMethodFromManagerReturnsFalseIfDuplicateEntry() {
//            //Arrange
//            var buildingToTest = BuildingToTest();
//            var buildings = new List<Building>() { buildingToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.Buildings()).Returns(buildings);
//            //BuildingManager checks if buildings contains a building with the identical values
//            mockBuildingRepo.Setup(r => r.CreateBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = mockManager.Object.CreateBuilding(buildingToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void UpdateBuildingMethodFromManagerSuccessfulIfBuildingExists() {
//            //Arrange
//            var buildingToTest = BuildingToTest();
//            var buildings = new List<Building>() { buildingToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            //buildingId 0 already exists in context
//            var updatedBuilding = new Building { BuildingId = 0, Name = "UpdatedBuilding" };

//            mockBuildingRepo.Setup(r => r.Buildings()).Returns(buildings);
//            //BuildingManager checks if buildings contains a building with the same BuildingId
//            mockBuildingRepo.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateBuilding(updatedBuilding);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void UpdateBuildingMethodFromManagerFailurelIfBuildingDoesNotExist() {
//            //Arrange
//            var buildings = new List<Building>() { };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            var updatedBuilding = new Building { BuildingId = 0, Name = "UpdatedBuilding" };

//            mockBuildingRepo.Setup(r => r.Buildings()).Returns(buildings);
//            //BuildingManager checks if buildings contains a building with the same BuildingId
//            mockBuildingRepo.Setup(r => r.UpdateBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateBuilding(updatedBuilding);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void UpdateAddressMethodFromManagerSuccessfulIfAddressExists() {
//            //Arrange
//            var addressToTest = AddressToTest();
//            var addresses = new List<Address>() { addressToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            //addressId 0 already exists in context
//            var updatedAddress = new Address { AddressId = 0, City = "UpdatedCity" };

//            mockBuildingRepo.Setup(r => r.Addresses()).Returns(addresses);
//            //BuildingManager checks if addresses contains a building with the same AddressId
//            mockBuildingRepo.Setup(r => r.UpdateAddress(It.IsAny<Address>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateAddress(updatedAddress);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void UpdateAddressMethodFromManagerFailureIfAddressDoesNotExist() {
//            //Arrange
//            var addresses = new List<Address>() { };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//           var updatedAddress = new Address { AddressId = 0, City = "UpdatedCity" };

//            mockBuildingRepo.Setup(r => r.Addresses()).Returns(addresses);
//            //BuildingManager checks if addresses contains a building with the same AddressId
//            mockBuildingRepo.Setup(r => r.UpdateAddress(It.IsAny<Address>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateAddress(updatedAddress);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void RemoveBuildingMethodFromManagerSuccessfulIfValidated() {
//            //Arrange
//            var buildingToTest = BuildingToTest();
//            var buildings = new List<Building>() { buildingToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.Buildings()).Returns(buildings);
//            //BuildingManager checks if buildings contains a building with the same BuildingId
//            mockBuildingRepo.Setup(r => r.RemoveBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveBuilding(buildingToTest);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void RemoveBuildingMethodFromManagerFailureIfDoesntExist() {
//            //Arrange
//            var buildingToTest = BuildingToTest();
//            var buildings = new List<Building>() { };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.Buildings()).Returns(buildings);
//            //BuildingManager checks if buildings contains a building with the same BuildingId
//            mockBuildingRepo.Setup(r => r.RemoveBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveBuilding(buildingToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void RemoveBuildingMethodFromManagerFailureIfReserved() {
//            //Arrange
//            var buildingToTest = BuildingToTest();
//            buildingToTest.Halls.ElementAt(1).Reservations.Add(new Reservation() {ReservationId = 1});
//            var buildings = new List<Building>() { buildingToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.Buildings()).Returns(buildings);
//            //BuildingManager checks if buildings contains a building with the same BuildingId & checks if any reservations are made for each hall
//            mockBuildingRepo.Setup(r => r.RemoveBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveBuilding(buildingToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void GetBuildingByBuildingIdMethodFromManagerReturnsBuildingIfExists() {
//            //Arrange
//            var buildingToTest = BuildingToTest();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.GetBuilding(0)).Returns(buildingToTest);

//            //Act
//            var buildingsFromManager = mockManager.Object.GetBuilding(0);

//            //Assert
//            Assert.Equal(buildingsFromManager, buildingToTest);
//        }

//        [Fact]
//        public void GetBuildingByBuildingIdMethodFromManagerReturnsNullIfDoesntExist() {
//            //Arrange
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //Act
//            var buildingFromManager = mockManager.Object.GetBuilding(0);

//            //Assert
//            Assert.Equal(buildingFromManager, null);
//        }

//        [Fact]
//        public void GetAddressByBuildingIdMethodFromManagerReturnsAddressIfExists() {
//            //Arrange
//            var addressToTest = AddressToTest();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.GetAddress(0)).Returns(addressToTest);

//            //Act
//            var addressFromManager = mockManager.Object.GetAddress(0);

//            //Assert
//            Assert.Equal(addressFromManager, addressToTest);
//        }

//        [Fact]
//        public void GetAddressByBuildingIdMethodFromManagerReturnsNullIfDoesntExist() {
//            //Arrange
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //Act
//            var addressFromManager = mockManager.Object.GetAddress(0);

//            //Assert
//            Assert.Equal(addressFromManager, null);
//        }

//        //------------- Hall/OpeningHours -------------
//        [Fact]
//        public void GetAllHallsMethodFromManagerReturnsListWithHalls() {
//            //Arrange
//            var halls = HallsList();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            mockBuildingRepo.Setup(r => r.Halls()).Returns(halls);

//            //Act
//            var buildingsFromManager = mockManager.Object.Halls();

//            //Assert
//            Assert.Equal(buildingsFromManager, halls);
//        }

//        [Fact]
//        public void GetAllOpeningHoursMethodFromManagerReturnsListWithOpeningHours() {
//            //Arrange
//            var openingHours = OpeningHoursList();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            mockBuildingRepo.Setup(r => r.OpeningHours()).Returns(openingHours);

//            //Act
//            var openingHoursFromManager = mockManager.Object.OpeningHours();

//            //Assert
//            Assert.Equal(openingHoursFromManager, openingHours);
//        }

//        [Fact]
//        public void GetAllHallOpeningHoursMethodFromManagerReturnsListWithHallOpeningHours() {
//            //Arrange
//            var hallOpeningHours = HallOpeningHoursList();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            mockBuildingRepo.Setup(r => r.HallOpeningHours()).Returns(hallOpeningHours);

//            //Act
//            var hallOpeningHoursFromManager = mockManager.Object.HallOpeningHours();

//            //Assert
//            Assert.Equal(hallOpeningHoursFromManager, hallOpeningHours);
//        }

//        [Fact]
//        public void CreateHallMethodFromManagerReturnsTrueIfNewEntryAndValidated() {
//            //Arrange
//            var hallToTest = HallToTest();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(m => m.CreateHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.CreateHall(hallToTest);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void CreateHallMethodFromManagerReturnsFalseIfNoOpeningHours() {
//            //Arrange
//            var hallToTest = HallToTest();
//            hallToTest.HallOpeningHours = new List<HallOpeningHours>();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.CreateHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.CreateHall(hallToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void CreateOpeningHoursMethodFromManagerReturnsTrueIfHallExists() {
//            //Arrange
//            var openingHours = OpeningHoursList();
//            var hallToTest = HallToTest();
//            hallToTest.HallOpeningHours.Add(new HallOpeningHours() {HallId = hallToTest.HallId, OpeningHours = openingHours.ElementAt(0)});
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(m => m.CreateOpeningHours(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.CreateOpeningHours(hallToTest);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void CreateOpeningHoursMethodFromManagerReturnsFalseIfHallAlreadyHasOpeningHours() {
//            //Arrange
//            var hallToTest = new Hall(){HallId = 0};

//            var openingHours = new OpeningHours() {OpeningHoursId = 0};
//            var openingHoursList = new List<OpeningHours>() {openingHours};

//            var hallOpeningHour = new HallOpeningHours() {Hall = hallToTest, OpeningHours = openingHours };
//            var hallOpeningHourList = new List<HallOpeningHours>() {hallOpeningHour};
    
//            hallToTest.HallOpeningHours = hallOpeningHourList;
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.HallOpeningHours()).Returns(hallOpeningHourList);
//            mockBuildingRepo.Setup(r => r.OpeningHours()).Returns(openingHoursList);
//            mockBuildingRepo.Setup(r => r.CreateOpeningHours(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.CreateOpeningHours(hallToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void UpdateHallMethodFromManagerSuccessfulIfHallExists() {
//            //Arrange
//            var hallToTest = HallToTest();
//            var halls = new List<Hall>() { hallToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            //hallId 0 already exists in context
//            var updatedHall = new Hall { HallId = 0, Name = "UpdatedHall" };

//            mockBuildingRepo.Setup(r => r.Halls()).Returns(halls);
//            //BuildingManager checks if halls contains a Hall with the same HallId
//            mockBuildingRepo.Setup(r => r.UpdateHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateHall(updatedHall);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void UpdateHallMethodFromManagerFailureIfHallDoesNotExist() {
//            //Arrange
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            var updatedHall = new Hall { HallId = 0, Name = "UpdatedHall" };

//            mockBuildingRepo.Setup(r => r.UpdateHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateHall(updatedHall);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void UpdateOpeningHoursMethodFromManagerSuccessfulIfOpeningHoursExists() {
//            //Arrange 
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //dummy Hall so it already exists in context when testing later
//            var dummyHall = new Hall() { HallId = 0 };
//            var dummyOpeningHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday"};
//            var dummyOpeningHoursList = new List<OpeningHours>() {dummyOpeningHours};
        
//            var dummyHallOpeningHours = new HallOpeningHours() { Hall = dummyHall, OpeningHours = dummyOpeningHours };
//            var dummyHallOpeningHoursList = new List<HallOpeningHours>() {dummyHallOpeningHours};
//            dummyHall.HallOpeningHours = dummyHallOpeningHoursList;

//            //Actual Hall that will be tested with updated OpeningHours
//            var hallToTest = new Hall() { HallId = 0 };
//            var openingHours = new OpeningHours() { OpeningHoursId = 0, Day = "Friday" };
//            var openingHoursList = new List<OpeningHours>() {openingHours};
       
//            var hallOpeningHours = new HallOpeningHours() { Hall = hallToTest, OpeningHours = openingHours };
//            var hallOpeningHoursList = new List<HallOpeningHours>() {hallOpeningHours};
//            hallToTest.HallOpeningHours = hallOpeningHoursList;

//             mockBuildingRepo.Setup(r => r.HallOpeningHours()).Returns(dummyHallOpeningHoursList);
//            mockBuildingRepo.Setup(r => r.OpeningHours()).Returns(dummyOpeningHoursList);
//            mockBuildingRepo.Setup(r => r.UpdateOpeningHours(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateOpeningHours(hallToTest);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void UpdateOpeningHoursMethodFromManagerReturnsFalseIfOpeningHoursAreNotChanged() {
//            //Arrange 
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //dummy Hall so it already exists in context when testing later
//            var dummyHall = new Hall() { HallId = 0 };
//            var dummyOpeningHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday" };
//            var dummyOpeningHoursList = new List<OpeningHours>() { dummyOpeningHours };

//            var dummyHallOpeningHours = new HallOpeningHours() { Hall = dummyHall, OpeningHours = dummyOpeningHours };
//            var dummyHallOpeningHoursList = new List<HallOpeningHours>() {dummyHallOpeningHours};
//            dummyHall.HallOpeningHours = dummyHallOpeningHoursList;

//            //Actual Hall that will be tested with the same OpeningHours
//            var hallToTest = new Hall() { HallId = 0 };
//            var openingHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday" };
     
  
//            var hallOpeningHours = new HallOpeningHours() { Hall = hallToTest, OpeningHours = openingHours };
//            var hallOpeningHoursList = new List<HallOpeningHours>() {hallOpeningHours};
//            hallToTest.HallOpeningHours = hallOpeningHoursList;

//            mockBuildingRepo.Setup(r => r.HallOpeningHours()).Returns(dummyHallOpeningHoursList);
//            mockBuildingRepo.Setup(r => r.OpeningHours()).Returns(dummyOpeningHoursList);
//            mockBuildingRepo.Setup(r => r.UpdateOpeningHours(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.UpdateOpeningHours(hallToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void RemoveHallMethodFromManagerSuccessfulIfHallExistsWithoutReservations() {
//            //Arrange
//            var hallToTest = HallToTest();
//            var halls = new List<Hall>() { hallToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
  
//            mockBuildingRepo.Setup(r => r.Halls()).Returns(halls);
//            mockBuildingRepo.Setup(r => r.RemoveHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveHall(hallToTest);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void RemoveHallMethodFromManagerFailureIfHallDoesNotExist() {
//            //Arrange
//            var hallToTest = HallToTest();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.RemoveHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveHall(hallToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void RemoveHallMethodFromManagerFailureIfHallExistsWithReservations() {
//            //Arrange
//            var hallToTest = HallToTest();
//            hallToTest.Reservations.Add(new Reservation() {ReservationId = 0});
//            var halls = new List<Hall>() { hallToTest };
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.Halls()).Returns(halls);
//            mockBuildingRepo.Setup(r => r.RemoveHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveHall(hallToTest);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void RemoveOpeningHoursMethodFromManagerSuccessfulIfOpeningHoursExists() {
//            //Arrange 
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            var openingHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday" };
//            var openingHoursList = new List<OpeningHours>() {openingHours};

//            mockBuildingRepo.Setup(r => r.OpeningHours()).Returns(openingHoursList);
//            mockBuildingRepo.Setup(r => r.RemoveOpeningHours(It.IsAny<OpeningHours>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveOpeningHours(openingHours);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void RemoveOpeningHoursMethodFromManagerSuccessfulIfOpeningHoursDoesNotExist() {
//            //Arrange 
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);
//            var openingHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday" };

//            mockBuildingRepo.Setup(r => r.RemoveOpeningHours(It.IsAny<OpeningHours>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveOpeningHours(openingHours);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void RemoveHallOpeningHoursMethodFromManagerSuccessfulIfStillOpeningHoursLeftForHall() {
//            //Arrange 
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //setting up Hall with multiple OpeningHours
//            var hallToTest = new Hall() { HallId = 0 };
//            var openingHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday" };
//            var openingHours2 = new OpeningHours() { OpeningHoursId = 1, Day = "Friday" };
    
//            //setting up connection between Hall and OpeningHours
//            var hallOpeningHours = new HallOpeningHours() { Hall = hallToTest, OpeningHours = openingHours };
//            var hallOpeningHours2 = new HallOpeningHours() { Hall = hallToTest, OpeningHours = openingHours2 };
//            var hallOpeningHoursList = new List<HallOpeningHours>() {hallOpeningHours, hallOpeningHours2 };
        
//            mockBuildingRepo.Setup(r => r.HallOpeningHours()).Returns(hallOpeningHoursList);
//            mockBuildingRepo.Setup(r => r.RemoveHallOpeningHours(It.IsAny<HallOpeningHours>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveHallOpeningHours(hallOpeningHours);

//            //Assert
//            Assert.True(result);
//        }

//        [Fact]
//        public void RemoveHallOpeningHoursMethodFromManagerFaiureIfDoesNotExist() {
//            //Arrange 
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //setting up Hall with multiple OpeningHours
//            var hallToTest = new Hall() { HallId = 0 };
//            var openingHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday" };

//            //setting up connection between Hall and OpeningHours
//            var hallOpeningHours = new HallOpeningHours() { Hall = hallToTest, OpeningHours = openingHours };
            
//            mockBuildingRepo.Setup(r => r.RemoveHallOpeningHours(It.IsAny<HallOpeningHours>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveHallOpeningHours(hallOpeningHours);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void RemoveHallOpeningHoursMethodFromManagerFailureIfOnlyOpeningHoursForHall() {
//            //Arrange 
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //setting up Hall with multiple OpeningHours
//            var hallToTest = new Hall() { HallId = 0 };
//            var openingHours = new OpeningHours() { OpeningHoursId = 0, Day = "Monday" };
      
//            //setting up connection between Hall and OpeningHours
//            var hallOpeningHours = new HallOpeningHours() { Hall = hallToTest, OpeningHours = openingHours };
//            var hallOpeningHoursList = new List<HallOpeningHours>() { hallOpeningHours };

//            mockBuildingRepo.Setup(r => r.HallOpeningHours()).Returns(hallOpeningHoursList);
//            mockBuildingRepo.Setup(r => r.RemoveHallOpeningHours(It.IsAny<HallOpeningHours>())).Returns(true);

//            //Act
//            var result = mockManager.Object.RemoveHallOpeningHours(hallOpeningHours);

//            //Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void GetHallByHallIdMethodFromManagerReturnsHallIfExists() {
//            //Arrange
//            var hallToTest = HallToTest();
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            mockBuildingRepo.Setup(r => r.GetHall(0)).Returns(hallToTest);

//            //Act
//            var hallsFromManager = mockManager.Object.GetHall(0);

//            //Assert
//            Assert.Equal(hallsFromManager, hallToTest);
//        }

//        [Fact]
//        public void GetHallByHallIdMethodFromManagerReturnsNullIfDoesntExist() {
//            //Arrange
//            var mockBuildingRepo = new Mock<IBuildingRepository>();
//            var mockManager = new Mock<EFBuildingManager>(mockBuildingRepo.Object);

//            //Act
//            var hallsFromManager = mockManager.Object.GetHall(0);

//            //Assert
//            Assert.Equal(hallsFromManager, null);
//        }

//        //--------- DATASEEDING ---------
//        private static IEnumerable<Building> BuildingsList()
//        {
//            var buildings = new List<Building>();
//            for (int i = 1; i <= 5; i++)
//            {
//                var building = new Building {Address = Address(i), Halls = HallsList(), Name = "Building " + i};
//                buildings.Add(building);
//            }
//            return buildings;
//        }

//        private static List<Hall> HallsList()
//        {
//            var halls = new List<Hall>();
//            for (int i = 1; i <= 5; i++)
//            {
//                var hall = new Hall
//                {
//                    Name = "Hall " + i,
//                    Size = "Size",
//                    Price = 10.00,
//                    Available = true, 
//                    HallOpeningHours = HallOpeningHoursList(),
//                    Reservations = new List<Reservation>()
//                };
//                halls.Add(hall);
//            }
//            return halls;
//        }

//       private static List<Address> AddressesList() {
//            var addresses = new List<Address>();
//            for (int i = 1; i <= 5; i++) {
//                var address = new Address {
//                    City = "City " + i,
//                    HouseNumber = 51,
//                    ZipCode = "4815AA",
//                    StreetName = "Sportstraat",
//                    Country = "Nederland",
//                    State = "Noord-Brabant",
//                };
//                addresses.Add(address);
//            }
//            return addresses;
//        }

//        //dataseeding 5 Halls with each 1 OpeningHours
//        private static List<HallOpeningHours> HallOpeningHoursList() {
//            var hallOpeningHoursList = new List<HallOpeningHours>();
//            for (int i = 1; i <= 5; i++) {
//                var openingHours = new OpeningHours {
//                    Day = "Maandag",
//                    ClosingTime = default(DateTime).AddHours(18 + i),
//                    OpeningTime = default(DateTime).AddHours(10)
//                };

//                hallOpeningHoursList.Add(new HallOpeningHours() {HallId = i-1, OpeningHours = openingHours});
//            }
//            return hallOpeningHoursList;
//        }


//        private static List<OpeningHours> OpeningHoursList() {
//            var openingHoursList = new List<OpeningHours>();
//            for (int i = 1; i <= 5; i++) {
//                var openingHours = new OpeningHours {
//                    Day = "Maandag",
//                    ClosingTime = default(DateTime).AddHours(18 + i),
//                    OpeningTime = default(DateTime).AddHours(10)
//                };
//                openingHoursList.Add(openingHours);
//            }
//            return openingHoursList;
//        }

//        public static Address Address(int i)
//        {
//            var testAddress = new Address
//            {
//                City = "City " + i,
//                HouseNumber = 51,
//                ZipCode = "4815AA",
//                StreetName = "Sportstraat",
//                Country = "Nederland",
//                State = "Noord-Brabant"
//            };
//            return testAddress;
//        }

//        public static Building BuildingToTest()
//        {
//            var buildingToTest = new Building { Address = Address(1), Halls = HallsList(), Name = "Building " + 1};
//            return buildingToTest;
//        }

//        public static Address AddressToTest() {
//            var addressToTest = new Address {
//                City = "City 1",
//                HouseNumber = 51,
//                ZipCode = "4815AA",
//                StreetName = "Sportstraat",
//                Country = "Nederland",
//                State = "Noord-Brabant"
//            };
//            return addressToTest;
//        }

//        private static Hall HallToTest() {
//            var hallToTest = new Hall {
//                Name = "Hall 1",
//                Size = "Size",
//                Price = 10.00,
//                Available = true,
//                HallOpeningHours = HallOpeningHoursList(),
//                Reservations = new List<Reservation>()
//            };
//            return hallToTest;
//        }
//    }
//}
