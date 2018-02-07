using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL;

namespace SportUnite.Api.Models.ViewModels
{
    public class SportEventDTO
    {
	    public int SportEventId { get; set; }

	    public string Name { get; set; }

	    public int MinAttendees { get; set; }

	    public int MaxAttendees { get; set; }

	    public string Description { get; set; }

	    public DateTime EventStartTime { get; set; }

	    public DateTime EventEndTime { get; set; }

	    public int SportId { get; set; }

	    public int? ReservationId { get; set; }
		}
}
