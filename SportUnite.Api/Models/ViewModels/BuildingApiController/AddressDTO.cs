using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.Api.Models.ViewModels.BuildingApiController
{
    public class AddressDTO
    {
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string Suffix { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
