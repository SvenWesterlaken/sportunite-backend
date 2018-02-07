using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace SportUnite.Domain
{
	public class SportEvent : AbstractAttributes
    {
	    public int SportEventId { get; set; }
	    [StringLength(224, MinimumLength = 3)]
		[Required(ErrorMessage = "Voeg een naam toe van 3 tot 224 karakters")]
	    public string Name { get; set; }
		[MaxLessThanMinAttendees("MaxAttendees")]
		[Required]
	    public int MinAttendees { get; set; }
	    [Required]
		public int MaxAttendees { get; set; }
	    [StringLength(224, MinimumLength = 5)]
	    [Required(ErrorMessage = "Voeg een beschrijving toe van 5 tot 224 karakters")]
		public string Description { get; set; }
	    [CustomStartDate]
		[DateLessThan("EventEndTime")]
		[Required(ErrorMessage = "Begintijd is vereist")]
	    public DateTime EventStartTime { get; set; }
		[CustomStartDate]
	    [Required(ErrorMessage = "Eindtijd is vereist")]
		public DateTime EventEndTime { get; set; }
	    public int SportId { get; set; }
	    public Sport Sport { get; set; }
	    public Reservation Reservation { get; set; }
		
    }
}
