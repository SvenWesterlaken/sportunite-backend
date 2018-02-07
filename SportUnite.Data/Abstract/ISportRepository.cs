using System;
using System.Collections.Generic;
using System.Text;
using SportUnite.Domain;

namespace SportUnite.Data.Abstract
{
    public interface ISportRepository
    {
        IEnumerable<Sport> Sports();

        IEnumerable<SportObject> GetAllSportObjects();

		Sport Read (int sportid);

        SportObject ReadSportObject (int sportObjectid);
        void AddSport(Sport sport);

        void AddSportObject(Sport sport, SportObject sportObject);

        void UpdateSport(Sport sport);

        void UpdateSportObject(SportObject sportObject);

        void DeleteSport(Sport sport);

        void DeleteSportObject(SportObject sportObject);

	    IEnumerable<SportObject> GetSportObjectsFromHall(Hall hall);

	    void UpdateHallObjects(Hall hall, int[] selectedEntries);
    }
}
