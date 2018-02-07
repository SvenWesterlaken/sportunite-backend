using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportUnite.Data.Sportevent.Abstract;
using SportUnite.Domain;

namespace SportUnite.Data.Concrete
{
	public class EfSportEventRepository : ISportEventRepository
	{
		private ApplicationDbContext _context;

		public EfSportEventRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public void AddSportEvent(SportEvent sportEvent)
		{
			sportEvent.CreatedAt = DateTime.Now;
			_context.SportEvents.Add(sportEvent);
			_context.SaveChanges();
		}

		public void UpdateSportEvent(SportEvent updatedSportEvent)
		{
			updatedSportEvent.ModifiedAt = DateTime.Now;
			_context.Update(updatedSportEvent);
			_context.SaveChanges();
		}

		public void UpdateReservation(Reservation updatedReservation)
		{

		    Reservation newReservation =
		        _context.Reservations.FirstOrDefault(x => x.ReservationId == updatedReservation.ReservationId);
		    if (newReservation != null)
		    {

		        newReservation.Definite = updatedReservation.Definite;
		        newReservation.HallId = updatedReservation.HallId;
                newReservation.SportEventId = updatedReservation.SportEventId;
                newReservation.StartTime = updatedReservation.StartTime;
                newReservation.TimeFinish = updatedReservation.TimeFinish;
		        newReservation.ModifiedAt = DateTime.Now;

                _context.Update(newReservation);

            }
          
		    _context.SaveChanges();
	
        }

		public SportEvent ReadSportEvent(int id)
		{
			return _context.SportEvents.AsNoTracking().Include(se => se.Sport).AsNoTracking().Include(se => se.Reservation)
				.ThenInclude(r => r.Hall).ThenInclude(h => h.Building).ThenInclude(b => b.Address).AsNoTracking().FirstOrDefault(s => s.SportEventId == id);
		}

		public Reservation ReadReservation(int id)
		{
		    return GetAllReservations().FirstOrDefault(r => r.ReservationId == id);
        }

		public void DeleteSportEvent(SportEvent sportEvent)
		{
			SportEvent se = new SportEvent
			{
				SportEventId = sportEvent.SportEventId,
				Name = sportEvent.Name,
				MinAttendees = sportEvent.MinAttendees,
				MaxAttendees = sportEvent.MaxAttendees,
				Description = sportEvent.Description,
				EventStartTime = sportEvent.EventStartTime,
				EventEndTime = sportEvent.EventEndTime,
				SportId = sportEvent.SportId,
				Sport = sportEvent.Sport,
				Reservation = sportEvent.Reservation
			};

			_context.Entry(_context.Set<SportEvent>().Local.FirstOrDefault(e => e.SportEventId.Equals(sportEvent.SportEventId)))
				.State = EntityState.Detached;
			
			_context.Remove(se);
			_context.SaveChanges();
			
		}

		public void DeleteReservation(Reservation reservation)
		{
		    reservation.DeletedAt = DateTime.Now;
		    _context.Reservations.Remove(reservation);
		    _context.SaveChanges();
        }
	    public void AddReservation(Reservation reservation)
	    {
	        reservation.CreatedAt = DateTime.Now; 
	       
	        _context.Reservations.Add(reservation);
	        _context.SaveChanges();
	    }
		public IEnumerable<SportEvent> GetAllEvents()
		{
		    return _context.SportEvents.AsNoTracking().Include(se => se.Sport).AsNoTracking().Include(se => se.Reservation).AsNoTracking();
		}


	    public IEnumerable<Reservation> GetAllReservations() =>
	        _context.Reservations.AsNoTracking().Include(r => r.SportEvent).Include(r => r.Hall).Include(r => r.Invoice);



	}
}
