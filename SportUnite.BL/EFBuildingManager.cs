using System.Collections.Generic;
using System.Linq;
using SportUnite.Data.Abstract;
using SportUnite.Domain;

namespace SportUnite.Logic {
    public class EFBuildingManager : IBuildingManager {

        private readonly IBuildingRepository _buildingRepository;
        private readonly IHallManager _hallManager;

        public EFBuildingManager(IBuildingRepository buildingRepository, IHallManager hallManager) {
            _buildingRepository = buildingRepository;
            _hallManager = hallManager;

        }

        public IEnumerable<Building> Buildings() {
            return _buildingRepository.Buildings();
        }

        public IEnumerable<Address> Addresses() {
            return _buildingRepository.Addresses();
        }

        public bool CreateBuilding(Building building) {
            //Building already exists
            if (Enumerable.Any<Building>(_buildingRepository.Buildings(), b => b.Address.City == building.Address.City &&
                                                        b.Address.Country == building.Address.Country &&
                                                         b.Address.State == building.Address.State &&
                                                         b.Address.StreetName == building.Address.StreetName &&
                                                         b.Address.Suffix == building.Address.Suffix &&
                                                         b.Address.ZipCode == building.Address.ZipCode &&
                                                         b.Address.HouseNumber == building.Address.HouseNumber)) {
                return false;
            }
            var halls = building.Halls;
            foreach (Hall h in halls) {
                for (int i = h.HallOpeningHours.Count - 1; i >= 0; i--) {
                    var repoOpeningHours = Enumerable.FirstOrDefault<OpeningHours>(_hallManager.OpeningHours(), repOh =>
                        repOh.Day == h.HallOpeningHours[i].OpeningHours.Day &&
                        repOh.OpeningTime == h.HallOpeningHours[i].OpeningHours.OpeningTime &&
                        repOh.ClosingTime == h.HallOpeningHours[i].OpeningHours.ClosingTime);

                    if (repoOpeningHours != null) {
                        h.HallOpeningHours.RemoveAt(i);
                        h.HallOpeningHours.Add(new HallOpeningHours() {
                            HallId = h.HallId,
                            OpeningHoursId = repoOpeningHours.OpeningHoursId
                        });
                    }
                }
            }
            return _buildingRepository.CreateBuilding(building);
        }

        public bool UpdateBuilding(Building building) {
            var buildingToUpdate = Enumerable.FirstOrDefault<Building>(_buildingRepository.Buildings(), b => b.BuildingId == building.BuildingId);
            if (buildingToUpdate != null) {
                buildingToUpdate.Name = building.Name;
                buildingToUpdate.Address.Country = building.Address.Country;
                buildingToUpdate.Address.State = building.Address.State;
                buildingToUpdate.Address.City = building.Address.City;
                buildingToUpdate.Address.ZipCode = building.Address.ZipCode;
                buildingToUpdate.Address.StreetName = building.Address.StreetName;
                buildingToUpdate.Address.HouseNumber = building.Address.HouseNumber;
                buildingToUpdate.Address.Suffix = building.Address.Suffix;
                return _buildingRepository.UpdateBuilding(buildingToUpdate);
            } else {
                return false;
            }
        }

        public bool UpdateAddress(Address address) {
            var addressToUpdate = Enumerable.FirstOrDefault<Address>(_buildingRepository.Addresses(), a => a.AddressId == address.AddressId);
            if (addressToUpdate != null) {
                return _buildingRepository.UpdateAddress(address);
            } else {
                return false;
            }
        }

        public bool RemoveBuilding(Building building) {
            var buildingToUpdate = Enumerable.FirstOrDefault<Building>(_buildingRepository.Buildings(), b => b.BuildingId == building.BuildingId);
            if (buildingToUpdate != null) {
                //checking for each Hall if it still has Reservations
                var openingHoursListAllHalls= new List<HallOpeningHours>();
                foreach (Hall h in buildingToUpdate.Halls)
                {
                    if (h.Reservations?.Any() == true)
                    {
                        return false;
                    }

                    //get list of all OpeningHours used by each Hall before removing
                    var openingHoursSingleHall =
                        Enumerable.Where<HallOpeningHours>(_hallManager.HallOpeningHours(),
                            hoh => hoh.HallId == h.HallId).ToList();
                    foreach (HallOpeningHours hoh in openingHoursSingleHall)
                    {
                        openingHoursListAllHalls.Add(hoh);
                    }
                }
                //remove Building
                var buildingRemoved = _buildingRepository.RemoveBuilding(building);
                 //get all distinct OpeningHours from all Halls
                foreach (HallOpeningHours hoh in openingHoursListAllHalls.Distinct()) {
                        int openingHourCount = Enumerable.Count<HallOpeningHours>(_hallManager.HallOpeningHours(), ho => ho.OpeningHoursId == hoh.OpeningHoursId);
                        //remove entry in OpeningHours table if it is no longer used
                        if (openingHourCount == 0) {
                            _hallManager.RemoveOpeningHours(new OpeningHours { OpeningHoursId = hoh.OpeningHoursId });
                        }
                    }
                return buildingRemoved;
            } else {
                return false;
            }
        }

        public Building GetBuilding(int buildingId) {
            return _buildingRepository.GetBuilding(buildingId);
        }

        public Address GetAddress(int buildingId) {
            return _buildingRepository.GetAddress(buildingId);
        }
    }
}
