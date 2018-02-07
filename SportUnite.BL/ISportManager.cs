using System;
using System.Collections.Generic;
using System.Text;
using SportUnite.Domain;

namespace SportUnite.Logic
{
    public interface ISportManager
    {
        IEnumerable<Sport> SportsList();

        IEnumerable<SportObject> SportObjects();
        Sport Read(int sportid);

        SportObject ReadSportObject(int sportObjectid);

        bool AddSport(Sport sport);

        bool AddSportObject(Sport sport,SportObject sportObject);

        bool UpdateSport(Sport sport);

        bool UpdateSportObject(SportObject sportObjects);

        bool DeleteSport(Sport sport);

        bool DeleteSportObject(SportObject sportObjects);

	    IEnumerable<SportObject> GetSportObjectsFromHall(Hall hall);

	    void UpdateHallObjects(Hall hall, int[] selectedEntries);
    }
}
