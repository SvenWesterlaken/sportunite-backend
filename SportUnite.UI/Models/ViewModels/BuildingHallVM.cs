using System;
using SportUnite.Domain;
using System.Collections.Generic;

namespace SportUnite.UI.ViewModels {
    // used for maken a new building, needs adjusting
    public class BuildingHallVM
    {

        public Building Building { get; set; }
        public Hall Hall { get; set; }

        //public string Day { get; set; }
        //public DateTime OpeningTime { get; set; }
        //public DateTime ClosingTime { get; set; }
        //public List<HallOpeningHours> HallOpeningHours { get; set; }
        //public List<BuildingHallVM> Halls { get; set; }
        public List<OpeningHours> OpeningHours { get; set; }
    }
}

