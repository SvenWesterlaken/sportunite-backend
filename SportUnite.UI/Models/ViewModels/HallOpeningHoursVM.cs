using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportUnite.Domain;
using SportUnite.Logic;

namespace SportUnite.UI.ViewModels
{
    public class HallOpeningHoursVM
    {
        public int BuildingId{ get; set; }
        public List<Building> Buildings { get; set; }
        public Hall Hall { get; set; }
        public int OpeningHoursId { get; set; }
        public string Day { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public List<OpeningHours> OpeningHours { get; set; }
        public List<HallOpeningHoursVM> OpeningHoursVM { get; set; }
	    public ISportManager SportManager { get; set; }
	    public IEnumerable<SportObject> SportObjects { get; set; }
    }
}
