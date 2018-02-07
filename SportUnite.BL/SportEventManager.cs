using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportUnite.Data;
using SportUnite.Data.Sportevent.Abstract;
using SportUnite.Domain;

namespace SportUnite.Logic
{
	public class SportEventManager : ISportEventManager
	{

		private readonly ISportEventRepository _repository;

		public SportEventManager(ISportEventRepository repo)
		{
			_repository = repo;
		}

		public bool AddSportEvent(SportEvent sportEvent)
		{

			if (sportEvent.SportEventId != 0)
			{
				return false;

			}

			_repository.AddSportEvent(sportEvent);
			return true;
		}

		public bool EditSportEvent(SportEvent updatedSportEvent)
		{
			var sportEventToUpdate = _repository.GetAllEvents().FirstOrDefault(s => s.SportEventId == updatedSportEvent.SportEventId);
			if (sportEventToUpdate == null)
			{
				return false;
			}
			_repository.UpdateSportEvent(updatedSportEvent);
			return true;
		}

		public SportEvent ViewSportEvent(int sportEventId)
		{
			return _repository.ReadSportEvent(sportEventId);
		}

		public IEnumerable<SportEvent> ViewAllSportEvents()
		{
			return _repository.GetAllEvents();
		}

		public bool DeleteSportEvent(int sportEventId)
		{
			var dbEntry = _repository.GetAllEvents().FirstOrDefault(s => s.SportEventId == sportEventId);
			if (dbEntry == null)
			{
				return false;
			}

			_repository.DeleteSportEvent(dbEntry);
			return true;
		}

	    public IEnumerable<Reservation> Reservations()
	    {
	        return _repository.GetAllReservations();
	    }

	    public IEnumerable<Reservation> Reservations(SportEvent sportEvent)
	    {

	        return _repository.GetAllReservations().Where(r => r.SportEvent.SportEventId == sportEvent.SportEventId);
	    }


        public Reservation GetReservation(int reservationId)
	    {
	        return _repository.ReadReservation(reservationId);
	    
	    }

	    public bool AddReservation(Reservation reservation)
	    {

	        if (Reservations().Any(r => r.ReservationId == reservation.ReservationId))
            {
	            return false;

	        }

	        _repository.AddReservation(reservation);
	        return true;

        }

	    public bool DeleteReservation(Reservation reservation)
	    {

	        if (Reservations().Any(r => r.ReservationId == reservation.ReservationId))
	        {
	            _repository.DeleteReservation(reservation);
	            return true;
	        }
	        return false;



	    }

	    public bool UpdateReservation(Reservation newReservation)
	    {
	        var reservationToUpdate = _repository.GetAllReservations()
	            .FirstOrDefault(s => s.ReservationId == newReservation.ReservationId);
	        if (reservationToUpdate != null)


	        {
	            _repository.UpdateReservation(newReservation);
	            return true;
	        }
	        return false;

	       

        }

	    public IEnumerable<SportEvent> GetAllEventsWithoutReservation()
	    {

	        return ViewAllSportEvents().Where(r => r.Reservation == null);

	    }

	 

    }
}

