//using Moq;
//using SportUnite.Domain;
//using SportUnite.Logic;
//using SportUnite.UI.Controllers;
//using Xunit;

//namespace SportUnite.UI.Tests {
//    public class BuildingControllerTests {

//        //index 

//        [Fact]
//        public void AddBuildingGetMethodFromControllerReturnsAddBuildingView() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);

//            //Act
//            var result = controller.AddBuilding();

//            //Assert
//            Assert.True(result.ViewName == "AddBuilding");
//        }

//        [Fact]
//        public void AddBuildingPostMethodFromControllerReturnsIndexViewIfSuccesful() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
 
//            mockManager.Setup(m => m.CreateBuilding(It.IsAny<Building>())).Returns(true);
           
//            //Act
//            var result = controller.AddBuilding(new Building());

//            //Assert
//            Assert.True(result.ViewName == "Index");
//        }

//        [Fact]
//        public void AddBuildingPostMethodFromControllerReturnsCurrentViewIfInvalid() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            controller.ModelState.AddModelError("key", "error message");

//            //Act
//            var result = controller.AddBuilding(new Building());

//            //Assert
//            Assert.True(result.ViewName == null);
//        }

//        [Fact]
//        public void AddBuildingPostMethodFromControllerReturnsCurrentViewIfFalse() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);

//            mockManager.Setup(m => m.CreateBuilding(It.IsAny<Building>())).Returns(false);

//            //Act
//            var result = controller.AddBuilding(new Building());

//            //Assert
//            Assert.True(result.ViewName == null);
//        }

//        [Fact]
//        public void UpdateBuildingGetMethodFromControllerReturnsCurrentViewWithBuildingObjectView() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            var buildingToTest = new Building();

//            mockManager.Setup(m => m.GetBuilding(It.IsAny<int>())).Returns(buildingToTest);

//            //Act
//            var result = controller.UpdateBuilding(0);

//            //Assert
//            Assert.True(result.ViewName == null);
//            Assert.True(result.Model == buildingToTest);
//        }

//        [Fact]
//        public void UpdateBuildingPostMethodFromControllerReturnsIndexViewIfSuccesful() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);

//            mockManager.Setup(m => m.UpdateBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = controller.UpdateBuilding(new Building());

//            //Assert
//            Assert.True(result.ViewName == "Index");
//        }


//        [Fact]
//        public void UpdateBuildingPostMethodFromControllerReturnsCurrentViewIfInvalid() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            controller.ModelState.AddModelError("key", "error message");

//            //Act
//            var result = controller.UpdateBuilding(new Building());

//            //Assert
//            Assert.True(result.ViewName == null);
//        }

//        [Fact]
//        public void UpdateBuildingPostMethodFromControllerReturnsCurrentViewIfFalse() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);

//            mockManager.Setup(m => m.UpdateBuilding(It.IsAny<Building>())).Returns(false);

//            //Act
//            var result = controller.UpdateBuilding(new Building());

//            //Assert
//            Assert.True(result.ViewName == null);
//        }

//        [Fact]
//        public void GetBuildingGetMethodFromControllerReturnsBuildingDetailViewWithBuildingObjectView() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            var buildingToTest = new Building();

//            mockManager.Setup(m => m.GetBuilding(It.IsAny<int>())).Returns(buildingToTest);

//            //Act
//            var result = controller.GetBuilding(0);

//            //Assert
//            Assert.True(result.ViewName == "BuildingDetail");
//            Assert.True(result.Model == buildingToTest);
//        }

//        [Fact]
//        public void DeleteBuildingPostMethodFromControllerReturnsIndexViewIfSuccesful() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            var buildingToTest = new Building();

//            mockManager.Setup(m => m.GetBuilding(It.IsAny<int>())).Returns(buildingToTest);
//            mockManager.Setup(m => m.RemoveBuilding(It.IsAny<Building>())).Returns(true);

//            //Act
//            var result = controller.DeleteBuilding(0);

//            //Assert
//            Assert.True(result.ViewName == "Index");
//        }

//        [Fact]
//        public void DeleteBuildingPostMethodFromControllerReturnsBuildingDetailViewIfFalse() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            var buildingToTest = new Building();

//            mockManager.Setup(m => m.GetBuilding(It.IsAny<int>())).Returns(buildingToTest);
//            mockManager.Setup(m => m.RemoveBuilding(It.IsAny<Building>())).Returns(false);

//            //Act
//            var result = controller.DeleteBuilding(0);

//            //Assert
//            Assert.True(result.ViewName == "BuildingDetail");
//            Assert.True(result.Model == buildingToTest);
//        }

//        [Fact]
//        public void AddHallGetMethodFromControllerReturnsCurrentView() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);

//            //Act
//            var result = controller.AddHall();

//            //Assert
//            Assert.True(result.ViewName == null);
//        }

//        [Fact]
//        public void UpdateHallPostMethodFromControllerReturnsIndexViewIfSuccesful() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);

//            mockManager.Setup(m => m.UpdateHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = controller.UpdateHall(new Hall());

//            //Assert
//            Assert.True(result.ViewName == "Index");
//        }


//        [Fact]
//        public void UpdateHallPostMethodFromControllerReturnsCurrentViewIfInvalid() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            controller.ModelState.AddModelError("key", "error message");

//            //Act
//            var result = controller.UpdateHall(new Hall());

//            //Assert
//            Assert.True(result.ViewName == null);
//        }

//        [Fact]
//        public void UpdateHallPostMethodFromControllerReturnsCurrentViewIfFalse() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);

//            mockManager.Setup(m => m.UpdateHall(It.IsAny<Hall>())).Returns(false);

//            //Act
//            var result = controller.UpdateHall(new Hall());

//            //Assert
//            Assert.True(result.ViewName == null);
//        }

//        [Fact]
//        public void GetHallGetMethodFromControllerReturnsHallDetailViewWithBuildingObjectView() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            var hallToTest = new Hall();

//            mockManager.Setup(m => m.GetHall(It.IsAny<int>())).Returns(hallToTest);

//            //Act
//            var result = controller.GetHall(0);

//            //Assert
//            Assert.True(result.ViewName == "HallDetail");
//            Assert.True(result.Model == hallToTest);
//        }
        
//        [Fact]
//        public void DeleteHallPostMethodFromControllerReturnsIndexViewIfSuccesful() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            var hallToTest = new Hall();

//            mockManager.Setup(m => m.GetHall(It.IsAny<int>())).Returns(hallToTest);
//            mockManager.Setup(m => m.RemoveHall(It.IsAny<Hall>())).Returns(true);

//            //Act
//            var result = controller.DeleteHall(0);

//            //Assert
//            Assert.True(result.ViewName == "Index");
//        }

//        [Fact]
//        public void DeleteHallPostMethodFromControllerReturnsBuildingDetailViewIfFalse() {
//            //Arrange
//            var mockManager = new Mock<IBuildingManager>();
//            var controller = new BuildingController(mockManager.Object);
//            var hallToTest = new Hall();

//            mockManager.Setup(m => m.GetHall(It.IsAny<int>())).Returns(hallToTest);
//            mockManager.Setup(m => m.RemoveHall(It.IsAny<Hall>())).Returns(false);

//            //Act
//            var result = controller.DeleteHall(0);

//            //Assert
//            Assert.True(result.ViewName == "HallDetail");
//            Assert.True(result.Model == hallToTest);
//        }
//    }
//}
