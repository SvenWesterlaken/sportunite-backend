using System;
using PagedList.Core;
using SportUnite.Domain;

namespace SportUnite.UI.Models.ViewModels
{
    public class ReservationHallsModel {
	    public DateTime StartTime { get; set; }
			public DateTime EndTime { get; set; }
			public int EventId { get; set; }
			public IPagedList<Building> Buildings { get; set; }
			public int HallId { get; set; }
			public int ReservationId { get; set; }
    }
}
