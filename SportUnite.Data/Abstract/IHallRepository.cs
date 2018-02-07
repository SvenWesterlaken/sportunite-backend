using System;
using System.Collections.Generic;
using System.Text;
using SportUnite.Domain;

namespace SportUnite.Data.Abstract
{
    public interface IHallRepository
    {
        IEnumerable<Hall> Halls();
        IEnumerable<OpeningHours> OpeningHours();
        IEnumerable<HallOpeningHours> HallOpeningHours();
        bool CreateHall(Hall hall);
        bool CreateOpeningHours(Hall hall);
        bool UpdateHall(Hall hall);
        bool UpdateOpeningHours(Hall hall);
        bool RemoveHall(Hall hall);
        bool RemoveOpeningHours(OpeningHours openingHours);
        bool RemoveHallOpeningHours(HallOpeningHours hallOpeningHours);
        Hall GetHall(int hallId);

        Hall GetHallReservation(int hallId);
    }
}
