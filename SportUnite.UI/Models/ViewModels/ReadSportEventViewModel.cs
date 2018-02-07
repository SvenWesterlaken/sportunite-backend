namespace SportUnite.UI.Models.ViewModels
{
    public class ReadSportEventViewModel
    {
	    public int SportEventId { get; set; }
	    public string SportEventName { get; set; }
	    public string SportName { get; set; }
	    public int MinAttendees { get; set; }
	    public int MaxAttendees { get; set; }
	    public string SportEventDescription { get; set; }
	    public string SportEventDate { get; set; }
	    public string SportEventEventTime { get; set; }
	    public int ReservationId { get; set; }
		public string Definite { get; set; }
	    public string HallName { get; set; }
	    public string BuildingName { get; set; }
	    public string BuildingAddress { get; set; }
	}
}
