using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SportUnite.Data.Abstract;
using SportUnite.Domain;

namespace SportUnite.Data.Concrete
{
	public class EFSportRepository : ISportRepository
	{
		private readonly ApplicationDbContext _sportContext;

		public EFSportRepository(ApplicationDbContext sportContext)
		{
			_sportContext = sportContext;
		}


		public IEnumerable<Sport> Sports() => _sportContext.Sports.AsNoTracking().Include(p => p.SportObjectSports).ThenInclude(x => x.SportObject).AsNoTracking();

		public Sport Read(int sportid)
		{
			return Sports().First(a => a.SportId == sportid);
		}

	    public IEnumerable<SportObject> GetAllSportObjects() => _sportContext.SportObjects.AsNoTracking();
	    
	    public SportObject ReadSportObject(int sportObjectid) => _sportContext.SportObjects.Find(sportObjectid);

	    public void AddSport(Sport sport)
		{
			_sportContext.Sports.Add(sport);
			_sportContext.SaveChanges();
		}

		public void UpdateSport(Sport sport) {
			var updatesport = Read(sport.SportId);
			updatesport.Name = sport.Name;
		    _sportContext.Sports.Update(updatesport);
		    _sportContext.SaveChanges();
		}

        public void AddSportObject(Sport sport, SportObject sportObject)
	    {
	        _sportContext.SportObjects.Add(sportObject);
            SportObjectSport sportObjectSport = new SportObjectSport
            {
                Sport = sport,SportId = sport.SportId,SportObject = sportObject, SportObjectId = sportObject.SportObjectId
            };
	        _sportContext.SportObjectSports.Add(sportObjectSport);
	        _sportContext.SaveChanges();
	    }


	    public void UpdateSportObject(SportObject sportObject)
	    {

	        _sportContext.SportObjects.Update(sportObject);
	        _sportContext.SaveChanges();
        }

	    public void DeleteSport(Sport sport)
		{
			_sportContext.Remove(sport);
			_sportContext.SaveChanges();
		}

	    public void DeleteSportObject(SportObject sportObject)
	    {
	        _sportContext.Remove(sportObject);
	        _sportContext.SaveChanges();
	    }

		public IEnumerable<SportObject> GetSportObjectsFromHall(Hall hall)
		{
			var objects = new List<SportObject>();
			foreach (var soh in _sportContext.SportObjectHalls.AsNoTracking().Include(a => a.SportObject).AsNoTracking())
			{
				if (soh.HallId == hall.HallId)
				{
					//Found match
					objects.Add(soh.SportObject);
				}
			}

			return objects;
		}

		public void UpdateHallObjects(Hall hall, int[] selectedEntries)
		{
			//First remove all of this hall from DB to clean up
			var objects = _sportContext.SportObjectHalls.AsNoTracking().Where(a => a.HallId == hall.HallId);
			_sportContext.SportObjectHalls.RemoveRange(objects);

			_sportContext.SaveChanges();

			//Loop through all SportObject IDs and add them again
			foreach (var i in selectedEntries)
			{
				var sportObject = _sportContext.SportObjects.AsNoTracking().FirstOrDefault(a => a.SportObjectId == i);

				//Object found
				if (sportObject != null)
				{
					var original = _sportContext.SportObjectHalls.AsNoTracking().FirstOrDefault(a => a.HallId == hall.HallId && a.SportObjectId == i);

					//Tracking issues: if original SportObjectHall is found, update the original one
					if (original != null)
					{
						original.Hall = hall;
						original.HallId = hall.HallId;
						original.SportObject = sportObject;
						original.SportObjectId = sportObject.SportObjectId;

						_sportContext.Update(original);
						_sportContext.SaveChanges();
					}
					else
					{
						var sportObjectHall = new SportObjectHall
						{
							Hall = hall,
							HallId = hall.HallId,
							SportObject = sportObject,
							SportObjectId = sportObject.SportObjectId
						};

						_sportContext.SportObjectHalls.Add(sportObjectHall);
						_sportContext.SaveChanges();
					}
				}
			}
		}
	}
}
