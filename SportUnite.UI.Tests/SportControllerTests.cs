using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PagedList.Core;
using SportUnite.Data.Abstract;
using SportUnite.Domain;
using SportUnite.Logic;
using SportUnite.UI.Controllers;
using SportUnite.UI.ViewModels;
using Xunit;

namespace SportUnite.UI.Tests
{
    public class SportControllerTests
    {
        [Fact]
        public void IndexMethodWithSearchStringReturnsCorrectCollectionOfSports()
        {
            //Arrange
            var sports = new[]
            {
                new Sport {Name = "Voetbal"},
                new Sport {Name = "Basketbal"},
                new Sport {Name = "Hockey"},
                new Sport {Name = "Volleybal"}
            };
            var mockManager = new Mock<ISportManager>();
            mockManager.Setup(x => x.SportsList()).Returns(sports);
            var controller = new SportController(mockManager.Object);

            //Act
            var result = controller.Index(null, null, "bal", 1);

            //Assert
            Assert.IsType(typeof(PagedList<Sport>), result.ViewData.Model);
            Assert.Equal(3, result.ViewData.Count);
        }

        [Fact]
        public void AmountOfPagesOnIndexMethodIsCorrect()
        {
            //Arrange
            var sports = new[]
            {
                new Sport{ Name = "Voetbal"},
                new Sport{Name = "Basketbal"},
                new Sport{Name = "Hockey"},
                new Sport{ Name = "Volleybal"},
                new Sport{Name = "Trefbal"},
                new Sport{Name = "Tennis"},
                new Sport{Name = "Darten"},
                new Sport{Name = "Karate"},
                new Sport{Name = "Dansen"},
                new Sport{Name = "Yoga"},
                new Sport{Name = "Badminton"},
                new Sport{Name = "Softbal"}
            };

            var mockManager = new Mock<ISportManager>();
            var controller = new SportController(mockManager.Object);

            mockManager.Setup(a => a.SportsList()).Returns(sports);

            //Act
            var result = controller.Index(null, null, null, 1);
            var pageList = (PagedList<Sport>)result.ViewData.Model;

            //Assert
            Assert.Equal(2, pageList.PageCount);
            Assert.Equal(12, pageList.TotalItemCount);
        }
        
        [Fact]
        public void IndexMethodWithSortOrderDescendingReturnsCorrectCollectionOfSportsSortedDescending()
        {
            //Arrange
            var sports = new[]
            {
                new Sport {Name = "Voetbal"},
                new Sport {Name = "Basketbal"},
                new Sport {Name = "Hockey"},
                new Sport {Name = "Volleybal"}
            };
            var mockManager = new Mock<ISportManager>();
            var controller = new SportController(mockManager.Object);


            mockManager.Setup(a => a.SportsList()).Returns(sports);

            //Act
            var result = controller.Index("name_desc", null, null, 1);
            var pageList = (PagedList<Sport>)result.ViewData.Model;

            //Assert
            Assert.IsType(typeof(PagedList<Sport>), result.ViewData.Model);
            Assert.Same("Volleybal", pageList[0].Name);
        }

        [Fact]
        public void TestAddSportReturnsToIndexAction()
        {
            //Arrange
            var dummysport = new Sport
            {
                Name = "KongBall",
            };

            var sports = new[]
            {
                new Sport {Name = "Voetbal"},
                new Sport {Name = "Basketbal"},
                new Sport {Name = "Hockey"},
                new Sport {Name = "Volleybal"}
            };

            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };

            mockManager.Setup(x => x.AddSport(It.IsAny<Sport>())).Throws<Exception>();

            //Act
            var result = controller.Add(dummysport);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
        }

        [Fact]
        public void TestAddInvalidSportReturnsCurrentView()
        {
            //Arrange
            var dummysport = new Sport
            {
                Name = "KongBall",
            };



            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };
            controller.ModelState.AddModelError("", "");

            mockManager.Setup(x => x.AddSport(It.IsAny<Sport>())).Throws<Exception>();

            //Act
            var result = (ViewResult)controller.Add(dummysport);

            //Assert
            Assert.True(result.ViewName == "Add");
        }

        [Fact]
        public void TestUpdateSportReturnsToIndexMethod()
        {
            //Arrange
            var dummysport = new Sport
            {
                Name = "KongBall",
            };

            var sports = new[]
            {
                new Sport {Name = "Voetbal"},
                new Sport {Name = "Basketbal"},
                new Sport {Name = "Hockey"},
                new Sport {Name = "Volleybal"}
            };

            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };

            mockManager.Setup(x => x.UpdateSport(It.IsAny<Sport>())).Throws<Exception>();

            //Act
            var result = controller.Update(dummysport);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
        }

        [Fact]
        public void TestUpdateInvalidSporReturnsCurrentView()
        {
            //Arrange
            var dummysport = new Sport
            {
                Name = "KongBall",
            };

            var sports = new[]
            {
                new Sport {Name = "Voetbal"},
                new Sport {Name = "Basketbal"},
                new Sport {Name = "Hockey"},
                new Sport {Name = "Volleybal"}
            };

            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };
            controller.ModelState.AddModelError("", "");

            mockManager.Setup(x => x.UpdateSport(It.IsAny<Sport>())).Throws<Exception>();

            //Act
            var result = (ViewResult)controller.Update(dummysport);

            //Assert
            Assert.True(result.ViewName == "Update");
        }

        [Fact]
        public void TestDeleteSportReturnsToIndexMethod()
        {
            //Arrange
            var sports = new[]
            {
                new Sport {Name = "Voetbal", SportId = 1},
                new Sport {Name = "Basketbal", SportId = 2},
                new Sport {Name = "Hockey", SportId = 3},
                new Sport {Name = "Volleybal", SportId = 4},
            };

            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };

            mockManager.Setup(x => x.DeleteSport(It.IsAny<Sport>())).Throws<Exception>();
            mockManager.Setup(x => x.Read(It.IsAny<int>())).Returns(sports[1]);

            //Act
            var result = controller.Delete(sports[1].SportId);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
        } 

        [Fact]
        public void TestAddSportObjectReturnsToIndexAction()
        {

            //Arrange

            var model = new SportObjectModel
            {
                Sport = new Sport { SportId = 1, Name = "testsport" },

                SportObject = new SportObject { SportObjectId = 1, Name = "TestOBJECT" }

            };

            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };

            mockManager.Setup(x => x.AddSportObject(It.IsAny<Sport>(), It.IsAny<SportObject>())).Throws<Exception>();

            //Act
            var result = controller.AddObject(model, 1);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
        }
        //
        [Fact]
        public void TestAddInvalidSportObjectReturnsCurrentView()
        {
            //Arrange

            var model = new SportObjectModel
            {
                Sport = new Sport { SportId = 1, Name = "testsport" },

                SportObject = new SportObject { SportObjectId = 3, Name = "TestOBJECT" }

            };
            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };
            controller.ModelState.AddModelError("", "");
            mockManager.Setup(x => x.AddSportObject(It.IsAny<Sport>(), It.IsAny<SportObject>())).Throws<Exception>();

            //Act
            var result = (ViewResult)controller.AddObject(model, 1);

            //Asert
            Assert.True(result.ViewName == "AddObject");
        }
        //
        [Fact]
        public void TestUpdateSportObjectRedirectsToIndexAction()
        {
            //Arrange

            var model = new SportObjectModel
            {
                Sport = new Sport { SportId = 1, Name = "testsport" },

                SportObject = new SportObject { SportObjectId = 3, Name = "TestOBJECT" }

            };
            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };

            mockManager.Setup(x => x.UpdateSportObject(It.IsAny<SportObject>())).Throws<Exception>();

            var objectToTest = new SportObject
            {
                SportObjectId = 3,
                Name = "Test"

            };

            //Act
            var result = controller.UpdateObject(objectToTest);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
        }
        //
        [Fact]
        public void TestUpdateInvalidSportObjectRedirectsToIndexAction()
        {
            var model = new SportObjectModel
            {
                Sport = new Sport { SportId = 1, Name = "testsport" },

                SportObject = new SportObject { SportObjectId = 3, Name = "TestOBJECT" }

            };
            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };
            controller.ModelState.AddModelError("","");
            mockManager.Setup(x => x.UpdateSportObject(It.IsAny<SportObject>())).Throws<Exception>();

            var objectToTest = new SportObject
            {
                SportObjectId = 3,
                Name = "Test"

            };

            //Act
            var result = (ViewResult)controller.UpdateObject(objectToTest);

            //Assert
            Assert.True(result.ViewName=="UpdateObject");
        }

        [Fact]
        public void TestDeleteSportObjectReturnsToIndexMethod()
        {
            //Arrange
            var sportObjects = new[]
            {
                new SportObject {Name = "testobject 1"},
                new SportObject {Name = "testobject 2"},
                new SportObject {Name = "testobject 3"},
                new SportObject {Name = "testobject 4"},
            };
            var mockManager = new Mock<ISportManager>();
            var mockTempData = new Mock<ITempDataDictionary>();
            var controller = new SportController(mockManager.Object)
            {
                TempData = mockTempData.Object
            };

            mockManager.Setup(x => x.DeleteSportObject(It.IsAny<SportObject>())).Throws<Exception>();
            mockManager.Setup(x => x.ReadSportObject(It.IsAny<int>())).Returns(sportObjects[1]);
            //Act
            var results = controller.DeleteObject(sportObjects[1].SportObjectId);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), results);
        }
      
    }
}
