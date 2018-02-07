using SportUnite.Domain;

namespace SportUnite.UI.Models.ViewModels
{
    public class SportEventViewModel
    {
	    public int SportEventId { get; set; }
	    public string Name { get; set; }
	    public string SportEventDate { get; set; }
	    public string SportEventStartAndEndTime { get; set; }
	    public int MinAttendees { get; set; }
	    public int MaxAttendees { get; set; }
	    public bool HasReservation { get; set; }
	    public Sport Sport { get; set; }
    }
}
