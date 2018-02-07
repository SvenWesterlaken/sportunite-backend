using System.Collections.Generic;
using System.Resources;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SportUnite.Domain;

namespace SportUnite.Data.Sportevent.Abstract
{
    public interface ISportEventRepository
    {
        void AddSportEvent(SportEvent sportEvent);

        void AddReservation(Reservation reservation);

	    void UpdateSportEvent(SportEvent updatedSportEvent);

        void UpdateReservation(Reservation reservationToUpdate);

        SportEvent ReadSportEvent(int id);

        Reservation ReadReservation(int id);

        void DeleteSportEvent(SportEvent sportEvent);

        void DeleteReservation(Reservation reservation);


        IEnumerable<SportEvent> GetAllEvents();

        IEnumerable<Reservation> GetAllReservations();

    }
}
