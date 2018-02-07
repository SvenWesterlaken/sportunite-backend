using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportUnite.Data.Sportevent.Abstract;
using SportUnite.Domain;
using Xunit;

namespace SportUnite.Logic.Tests
{
    public class SportEventManagerTests
    {
		[Fact]
		public void AddSportEventSportEventManagerReturnsTrue()
		{
			//Arrange
			var events = List();
			var eventToTest = new SportEvent
			{
				SportEventId = 0, // new SportEvent doesnt have an id assigned to it yet
				Description = "Test",
				MaxAttendees = 10,
				MinAttendees = 5,
				Reservation = new Reservation()
			};

			var mockRepo = new Mock<ISportEventRepository>();
			mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
			mockRepo.Setup(a => a.AddSportEvent(It.IsAny<SportEvent>()));
			var manager = new SportEventManager(mockRepo.Object);

			//Act
			var result = manager.AddSportEvent(eventToTest);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public void AddSportEventSportEventManagerReturnsfalse()
		{
			//Arrange
			var events = List();
			var eventToTest = new SportEvent
			{
				SportEventId = 2, //Existing item ID
				Description = "Test",
				MaxAttendees = 10,
				MinAttendees = 5
			};

			var mockRepo = new Mock<ISportEventRepository>();
			mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
			mockRepo.Setup(a => a.AddSportEvent(It.IsAny<SportEvent>()));
			var manager = new SportEventManager(mockRepo.Object);

			//Act
			var result = manager.AddSportEvent(eventToTest);

			//Assert
			Assert.False(result);
		}



	    [Fact]
	    public void UpdateSportEventReturnsTrueOnSuccessfullyUpdatingSportEvent()
	    {
		    //Arrange
		    var events = List();
		    var updatedSportEvent = new SportEvent
		    {
			    SportEventId = 2, //Existing item ID
			    Description = "Test",
			    MaxAttendees = 10,
			    MinAttendees = 5
		    };


			var mockRepo = new Mock<ISportEventRepository>();
		    mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
		    mockRepo.Setup(a => a.UpdateSportEvent(It.IsAny<SportEvent>()));
		    var manager = new SportEventManager(mockRepo.Object);

			//Act
		    var result = manager.EditSportEvent(updatedSportEvent);

		    //Assert
		    Assert.True(result);
			
	    }

	    [Fact]
	    public void UpdateSportEventReturnsFalseOnFailedUpdatingSportEvent()
	    {
		    //Arrange
		    var events = List();
		    var updatedSportEvent = new SportEvent
		    {
			    SportEventId = 324, //id does not exist
			    Description = "Test",
			    MaxAttendees = 10,
			    MinAttendees = 5
		    };


		    var mockRepo = new Mock<ISportEventRepository>();
		    mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
		    mockRepo.Setup(a => a.UpdateSportEvent(It.IsAny<SportEvent>()));
		    var manager = new SportEventManager(mockRepo.Object);

		    //Act
		    var result = manager.EditSportEvent(updatedSportEvent);

		    //Assert
		    Assert.False(result);

	    }

	    [Fact]
	    public void DeleteSportEventReturnsTrueOnSuccessfullyDeletingSportEvent()
	    {
		    //Arrange
		    var events = List();
		    var sportEventToDelete = new SportEvent
		    {
			    SportEventId = 2, //id exists
			    Description = "Test",
			    MaxAttendees = 10,
			    MinAttendees = 5
		    };


		    var mockRepo = new Mock<ISportEventRepository>();
		    mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
		    mockRepo.Setup(a => a.DeleteSportEvent(It.IsAny<SportEvent>()));
		    var manager = new SportEventManager(mockRepo.Object);

		    //Act
		    var result = manager.DeleteSportEvent(sportEventToDelete.SportEventId);

		    //Assert
		    Assert.True(result);

	    }

	    [Fact]
	    public void DeleteSportEventReturnsFalseOnFailedDeletingSportEvent()
	    {
		    //Arrange
		    var events = List();
		    var sportEventToDelete = new SportEvent
		    {
			    SportEventId = 234, //id does not exists
			    Description = "Test",
			    MaxAttendees = 10,
			    MinAttendees = 5
		    };


		    var mockRepo = new Mock<ISportEventRepository>();
		    mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
		    mockRepo.Setup(a => a.DeleteSportEvent(It.IsAny<SportEvent>()));
		    var manager = new SportEventManager(mockRepo.Object);

		    //Act
		    var result = manager.DeleteSportEvent(sportEventToDelete.SportEventId);

		    //Assert
		    Assert.False(result);

	    }

	    [Fact]
	    public void ViewSportEventReturnsSportEvent()
	    {
		    //Arrange
		    var events = List();
		    var sportEventToRead = new SportEvent
		    {
			    SportEventId = 3, //id  exists
			    Description = "Test",
			    MaxAttendees = 10,
			    MinAttendees = 5
		    };


		    var mockRepo = new Mock<ISportEventRepository>();
		    mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
		    mockRepo.Setup(a => a.ReadSportEvent(It.IsAny<int>())).Returns(sportEventToRead);
		    var manager = new SportEventManager(mockRepo.Object);

		    //Act
		    var result = manager.ViewSportEvent(3);

		    //Assert
		    Assert.IsType(sportEventToRead.GetType(), result);

	    }

	    [Fact]
	    public void ViewSportEventReturnsNull()
	    {
		    //Arrange
		    var events = List();

		    var mockRepo = new Mock<ISportEventRepository>();
		    mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
		    mockRepo.Setup(a => a.ReadSportEvent(It.IsAny<int>()));
		    var manager = new SportEventManager(mockRepo.Object);

		    //Act
			// SportEvent with this id doesnt exist
		    var result = manager.ViewSportEvent(254);

		    //Assert
		    Assert.Equal(null, result);

	    }

	    [Fact]
	    public void ViewAllSportEventReturnsSportEvents()
	    {
		    //Arrange
		    var events = List();

		    var mockRepo = new Mock<ISportEventRepository>();
		    mockRepo.Setup(a => a.GetAllEvents()).Returns(events);
		    var manager = new SportEventManager(mockRepo.Object);

		    //Act
		    // SportEvent with this id doesnt exist
		    var result = manager.ViewAllSportEvents();

		    //Assert
		    Assert.Equal(events, result);

	    }

		

        //Manager tests for reservations 

       [Fact]
       public void AddReservationMethodReturnsTrueOnAddingUniqueItem()
       {
           //Arrange
           var repo = new Mock<ISportEventRepository>();
           repo.Setup(r => r.GetAllReservations()).Returns(Reservations());
           repo.Setup(a => a.AddReservation(It.IsAny<Reservation>()));

            var newReservation = new Reservation()
           {
               Hall = new Hall() { HallId = 10},
               Invoice = new Invoice() {InvoiceId = 10},
               ReservationId = 0 ,
               SportEvent  =  new SportEvent {SportEventId = 10}
           };
           var manager = new SportEventManager(repo.Object);

           //Act

           var result = manager.AddReservation(newReservation);

           //Assert

           Assert.True(result);

       }

       [Fact]
       public void AddReservationMethodReturnsFalseOnAddingExistingItem()
       {
           //Arrange
           var reservations = Reservations();
           var existingReservation = new Reservation()
           {
               SportEvent = new SportEvent() { SportEventId = 1 },
               Hall = new Hall() { HallId = 1 },
               Invoice = new Invoice() { InvoiceId = 1 },
               ReservationId = 1
           };
            
            var repo = new Mock<ISportEventRepository>();
           repo.Setup(r => r.GetAllReservations()).Returns(reservations);
            repo.Setup(a => a.AddReservation(It.IsAny<Reservation>()));
           var manager = new SportEventManager(repo.Object);

           //Act

           var result = manager.AddReservation(existingReservation);
      

           //Assert
           Assert.False(result);
        }



        [Fact]
        public void UpdateReservationMethodReturnsTrueOnExistingItem()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();
            repo.Setup(r => r.GetAllReservations()).Returns(Reservations());
            repo.Setup(a => a.UpdateReservation(It.IsAny<Reservation>()));


            var existingReservation = new Reservation()
            {
                SportEvent = new SportEvent() { SportEventId = 11},
                Hall = new Hall() { HallId = 1 },
                Invoice = new Invoice() { InvoiceId = 1 },
                ReservationId = 1
            };
            var manager = new SportEventManager(repo.Object);

            //Act

            var result = manager.UpdateReservation(existingReservation);
  
         
            //Assert

            Assert.True(result);
       

        }

        [Fact]
        public void UpdateReservationMethodReturnsFalseOnNewItem()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();
            repo.Setup(r => r.GetAllReservations()).Returns(Reservations());
           repo.Setup(a => a.UpdateReservation(It.IsAny<Reservation>()));

            var newReservation = new Reservation()
            {
                SportEvent = new SportEvent() { SportEventId = 10 },
                Hall = new Hall() { HallId = 10 },
                Invoice = new Invoice() { InvoiceId = 10 },
                ReservationId = 15
            };
            var manager = new SportEventManager(repo.Object);

            //Act

            var result = manager.UpdateReservation(newReservation);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteReservationMethodReturnsTrueOnExistingItem()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();
            repo.Setup(r => r.GetAllReservations()).Returns(Reservations());
            repo.Setup(a => a.DeleteReservation(It.IsAny<Reservation>()));

            var existingReservation = new Reservation()
            {
                SportEvent = new SportEvent() { SportEventId = 1 },
                Hall = new Hall() { HallId = 1 },
                Invoice = new Invoice() { InvoiceId = 1 },
                ReservationId = 1
            };
            var manager = new SportEventManager(repo.Object);

            //Act

            var result = manager.DeleteReservation(existingReservation);
            //Assert


            Assert.True(result);


        }

        [Fact]
        public void DeleteReservationMethodReturnsFalseOnNewItem()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();
            repo.Setup(r => r.GetAllReservations()).Returns(Reservations());
            repo.Setup(a => a.DeleteReservation(It.IsAny<Reservation>()));

            var newReservation = new Reservation()
            {
                SportEvent = new SportEvent() { SportEventId=  10 },
                Hall = new Hall() { HallId = 10 },
                Invoice = new Invoice() { InvoiceId = 10 },
                ReservationId = 15
            };
            var manager = new SportEventManager(repo.Object);

            //Act

            var result = manager.DeleteReservation(newReservation);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GetReservationReturnsRightReservation()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();
            repo.Setup(r => r.GetAllReservations()).Returns(Reservations());

            var reservation = new Reservation()
            {
                SportEvent = new SportEvent() { SportEventId = 10},
                Hall = new Hall() {HallId = 10},
                Invoice = new Invoice() {InvoiceId = 10},
                ReservationId = 15
            };
            repo.Setup(a => a.ReadReservation(It.IsAny<int>())).Returns(reservation);

            var manager = new SportEventManager(repo.Object);

            //Act

            var result = manager.GetReservation(15);

            //Assert
            Assert.Equal(reservation, result);
        }


        [Fact]
        public void GetReservationReturnsNull()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();
            repo.Setup(r => r.GetAllReservations()).Returns(Reservations());

            repo.Setup(a => a.ReadReservation(It.IsAny<int>()));

            var manager = new SportEventManager(repo.Object);

            //Act

            var result = manager.GetReservation(16);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetReservationsReturnsList()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();

            var list = Reservations();
            repo.Setup(r => r.GetAllReservations()).Returns(list);
            
            var manager = new SportEventManager(repo.Object);

            //Act

            var result = manager.Reservations();

            //Assert
            Assert.Equal(list, result);
        }

        [Fact]
        public void GetEventsWithoutReservationReturnsList()
        {
            //Arrange
            var repo = new Mock<ISportEventRepository>();

          
            repo.Setup(r => r.GetAllEvents()).Returns(List);

            var manager = new SportEventManager(repo.Object);
            
            //Act

            var result = manager.GetAllEventsWithoutReservation();

            //Assert
            Assert.Equal(result.Count(), 4);
        }



        private static IEnumerable<Reservation> Reservations()
       {
           var testdata = new List<Reservation>();

           for (var i = 1; i <= 3; i++)
           {
               var reservation = new Reservation
               {
                   SportEvent = new SportEvent { SportEventId = i},
                   Hall = new Hall {HallId = i},
                   Invoice = new Invoice {InvoiceId = i},
                   ReservationId = i
               };

               testdata.Add(reservation);

           }

           return testdata;
       }

        private static IEnumerable<SportEvent> List()
        {
            var events = new List<SportEvent>();

            var e1 = new SportEvent { Description = "Test", SportEventId = 1, MaxAttendees = 10, MinAttendees = 5 };
            var e2 = new SportEvent { Description = "Test", SportEventId = 2, MaxAttendees = 10, MinAttendees = 5 };
            var e3 = new SportEvent { Description = "Test", SportEventId = 3, MaxAttendees = 10, MinAttendees = 5 };
            var e4 = new SportEvent { Description = "Test", SportEventId = 4, MaxAttendees = 10, MinAttendees = 5 };
            var e5 = new SportEvent { Description = "Test", SportEventId = 5, MaxAttendees = 10, MinAttendees = 5 , Reservation = new Reservation { ReservationId = 1, SportEventId = 5}};
            events.Add(e1);
            events.Add(e2);
            events.Add(e3);
            events.Add(e4);
            events.Add(e5);

            return events;
        }

    }



}

