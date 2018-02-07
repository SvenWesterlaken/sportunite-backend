using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.Api.Models.ViewModels.BuildingApiController
{
    public class OpeningHoursDTO
    {
        public int OpeningHoursId { get; set; }
        public string Day { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
    }
}
