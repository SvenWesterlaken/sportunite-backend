using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.Api.Models.ViewModels.BuildingApiController
{
    public class HallDTO
    {
        public int HallId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public int BuildingId { get; set; }
        public List<SportDTO> Sports { get; set; }
        public List<OpeningHoursDTO> OpeningHours { get; set; }
        public List<ReservationDTO> Reservations { get; set; }
    }
}
