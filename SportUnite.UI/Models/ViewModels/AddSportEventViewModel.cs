using System.Collections.Generic;
using SportUnite.Domain;

namespace SportUnite.UI.Models.ViewModels
{
    public class AddSportEventViewModel
    {
	    public IEnumerable<Sport> Sports { get; set; }
	    public SportEvent SportEvent { get; set; }
	    public bool AddReservation { get; set; }
    }
}
