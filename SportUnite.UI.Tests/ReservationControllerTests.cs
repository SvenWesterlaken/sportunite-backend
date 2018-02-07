using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using PagedList.Core;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using SportUnite.Data.Sportevent.Abstract;
using SportUnite.Domain;
using SportUnite.Logic;
using SportUnite.UI.Controllers;
using SportUnite.UI.Models.ViewModels;
using Xunit;

namespace SportUnite.UI.Tests
{
    public class ReservationControllerTests
    {
        [Fact]
        public void IndexMethodWithSearchStringReturnsCorrectCollectionOfReservations()
        {
            //arrange 
            var reservations = new[]
            {
                new Reservation {SportEvent = new SportEvent {Name = "Potje voetballen"}},
                new Reservation {SportEvent = new SportEvent {Name = "Basketballen voor gevordenen"}},
                new Reservation {SportEvent = new SportEvent {Name = "Lessen hockey"}}
            };

            var reservationMockManager = new Mock<ISportEventManager>();
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();

            reservationMockManager.Setup(x => x.Reservations()).Returns(reservations);
            var controller = new ReservationController(reservationMockManager.Object, hallMockManager.Object,
                buildingMockManager.Object);


            //act
            var result = controller.Index(null, null, "ball", 1) as ViewResult;
            var model = (IEnumerable<Reservation>) result?.Model;
            var countModelsInViewModels = model?.Count();



            //assert
            Assert.IsType(typeof(PagedList<Reservation>), result.ViewData.Model);
            Assert.Equal(2, countModelsInViewModels);
        }

        [Fact]
        public void ReservationListViewContainsFullList()
        {
            //arrange
            var reservations = Reservations();
            var mockRepository = new Mock<ISportEventRepository>();

            var manager = new SportEventManager(mockRepository.Object);
            mockRepository.Setup(r => r.GetAllReservations()).Returns(reservations);

            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();

            var reservationController =
                new ReservationController(manager, hallMockManager.Object, buildingMockManager.Object);

            //act 
            var viewResult = (ViewResult) reservationController.Index("", "", "", 1);

            //assert

            Assert.Equal(viewResult.Model, reservations);

        }

        [Fact]
        public void AmountOfReservationPagesOnIndexMethodIsCorrect()
        {


            //arrange
            var reservations = Reservations();
            var mockRepository = new Mock<ISportEventRepository>();

            var manager = new SportEventManager(mockRepository.Object);
            mockRepository.Setup(r => r.GetAllReservations()).Returns(reservations);
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();

            var reservationController =
                new ReservationController(manager, hallMockManager.Object, buildingMockManager.Object);

            //act 
            var viewResult = (ViewResult) reservationController.Index("", "", "", 1);
            var pageList = (PagedList<Reservation>) viewResult.ViewData.Model;

            //assert

            Assert.Equal(viewResult.Model, reservations);
            Assert.Equal(1, pageList.PageCount);
            Assert.Equal(3, pageList.TotalItemCount);


        }


        [Fact]
        public void IndexMethodWithSortOrderDescendingReturnsCorrectCollectionOfReservationsSortedDescending()
        {
            //Arrange
            var reservations = Reservations();
            var mockManager = new Mock<ISportEventManager>();
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();

            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object);


            mockManager.Setup(a => a.Reservations()).Returns(reservations);

            //Act
            var result = controller.Index("default", null, null, 1) as ViewResult;
            var pageList = (PagedList<Reservation>) result.ViewData.Model;

            //Assert
            Assert.IsType(typeof(PagedList<Reservation>), result.ViewData.Model);
            Assert.Equal(DateTime.Today.AddDays(2), pageList[0].StartTime);
        }

        [Fact]

        public void TestReservationFailAndThrowsExceptionAndReturnsActionResult()
        {
            var dummyReservation = new Reservation
            {
                SportEventId = 3,
                HallId = 1,
                StartTime = DateTime.Today.AddHours(10),
                TimeFinish = DateTime.Today.AddHours(12),
                Definite = true,
                Invoice = new Invoice {DateTime = DateTime.Today.AddDays(1), Price = 10.00},
                ReservationId = 1

            };
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var mockManager = new Mock<ISportEventManager>();


            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object)
                {
                    TempData = tempData.Object
                };


            mockManager.Setup(r => r.GetReservation(1)).Returns(dummyReservation);
            mockManager.Setup(r => r.DeleteReservation(It.IsAny<Reservation>())).Throws<Exception>();

            //Act
            var result = controller.DeleteReservation(dummyReservation.ReservationId);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), result);

        }

        [Fact]

        public void TestDeleteReservationSuccesAndReturnsToIndexMethod()
        {
            var dummyReservation = new Reservation
            {
                SportEventId = 3,
                HallId = 1,
                StartTime = DateTime.Today.AddHours(10),
                TimeFinish = DateTime.Today.AddHours(12),
                Definite = true,
                ReservationId = 1

            };
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var mockManager = new Mock<ISportEventManager>();


            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object)
                {
                    TempData = tempData.Object
                };


            mockManager.Setup(r => r.GetReservation(1)).Returns(dummyReservation);
            mockManager.Setup(r => r.DeleteReservation(It.IsAny<Reservation>())).Returns(true);

            //Act
            var result = controller.DeleteReservation(dummyReservation.ReservationId);

            //Assert
            Assert.IsType(typeof(RedirectToActionResult), result);
        }

        [Fact]
        public void ChooseUpdateReservationViewSuccess()
        {
            //arrange
            var reservation = new Reservation()
            {
                SportEventId = 3,
                HallId = 1,
                StartTime = DateTime.Today.AddHours(10),
                TimeFinish = DateTime.Today.AddHours(12),
                Definite = true,
                Invoice = new Invoice {DateTime = DateTime.Today.AddDays(1), Price = 10.00},
                ReservationId = 1
            };

            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var mockManager = new Mock<ISportEventManager>();

            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object);

            mockManager.Setup(r => r.GetReservation(1)).Returns(reservation);
       
            //act

            var result = controller.ChooseUpdateReservation(1) as ViewResult;

            //assert


            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ReservationUpdateModel>(
                viewResult.ViewData.Model);
           
        

        }

        [Fact]

        public void UpdateReservationHallActionReturnsFilledReservation()
        {
          
            //arrange
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var mockManager = new Mock<ISportEventManager>();

            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object);

            var model = new ReservationHallsModel
            {
                HallId = 1,
                EndTime = Reservations().First().TimeFinish,
                StartTime = Reservations().First().StartTime,
                ReservationId = Reservations().First().ReservationId

            };

            mockManager.Setup(x => x.GetReservation(model.ReservationId)).Returns(Reservations().First());
            hallMockManager.Setup(x => x.GetHallReservation(1)).Returns(new Hall {HallId = 1, BuildingId = 2});
            buildingMockManager.Setup(x => x.GetBuilding(2)).Returns(new Building {BuildingId = 2});
            //act
            var result = controller.UpdateReservationHall(model);


            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ReservationModel>(
                viewResult.ViewData.Model);
            Assert.Equal(viewResult.ViewName, "UpdateReservation");
        
        }

        [Fact]
        public void UpdateReservationReturnsRightView()
        {
            //arrange
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var mockManager = new Mock<ISportEventManager>();

            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object);

            var reservationModel = new ReservationModel
            {
                Reservation = new Reservation
                {
                    Definite = true,
                    HallId = 2,
                    StartTime = DateTime.Now.AddDays(1).AddHours(1),
                    TimeFinish = DateTime.Now.AddDays(1).AddHours(3),
                    SportEventId = 3,
                    ReservationId = 15

                }
            };

            mockManager.Setup(x => x.GetReservation(1)).Returns(reservationModel.Reservation);

            hallMockManager.Setup(r => r.GetHallReservation(2)).Returns(new Hall { HallId = 2, BuildingId = 1 });

            buildingMockManager.Setup(r => r.GetBuilding(1)).Returns(new Building { BuildingId = 1 });

            //act
            var result = controller.UpdateReservation(1);


            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ReservationModel>(
                viewResult.ViewData.Model);
           

        }

     

        [Fact]
        public void TestUpdateReservationGetRequestsReturnsViewWithReservationModel()
        {
            //Arrange
            var reservation = new Reservation()
            {
                SportEventId = 3,
                HallId = 1,
                StartTime = DateTime.Today.AddHours(10),
                TimeFinish = DateTime.Today.AddHours(12),
                Definite = true,
                Invoice = new Invoice { DateTime = DateTime.Today.AddDays(1), Price = 10.00 },
                ReservationId = 1
            };

            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var mockManager = new Mock<ISportEventManager>();

            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object);


            mockManager.Setup(sm => sm.GetReservation(It.IsAny<int>())).Returns(reservation);
            hallMockManager.Setup(r => r.GetHallReservation(1)).Returns(new Hall { HallId = 1, BuildingId = 1 });

            buildingMockManager.Setup(r => r.GetBuilding(1)).Returns(new Building { BuildingId = 1 });



            //Act
            var result = controller.UpdateReservation(1) as ViewResult;
            var model = result.Model as ReservationModel;

            //Assert
            Assert.IsType(typeof(ViewResult), result);

            Assert.Equal(reservation, model.Reservation);
        }

      

       [Fact]
          public void ReservationUpdateResultSuccess()
          {

            //arrange
              var buildingMockManager = new Mock<IBuildingManager>();
              var hallMockManager = new Mock<IHallManager>();
              var manager = new Mock<ISportEventManager>();

              var reservationModel = new ReservationModel
              {
                  Reservation = new Reservation
                  {
                      Definite = true,
                      HallId = 2,
                      StartTime = DateTime.Now.AddDays(1).AddHours(1),
                      TimeFinish = DateTime.Now.AddDays(1).AddHours(3),
                      SportEventId = 3,
                      ReservationId = 15

                  }
              };

              Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
              var reservationController = new ReservationController(manager.Object, hallMockManager.Object, buildingMockManager.Object)
              {
                  TempData = tempData.Object
              };


              manager.Setup(r => r.UpdateReservation(reservationModel.Reservation)).Returns(true);

              hallMockManager.Setup(r => r.GetHallReservation(2)).Returns(new Hall { HallId = 2, BuildingId = 1 });

              buildingMockManager.Setup(r => r.GetBuilding(1)).Returns(new Building { BuildingId = 1 });
            //act 
            var viewResult =  reservationController.UpdateReservation(reservationModel) as RedirectToActionResult;


              //assert
             
              
              Assert.IsType<RedirectToActionResult>(viewResult);
              Assert.Equal("Index", (viewResult).ActionName);

          }


        [Fact]
        public void ReservationUpdateResultFail()
        {

            //arrange
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var manager = new Mock<ISportEventManager>();

            var reservationModel = new ReservationModel
            {
                Reservation = new Reservation
                {
                    Definite = true,
                    HallId = 2,
                    StartTime = DateTime.Now.AddDays(1).AddHours(1),
                    TimeFinish = DateTime.Now.AddDays(1).AddHours(3),
                    SportEventId = 3,
                    ReservationId = 15

                }
            };

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var reservationController = new ReservationController(manager.Object, hallMockManager.Object, buildingMockManager.Object)
            {
                TempData = tempData.Object
            };

            hallMockManager.Setup(r => r.GetHallReservation(2)).Returns(new Hall {HallId = 2, BuildingId = 1});

            buildingMockManager.Setup(r => r.GetBuilding(1)).Returns(new Building {BuildingId = 1});
            manager.Setup(r => r.UpdateReservation(reservationModel.Reservation)).Returns(false);
            //act 
            var viewResult = reservationController.UpdateReservation(reservationModel) as ViewResult;


            //assert


            Assert.IsType<ViewResult>(viewResult);
          
            Assert.IsAssignableFrom<ReservationModel>(
                viewResult.ViewData.Model);

        }

        [Fact]
        public void AddReservationGetMethodReturnsModel()
        {
            //Arrange
         
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var mockManager = new Mock<ISportEventManager>();

            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object);


            //Act
            var result = controller.AddReservation() as ViewResult;
          

            //Assert
            Assert.IsType(typeof(ViewResult), result);

            Assert.IsAssignableFrom<ReservationModel>(
                result.ViewData.Model);

        }

        [Fact]
        public void AddReservationGetMethodSuccess()
        {
            //arrange
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var manager = new Mock<ISportEventManager>();

            var reservationModel = new ReservationModel
            {
                Reservation = new Reservation
                {
                    Definite = true,
                    HallId = 2,
                    StartTime = DateTime.Now.AddDays(1).AddHours(1),
                    TimeFinish = DateTime.Now.AddDays(1).AddHours(3),
                    SportEventId = 3,
                    ReservationId = 15

                }
            };

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var reservationController = new ReservationController(manager.Object, hallMockManager.Object, buildingMockManager.Object)
            {
                TempData = tempData.Object
            };


            manager.Setup(r => r.AddReservation(reservationModel.Reservation)).Returns(true);
            //act 
            var viewResult = reservationController.AddReservation(reservationModel) as RedirectToActionResult;


            //assert


            Assert.IsType<RedirectToActionResult>(viewResult);
            Assert.Equal("SearchHall", (viewResult).ActionName);
            Assert.Equal("Building", (viewResult).ControllerName);

            Assert.True(reservationController.ModelState.IsValid);


        }

        [Fact]
        public void AddReservationGetMethodFail()
        {
            //arrange
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var manager = new Mock<ISportEventManager>();


            var reservationModel = new ReservationModel
            {
                Reservation = new Reservation()
                
               
            };


            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var reservationController = new ReservationController(manager.Object, hallMockManager.Object, buildingMockManager.Object)
            {
                TempData = tempData.Object
            };

            reservationController.ModelState.AddModelError("Error", "test");
    
            //act 
            var viewResult = reservationController.AddReservation(reservationModel) as ActionResult;


            //assert

           
            Assert.False(reservationController.ModelState.IsValid);
            Assert.IsType<ViewResult>(viewResult);






        }


        [Fact]
        public void ConfirmationPostMethodReturnsModel()
        {
            //Arrange
            var reservation = new ReservationHallsModel()
            {
                EndTime = DateTime.Now.AddDays(2).AddHours(2),
                StartTime = DateTime.Now.AddDays(2),
                EventId = 1,
                HallId = 1,
                
            };
          

            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            hallMockManager.Setup(h => h.GetHallReservation(1))
                .Returns(new Hall {Available = true, Building = new Building {BuildingId = 2}});
            buildingMockManager.Setup(b => b.GetBuilding(2)).Returns(new Building() {BuildingId = 2});
            
            var mockManager = new Mock<ISportEventManager>();
            mockManager.Setup(m => m.ViewSportEvent(1))
                .Returns(new SportEvent {SportEventId = 1, Description = "Voetbaltoernooi"});

            var controller =
                new ReservationController(mockManager.Object, hallMockManager.Object, buildingMockManager.Object);

            //Act
            var result = controller.Confirmation(reservation) as ViewResult;
            var model = result.Model as Reservation;

            //Assert
            Assert.IsType(typeof(ViewResult), result);

            Assert.IsAssignableFrom<Reservation>(
                result.ViewData.Model);
            Assert.Equal(model.SportEventId, 1);
        }

        [Fact]
        public void SaveReservationPostMethodFail()
        {

            //arrange
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var manager = new Mock<ISportEventManager>();

            var reservation = new Reservation
                {
                    Definite = true,
                    HallId = 2,
                    StartTime = DateTime.Now.AddDays(1).AddHours(1),
                    TimeFinish = DateTime.Now.AddDays(1).AddHours(3),
                    SportEventId = 3,
                    ReservationId = 15
            };

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var reservationController = new ReservationController(manager.Object, hallMockManager.Object, buildingMockManager.Object)
            {
                TempData = tempData.Object
            };

            manager.Setup(s => s.ViewSportEvent(3)).Returns(new SportEvent {Name = "Toernooi"});
            manager.Setup(r => r.AddReservation(reservation)).Returns(false);
            //act 
            var viewResult = reservationController.SaveReservation(reservation) as ViewResult;


            //assert

            Assert.IsType(typeof(ViewResult), viewResult);
            Assert.IsAssignableFrom<Reservation>(
                viewResult.ViewData.Model);
            Assert.Equal(viewResult.ViewName, "Confirmation");

        }


        [Fact]
        public void SaveReservationPostMethodSuccess()
        {

            //arrange
            var buildingMockManager = new Mock<IBuildingManager>();
            var hallMockManager = new Mock<IHallManager>();
            var manager = new Mock<ISportEventManager>();

            var reservation = new Reservation
            {
                Definite = true,
                HallId = 2,
                StartTime = DateTime.Now.AddDays(1).AddHours(1),
                TimeFinish = DateTime.Now.AddDays(1).AddHours(3),
                SportEventId = 3,
                ReservationId = 15
            };

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            var reservationController = new ReservationController(manager.Object, hallMockManager.Object, buildingMockManager.Object)
            {
                TempData = tempData.Object
            };

            manager.Setup(s => s.ViewSportEvent(3)).Returns(new SportEvent { Name = "Toernooi" });
            manager.Setup(r => r.AddReservation(reservation)).Returns(true);
            //act 
            var viewResult = reservationController.SaveReservation(reservation) as RedirectToActionResult;


            //assert

            Assert.Equal(viewResult.ActionName, "Index");
            Assert.IsType(typeof(RedirectToActionResult), viewResult);

        }


        private static IEnumerable<Reservation> Reservations()
        {
            var testdata = new List<Reservation>();

            for (var i = 1; i <= 3; i++)
            {
                var reservation = new Reservation
                {
                    SportEvent = new SportEvent {SportEventId = i},
                    Hall = new Hall {HallId = i},
                    Invoice = new Invoice {InvoiceId = i},
                    ReservationId = i,
                    StartTime = DateTime.Today.AddDays(1 + i).Date
                };
                testdata.Add(reservation);

            }

            return testdata;
        }
    }



}















