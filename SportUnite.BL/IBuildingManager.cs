using System.Collections.Generic;
using SportUnite.Domain;

namespace SportUnite.Logic {
    public interface IBuildingManager {
        IEnumerable<Building> Buildings();
        IEnumerable<Address> Addresses();
        bool CreateBuilding(Building building);
        bool UpdateBuilding(Building building);
        bool UpdateAddress(Address address);
        bool RemoveBuilding(Building building);
        Building GetBuilding(int buildingId);
        Address GetAddress(int buildingId);
    }
}
