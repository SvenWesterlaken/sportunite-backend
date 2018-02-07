using Moq;
using SportUnite.Data.Abstract;
using SportUnite.Domain;
using Xunit;

namespace SportUnite.Logic.Tests {
    public class SportManagerTests {
		[Fact]
		public void GetAllSportMethodReturnListWithSports() {
			//Arrange
			var sports = new Sport[] {
				new Sport {Name = "sport 1"},
				new Sport {Name = "sport 2"},
				new Sport {Name = "sport 3"},
				new Sport {Name = "sport 4"},
			};
			var mockRepo = new Mock<ISportRepository>();
			mockRepo.Setup(s => s.Sports()).Returns(sports);
			var manager = new SportManager(mockRepo.Object);

			//Act
			var returnSport = manager.SportsList();

			//Assert
			Assert.Equal(sports, returnSport);
		}


		[Fact]
		public void GetAllSportObjectsMethodReturnListWithSportsObjects()
		{
			//Arrange
			var sportobjects = new SportObject[]
			{
				new SportObject {Name = "sportobject 1"},
				new SportObject {Name = "sportobject 2"},
				new SportObject {Name = "sportobject 3"},
				new SportObject {Name = "sportobject 4"}
			};
			var mockRepo = new Mock<ISportRepository>();
			mockRepo.Setup(s => s.GetAllSportObjects()).Returns(sportobjects);
			var manager = new SportManager(mockRepo.Object);
		}

		[Fact]
		public void AddSportMethodReturnsTrueIfNewEntry() {
			//Arrange
			var dummysport = new Sport {
				Name = "KongBall",
			};
			var sports = new Sport[] {
				new Sport {Name = "sport 1"},
				new Sport {Name = "sport 2"},
				new Sport {Name = "sport 3"},
				new Sport {Name = "sport 4"},
			};
			var mockRepo = new Mock<ISportRepository>();
			mockRepo.Setup(s => s.Sports()).Returns(sports);
			var manager = new SportManager(mockRepo.Object);



			//Act
			var result = manager.AddSport(dummysport);

			//Assert
			Assert.True(result);
		}

	    [Fact]
	    public void AddSportObjectMethodReturnsTrueIfNewEntry()
	    {
            //Arrange
	        
            var sportobjects = new SportObject
	        {
	            Name = "KongballObject"
	        };
	        var dummysport = new Sport
	        {
	            Name = "KongBall",
	        };
            var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.AddSportObject(dummysport, sportobjects));
	        var manager = new SportManager(mockRepo.Object);



	        //Act
	        var result = manager.AddSportObject(dummysport, sportobjects);

	        //Assert
	        Assert.True(result);
	    }

	    [Fact]
	    public void FailedAddSportObject()
	    {
	        var dummysportobject = new SportObject
	        {
	            Name = "sportobject 1",
	        };
	        var dummysport = new Sport
	        {
	            Name = "KongBall",
	        };
            var sportobjects = new SportObject[]
	        {
	            new SportObject {Name = "sportobject 1"},
	            new SportObject {Name = "sportobject 2"},
	            new SportObject {Name = "sportobject 3"},
	            new SportObject {Name = "sportobject 4"}
	        };
	        var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.GetAllSportObjects()).Returns(sportobjects);
	        mockRepo.Setup(x => x.AddSportObject(dummysport, dummysportobject));
	        var manager = new SportManager(mockRepo.Object);



	        //Act
	        var result = manager.AddSportObject(dummysport, dummysportobject);

	        //Assert
	        Assert.False(result);
	    }

        [Fact]
		public void FailedAddSport() {
			var dummysport = new Sport {
				Name = "sport 1",
			};
			var sports = new Sport[] {
				new Sport {Name = "sport 2"},
				new Sport {Name = "sport 3"},
				new Sport {Name = "sport 4"},
				dummysport
			};

			var mockRepo = new Mock<ISportRepository>();
			mockRepo.Setup(s => s.Sports()).Returns(sports);
			var manager = new SportManager(mockRepo.Object);



			//Act
			var result = manager.AddSport(dummysport);

			//Assert
			Assert.False(result);
		}

	    [Fact]
	    public void TestUpdateSportMethodReturnTrue()
	    {
	        //Arrange
	        var sports = new Sport[]
	        {
	            new Sport {Name = "sport 1"},
	            new Sport {Name = "sport 2"},
	            new Sport {Name = "sport 3"},
	            new Sport {Name = "sport 4"},
	        };
	        var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.Sports()).Returns(sports);
	        var manager = new SportManager(mockRepo.Object);
	        var updatedSport = new Sport { SportId = 0, Name = "VoetBall" };

	        //Act
	        var result = manager.UpdateSport(updatedSport);
	        //Assert
	        Assert.True(result);
	    }

	    [Fact]
	    public void TestUpdateSportMethodReturnFalse()
	    {
	        //Arrange
	        var sports = new Sport[]
	        {
	            new Sport {Name = "sport 1"},
	            new Sport {Name = "sport 2"},
	            new Sport {Name = "sport 3"},
	            new Sport {Name = "sport 4"},
	        };
	        var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.Sports()).Returns(sports);
	        var manager = new SportManager(mockRepo.Object);
	        var updatedSport = new Sport { SportId = 6, Name = "VoetBall" };

	        //Act
	        var result = manager.UpdateSport(updatedSport);
	        //Assert
	        Assert.False(result);
	    }

	    [Fact]
	    public void TestUpdateSportObjectMethodReturnTrue()
	    {
	        //Arrange
	        var sportobjects = new SportObject[]
	        {
	            new SportObject {Name = "sportobject 1"},
	            new SportObject {Name = "sportobject 2"},
	            new SportObject {Name = "sportobject 3"},
	            new SportObject {Name = "sportobject 4"}
	        };
	        var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.GetAllSportObjects()).Returns(sportobjects);
	        var manager = new SportManager(mockRepo.Object);
	        var updatedSportObject = new SportObject { SportObjectId = 0, Name = "VoetBall" };

	        //Act
	        var result = manager.UpdateSportObject(updatedSportObject);
	        //Assert
	        Assert.True(result);
	    }

	    [Fact]
	    public void TestUpdateSportObjectMethodReturnFalse()
	    {
	        //Arrange
	        var sportobjects = new SportObject[]
	        {
	            new SportObject {Name = "sportobject 1"},
	            new SportObject {Name = "sportobject 2"},
	            new SportObject {Name = "sportobject 3"},
	            new SportObject {Name = "sportobject 4"}
	        };
	        var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.GetAllSportObjects()).Returns(sportobjects);
	        var manager = new SportManager(mockRepo.Object);
	        var updatedSportObject = new SportObject { SportObjectId = 6, Name = "VoetBall" };

	        //Act
	        var result = manager.UpdateSportObject(updatedSportObject);
	        //Assert
	        Assert.False(result);
	    }

        [Fact]
	    public void TestDeleteSportMethodReturnTrueIfExist()
	    {
	        //Arrange

	        var sports = new Sport[]
	        {
	            new Sport {Name = "sport 1"},
	            new Sport {Name = "sport 2"},
	            new Sport {Name = "sport 3"},
	            new Sport {Name = "sport 4"},
	        };
	        var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.Sports()).Returns(sports);
	        var manager = new SportManager(mockRepo.Object);

	        //Act
	        var result = manager.DeleteSport(sports[1]);
	        var resultFalse = manager.DeleteSport(new Sport() { Name = "sport 5" });

	        //Assert
	        Assert.True(result);
	        Assert.False(resultFalse);
	    }

        [Fact]
	    public void TestDeleteSportObjectMethodReturnTrueIfExist()
	    {
	        //Arrange

	        var sportobjects = new SportObject[]
	        {
	            new SportObject {Name = "sportobject 1"},
	            new SportObject {Name = "sportobject 2"},
	            new SportObject {Name = "sportobject 3"},
	            new SportObject {Name = "sportobject 4"}

	        };
	        var mockRepo = new Mock<ISportRepository>();
	        mockRepo.Setup(s => s.GetAllSportObjects()).Returns(sportobjects);
	        var manager = new SportManager(mockRepo.Object);

	        //Act
	        var result = manager.DeleteSportObject(sportobjects[1]);
	        var resultFalse = manager.DeleteSportObject(new SportObject() { Name = "sportobject 5" });

	        //Assert
	        Assert.True(result);
	        Assert.False(resultFalse);
	    }
    }
}

