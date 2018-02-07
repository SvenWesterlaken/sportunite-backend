using SportUnite.Data.Abstract;
using SportUnite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportUnite.Data.Concrete
{
    public class EFBuildingRepository : IBuildingRepository
    {
        private readonly ApplicationDbContext _context;

        public EFBuildingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Building> Buildings() =>
            _context.Buildings.AsNoTracking()
            .Include(a => a.Halls)
                .ThenInclude(b => b.HallOpeningHours)
                    .ThenInclude(c => c.OpeningHours)
            .Include(a => a.Halls)
                .ThenInclude(b => b.Reservations)
             .Include(a => a.Halls)
                .ThenInclude(b => b.SportHalls)
                    .ThenInclude(c => c.Sport)
                        .ThenInclude(d => d.SportObjectSports)
                            .ThenInclude(e => e.SportObject)
            .Include(a => a.Address);

        public IEnumerable<Address> Addresses() => _context.Addresses.AsNoTracking();

        public bool CreateBuilding(Building building)
        {
            try
            {
                _context.Add(building);
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateBuilding(Building building)
        {
            try
            {
                _context.Update(building);
                building.ModifiedAt = DateTime.Now;
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateAddress(Address address)
        {
            try
            {
                _context.Update(address);
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveBuilding(Building building)
        {
            try
            {
                _context.Remove(building);
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public Building GetBuilding(int buildingId)
        {
            //return Queryable.FirstOrDefault<Building>(_context.Buildings.AsNoTracking().Include(a => a.Halls).Include(b => b.Address), c => c.BuildingId.Equals(buildingId));
            return Queryable.FirstOrDefault<Building>(_context.Buildings.AsNoTracking()
                .Include(a => a.Halls)
                    .ThenInclude(b => b.HallOpeningHours)
                        .ThenInclude(c => c.OpeningHours)
                .Include(a => a.Halls)
                    .ThenInclude(b => b.Reservations)
                .Include(a => a.Halls)
                    .ThenInclude(b => b.SportHalls)
                        .ThenInclude(c => c.Sport)
                            .ThenInclude(d => d.SportObjectSports)
                                .ThenInclude(e => e.SportObject)
                .Include(a => a.Address),
                c => c.BuildingId.Equals(buildingId));
        }

        public Address GetAddress(int buildingId)
        {
            return Queryable.FirstOrDefault<Address>(_context.Addresses.AsNoTracking(), a => a.BuildingId.Equals(buildingId));
        }
    }
}
