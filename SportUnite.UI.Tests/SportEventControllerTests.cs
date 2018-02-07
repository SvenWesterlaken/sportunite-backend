using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PagedList.Core;
using SportUnite.Data.Abstract;
using SportUnite.Domain;
using SportUnite.Logic;
using SportUnite.UI.Controllers;
using SportUnite.UI.Extentions;
using SportUnite.UI.Models.ViewModels;
using SportUnite.UI.ViewModels;
using Xunit;

namespace SportUnite.UI.Tests
{
    public class SportEventControllerTests
    {
	    [Fact]
	    public void IndexMethodWithSearchStringReturnsCorrectPagedListOfSportEventViewModels()
	    {
			//Arrange
		    var sportEvents = new[]
		    {
			    new SportEvent {Name = "Voetbal"},
			    new SportEvent {Name = "Basketbal"},
			    new SportEvent {Name = "Hockey"},
			    new SportEvent {Name = "Volleybal"}
		    };
		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    mockSportEventManager.Setup(x => x.ViewAllSportEvents()).Returns(sportEvents);
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object);

		    //Act
		    var result = controller.Index(null, null, "bal", 1) as ViewResult;
		    var model = (IEnumerable<SportEventViewModel>) result?.Model;
		    var countModelsInViewModels = model.Count();

			//Assert
			Assert.IsType(typeof(PagedList<SportEventViewModel>), result.ViewData.Model);
		    Assert.Equal(3, countModelsInViewModels);
		}

	    [Fact]
	    public void AmountOfPagesOnIndexMethodIsCorrect()
	    {
		    //Arrange
		    var sportEvents = new[]
		    {
			    new SportEvent{ Name = "Voetbal"},
			    new SportEvent{Name = "Basketbal"},
			    new SportEvent{Name = "Hockey"},
			    new SportEvent{ Name = "Volleybal"},
			    new SportEvent{Name = "Trefbal"},
			    new SportEvent{Name = "Tennis"},
			    new SportEvent{Name = "Darten"},
			    new SportEvent{Name = "Karate"},
			    new SportEvent{Name = "Dansen"},
			    new SportEvent{Name = "Yoga"},
			    new SportEvent{Name = "Badminton"},
			    new SportEvent{Name = "Softbal"}
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object);

		    mockSportEventManager.Setup(a => a.ViewAllSportEvents()).Returns(sportEvents);

		    //Act
		    var result = controller.Index(null, null, null, 1) as ViewResult;
		    var pageList = (PagedList<SportEventViewModel>)result.ViewData.Model;

		    //Assert
		    Assert.Equal(2, pageList.PageCount);
		    Assert.Equal(12, pageList.TotalItemCount);
	    }

	    [Fact]
	    public void IndexMethodWithSortOrderDescendingReturnsCorrectCollectionOfSportEventsSortedDescending()
	    {
		    //Arrange
		    var sportEvents = new[]
		    {
			    new SportEvent {Name = "Voetbal"},
			    new SportEvent {Name = "Basketbal"},
			    new SportEvent {Name = "Hockey"},
			    new SportEvent {Name = "Volleybal"}
		    };
		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object);


		    mockSportEventManager.Setup(a => a.ViewAllSportEvents()).Returns(sportEvents);

		    //Act
		    var result = controller.Index("name_desc", null, null, 1) as ViewResult;
		    var pageList = (PagedList<SportEventViewModel>)result.ViewData.Model;

		    //Assert
		    Assert.IsType(typeof(PagedList<SportEventViewModel>), result.ViewData.Model);
		    Assert.Same("Volleybal", pageList[0].Name);
	    }

	    [Fact]
	    public void TestAddSportEventReturnsToIndexActionWithSuccesMessage()
	    {
			//Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 30,
			    MinAttendees = 22,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent,
			    AddReservation = false
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();	
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
			    TempData = mockTempData.Object
		    };

		    mockSportEventManager.Setup(x => x.AddSportEvent(It.IsAny<SportEvent>())).Returns(true);

		    //Act
		    var result = controller.Add(addSportEventViewModel) as RedirectToActionResult;

		    //Assert
		    Assert.IsType(typeof(RedirectToActionResult), result);
		    Assert.Same("Index", result.ActionName);
		}

	    [Fact]
	    public void TestAddSportEventReturnsToAddMethodInReservationControllerAction()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 30,
			    MinAttendees = 22,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent,
			    AddReservation = true
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
			    TempData = mockTempData.Object
		    };

		    mockSportEventManager.Setup(x => x.AddSportEvent(It.IsAny<SportEvent>())).Returns(true);

		    //Act
		    var result = controller.Add(addSportEventViewModel) as RedirectToActionResult;

		    //Assert
		    Assert.IsType(typeof(RedirectToActionResult), result);
		    Assert.Same("Index", result.ActionName);
	    }

		[Fact]
	    public void TestAddModelInvalidSportEventReturnsAddViewWithErrorMessage()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent,
			    AddReservation = false
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
			    TempData = mockTempData.Object
		    };
		    controller.ModelState.AddModelError("key", "error message");
		    

		    //Act
		    var result = controller.Add(addSportEventViewModel) as ViewResult;

		    //Assert
		    Assert.IsType(typeof(ViewResult), result);
	    }

	    [Fact]
	    public void TestAddSportEventThatThrowsExceptionReturnsAddViewWithErrorMessage()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent,
			    AddReservation = false
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
			    TempData = mockTempData.Object
		    };

		    mockSportEventManager.Setup(sm => sm.AddSportEvent(It.IsAny<SportEvent>())).Throws<Exception>();

		    //Act
		    var result = controller.Add(addSportEventViewModel) as ViewResult;

			//Assert
			Assert.IsType(typeof(ViewResult), result);
		}

	    [Fact]
	    public void TestAddSportEventGetViewThatContainsListOfSports()
	    {
		    //Arrange
		    var sports = new[]
		    {
			    new Sport {Name = "Voetbal"},
			    new Sport {Name = "Basketbal"},
			    new Sport {Name = "Hockey"},
			    new Sport {Name = "Volleybal"}
		    };

			var addSportEventViewModel = new AddSportEventViewModel
		    {
			    Sports = sports,
			    AddReservation = false
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object);

		    mockSportManager.Setup(sm => sm.SportsList()).Returns(sports);

		    //Act
		    var result = controller.Add();
		    var viewModel = (AddSportEventViewModel) result.Model;

		    //Assert
		    Assert.IsType(typeof(ViewResult), result);
			Assert.Equal(4, viewModel.Sports.Count());
		}

	    [Fact]
	    public void TestDeleteSportEventSuccesAndReturnsToIndexMethod()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
				TempData = mockTempData.Object
			};

		    mockSportEventManager.Setup(sm => sm.DeleteSportEvent(It.IsAny<int>())).Returns(true);

		    //Act
		    var result = controller.Delete(dummySportEvent);

		    //Assert
		    Assert.IsType(typeof(RedirectToActionResult), result);
	    }

	    [Fact]
	    public void TestDeleteSportEventFailAndThrowsExceptionAndReturnsActionResult()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
			    TempData = mockTempData.Object
		    };

		    mockSportEventManager.Setup(sm => sm.DeleteSportEvent(It.IsAny<int>())).Throws<Exception>();

		    //Act
		    var result = controller.Delete(dummySportEvent);

		    //Assert
		    Assert.IsType(typeof(RedirectToActionResult), result);
	    }

	    [Fact]
	    public void TestReadSportEventSuccesAndReturnsModelViewWithoutReservationData()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13),
				Sport = new Sport
				{
					Name = "Sport"
				}
		    };

		    var viewModel = new ReadSportEventViewModel
		    {
			    SportEventId = dummySportEvent.SportEventId,
			    SportEventName = dummySportEvent.Name,
			    SportName = dummySportEvent.Sport.Name,
			    SportEventEventTime =
				    $"{dummySportEvent.EventStartTime.FormatTime()} - {dummySportEvent.EventEndTime.FormatTime()}",
			    SportEventDate = dummySportEvent.EventStartTime.FormatDate(),
			    MinAttendees = dummySportEvent.MinAttendees,
			    MaxAttendees = dummySportEvent.MaxAttendees,
			    SportEventDescription = dummySportEvent.Description

		    };

			var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object);
		    

		    mockSportEventManager.Setup(sm => sm.ViewSportEvent(It.IsAny<int>())).Returns(dummySportEvent);

		    //Act
		    var result = controller.Read(1) as ViewResult;
		    var resultModel = (ReadSportEventViewModel) result.Model;

		    //Assert
		    Assert.IsType(typeof(ViewResult), result);
			Assert.Equal(viewModel.SportEventId, resultModel.SportEventId);
			Assert.Equal(0, resultModel.ReservationId);
	    }

	    [Fact]
	    public void TestReadSportEventSuccesAndReturnsModelViewWithReervationData()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13),
			    Sport = new Sport
			    {
				    Name = "Sport"
			    },
				Reservation = new Reservation
				{
					ReservationId = 2,
					Hall = new Hall
					{
						Name = "test",
						Building = new Building
						{
							Name = "test",
							Address = new Address
							{
								StreetName = "test",
								HouseNumber = 3,
								Suffix = "test",
								City = "test"
							}
						}
					}
				}
			};

		    var viewModel = new ReadSportEventViewModel
		    {
			    SportEventId = dummySportEvent.SportEventId,
			    SportEventName = dummySportEvent.Name,
			    SportName = dummySportEvent.Sport.Name,
			    SportEventEventTime =
				    $"{dummySportEvent.EventStartTime.FormatTime()} - {dummySportEvent.EventEndTime.FormatTime()}",
			    SportEventDate = dummySportEvent.EventStartTime.FormatDate(),
			    MinAttendees = dummySportEvent.MinAttendees,
			    MaxAttendees = dummySportEvent.MaxAttendees,
			    SportEventDescription = dummySportEvent.Description,
			    ReservationId = dummySportEvent.Reservation.ReservationId,

			};

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object);


		    mockSportEventManager.Setup(sm => sm.ViewSportEvent(It.IsAny<int>())).Returns(dummySportEvent);

		    //Act
		    var result = controller.Read(1) as ViewResult;
		    var resultModel = (ReadSportEventViewModel)result.Model;

		    //Assert
		    Assert.IsType(typeof(ViewResult), result);
		    Assert.Equal(viewModel.SportEventId, resultModel.SportEventId);
		    Assert.Equal(2, resultModel.ReservationId);
	    }

		[Fact]
	    public void TestUpdateSportEventGetRequestsReturnsViewWithSportEventModel()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };
		    var sports = new[]
		    {
			    new Sport{ Name = "Voetbal"},
			    new Sport{Name = "Basketbal"},
			    new Sport{Name = "Hockey"},
			    new Sport{ Name = "Volleybal"},
		    };

			var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent,
			    Sports = sports
			};


			var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object);

			mockSportManager.Setup(sm => sm.SportsList()).Returns(sports);
			mockSportEventManager.Setup(sm => sm.ViewSportEvent(It.IsAny<int>())).Returns(dummySportEvent);

		    //Act
		    var result = controller.Update(1) as ViewResult;
		    var viewModel = (AddSportEventViewModel) result.Model;

			//Assert
			Assert.IsType(typeof(ViewResult), result);
		    Assert.Equal(4, viewModel.Sports.Count());
	    }

	    [Fact]
	    public void TestUpdateSportEventPostRequestsSuccesAndReturnsToIndex()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };
		    

			var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent
		    };

		    var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();

			var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
			{
				TempData = mockTempData.Object
			};

			mockSportEventManager.Setup(sm => sm.EditSportEvent(It.IsAny<SportEvent>())).Returns(true);

		    //Act
		    var result = controller.Update(addSportEventViewModel) as RedirectToActionResult;

		    //Assert
		    Assert.IsType(typeof(RedirectToActionResult), result);
			Assert.Same("Index", result.ActionName);
	    }

	    [Fact]
	    public void TestUpdateSportEventPostRequestModelStateInvalidReturnsToIndex()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent
		    };

			var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();
			var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
			    TempData = mockTempData.Object
		    };
			controller.ModelState.AddModelError("key", "error message");

			mockSportEventManager.Setup(sm => sm.EditSportEvent(It.IsAny<SportEvent>())).Returns(true);

		    //Act
		    var result = controller.Update(addSportEventViewModel) as ViewResult;
		    

			//Assert
			Assert.IsType(typeof(ViewResult), result);
		    Assert.Same("Update", result.ViewName);
	    }

	    [Fact]
	    public void TestUpdateSportEventPostRequestModelStateValidButSportEventManagerThrowsException()
	    {
		    //Arrange
		    var dummySportEvent = new SportEvent
		    {
			    Name = "Voetbaltoernooi",
			    Description = "Potje voetballen voor de starters",
			    MaxAttendees = 5,
			    MinAttendees = 20,
			    SportId = 2,
			    EventStartTime = DateTime.Today.AddHours(11),
			    EventEndTime = DateTime.Today.AddHours(13)
		    };

		    var addSportEventViewModel = new AddSportEventViewModel
		    {
			    SportEvent = dummySportEvent
		    };

			var mockSportManager = new Mock<ISportManager>();
		    var mockSportEventManager = new Mock<ISportEventManager>();
		    var mockTempData = new Mock<ITempDataDictionary>();
		    var controller = new SportEventController(mockSportEventManager.Object, mockSportManager.Object)
		    {
			    TempData = mockTempData.Object
		    };

		    mockSportEventManager.Setup(sm => sm.EditSportEvent(It.IsAny<SportEvent>())).Throws<Exception>();

		    //Act
		    var result = controller.Update(addSportEventViewModel) as ViewResult;

		    //Assert
		    Assert.IsType(typeof(ViewResult), result);
			Assert.Same("Update", result.ViewName);
	    }
	}
}
