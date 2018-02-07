using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.Api.Models.ViewModels.BuildingApiController
{
    public class BuildingDTO
    {
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public List<HallDTO> Halls { get; set; }
    }
}
