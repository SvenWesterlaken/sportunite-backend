using SportUnite.Data.Abstract;
using SportUnite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportUnite.Data.Concrete
{
    public class EFHallRepository : IHallRepository {
        private readonly ApplicationDbContext _context;

        public EFHallRepository(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<Hall> Halls() =>
            _context.Halls.AsNoTracking().Include(a => a.Reservations).Include(a => a.HallOpeningHours).ThenInclude(b => b.OpeningHours)
			.Include(a => a.Building)
			.Include(a => a.SportHalls).ThenInclude(ah =>ah.Sport).ThenInclude(a => a.SportObjectSports).ThenInclude(a => a.SportObject)
			.Include(a => a.SportObjectHalls).ThenInclude(a => a.SportObject);
        public IEnumerable<OpeningHours> OpeningHours() => _context.OpeningHours.AsNoTracking().Include(a => a.HallOpeningHours).ThenInclude(b => b.Hall);
        public IEnumerable<HallOpeningHours> HallOpeningHours() => _context.HallOpeningHours.AsNoTracking().Include(a => a.OpeningHours).Include(a => a.Hall);

        public bool CreateHall(Hall hall) {
            try {
                _context.Add(hall);
                _context.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public bool CreateOpeningHours(Hall hall) {
            try {
                _context.Update(hall);
                _context.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public bool UpdateHall(Hall hall) {
            try {
                _context.Update(hall);
                hall.ModifiedAt = DateTime.Now;
                _context.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public bool UpdateOpeningHours(Hall hall) {
            try {
                _context.Update(hall);
                _context.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public bool RemoveHall(Hall hall) {
            try
            {
                _context.Remove(hall);
                _context.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public bool RemoveOpeningHours(OpeningHours openingHours) {
            try {
                _context.Remove(openingHours);
                _context.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public bool RemoveHallOpeningHours(HallOpeningHours hallOpeningHours) {
            try {
                _context.Remove(hallOpeningHours);
                _context.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public Hall GetHallReservation(int hallId) {
            return Queryable.FirstOrDefault<Hall>(_context.Halls.AsNoTracking().Include(a => a.Reservations).Include(a => a.HallOpeningHours), h => h.HallId.Equals(hallId));

        }

        public Hall GetHall(int hallId)
        {
            return Queryable.FirstOrDefault<Hall>(_context.Halls.AsNoTracking().Include(a => a.Reservations)
				.Include(a => a.HallOpeningHours).ThenInclude(b => b.OpeningHours).Include(a => a.Building)
				.Include(a => a.SportHalls).ThenInclude(a => a.Sport).ThenInclude(a => a.SportObjectSports).ThenInclude(a => a.SportObject)
				.Include(a => a.SportObjectHalls).ThenInclude(a => a.SportObject).AsNoTracking(), h => h.HallId.Equals(hallId));
        }
    }
}
