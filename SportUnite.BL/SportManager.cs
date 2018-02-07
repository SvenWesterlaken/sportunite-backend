using System.Collections.Generic;
using System.Linq;
using SportUnite.Data;
using SportUnite.Data.Abstract;
using SportUnite.Domain;

namespace SportUnite.Logic
{
	public class SportManager : ISportManager
	{
		private readonly ISportRepository _sportRepository;
		public SportManager(ISportRepository sportcontext)
		{
			_sportRepository = sportcontext;
		}

		public IEnumerable<Sport> SportsList()
		{
			return _sportRepository.Sports();
		}

        public IEnumerable<SportObject> SportObjects()
        {
            return _sportRepository.GetAllSportObjects();
        }

        public Sport Read(int sportid)
        {
            return _sportRepository.Read(sportid);
        }

        public SportObject ReadSportObject(int sportObjectid)
        {
            return _sportRepository.ReadSportObject(sportObjectid);
        }

        public bool AddSport(Sport sport)
        {
            if (SportsList().All(a => a.Name != sport.Name))
            {
                _sportRepository.AddSport(sport);
                return true;
                
            }
            else
            {
                return false;
            }
            
        }

        public bool AddSportObject(Sport sport, SportObject sportObject)
        {
            if (SportObjects().All (o => o.Name != sportObject.Name))
            {
                _sportRepository.AddSportObject(sport,sportObject);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateSport(Sport sport)
        {
            if (SportsList().Any(a => a.SportId == sport.SportId))
            {
                _sportRepository.UpdateSport(sport);
                return true;
            }
            return false;

		}

        public bool UpdateSportObject(SportObject sportObject)
        {
            if (SportObjects().Any(o => o.SportObjectId == sportObject.SportObjectId))
            {
                _sportRepository.UpdateSportObject(sportObject);
                return true;
            }
            return false;
        }


        public bool DeleteSport(Sport sport)
        {
            if (SportsList().Any(a => a.Name == sport.Name))
            {
                _sportRepository.DeleteSport(sport);
                return true;
            }
            return false;
        }

        public bool DeleteSportObject(SportObject sportObject)
        {
            if (SportObjects().Any(o=> o.Name == sportObject.Name))
            {
                _sportRepository.DeleteSportObject(sportObject);
                return true;
            }
            return false;
        }

	    public IEnumerable<SportObject> GetSportObjectsFromHall(Hall hall)
	    {
		    return _sportRepository.GetSportObjectsFromHall(hall);
	    }

	    public void UpdateHallObjects(Hall hall, int[] selectedEntries)
	    {
		    _sportRepository.UpdateHallObjects(hall, selectedEntries);
	    }
	}
}
