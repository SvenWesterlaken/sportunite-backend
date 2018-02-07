using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.Api.Models.ViewModels.ReservationApi
{
      
    public class ReservationDTO
    {
        public int ReservationId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime TimeFinish { get; set; }

        public bool Definite { get; set; }

        public int SportEventId { get; set; }

        public int HallId { get; set; }
    }
}
