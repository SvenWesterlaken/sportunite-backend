using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportUnite.Domain;

namespace SportUnite.Logic
{
    public interface ISportEventManager
    {
        

            bool AddSportEvent(SportEvent sportEvent);


            bool EditSportEvent(SportEvent updatedSportEvent);

            SportEvent ViewSportEvent(int sportEventId);

            IEnumerable<SportEvent>
            ViewAllSportEvents();


        bool DeleteSportEvent(int sportEventId);


            IEnumerable<Reservation>
            Reservations();


            IEnumerable<Reservation> Reservations(SportEvent sportEvent);



            Reservation GetReservation(int reservationId);


            bool AddReservation(Reservation reservation);

            bool DeleteReservation(Reservation reservation);


            bool UpdateReservation(Reservation newReservation);

        IEnumerable<SportEvent> GetAllEventsWithoutReservation();

    }
    }
