using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL;
using SportUnite.Api.Models.ViewModels;
using SportUnite.Api.Models.ViewModels.BuildingApiController;
using SportUnite.Domain;
using HallDTO = SportUnite.Api.Models.ViewModels.HallApi.HallDTO;
using BuildingDTO = SportUnite.Api.Models.ViewModels.BuildingApiController.BuildingDTO;
using ReservationDTO = SportUnite.Api.Models.ViewModels.ReservationApi.ReservationDTO;
using SportDTO = SportUnite.Api.Models.ViewModels.SportApi.SportDTO;

namespace SportUnite.Api.Extension
{
    public static class LinkExtensions
    {
		public static IEnumerable<Link> GetLinks(this SportEventDTO se)
		{
			return new[] {
				new Link("self", "/api/sportevents/" + se.SportEventId, null, "GET"),
				new Link("self", "/api/sportevents/" + se.SportEventId, null, "DELETE"),
				new Link("self", "/api/sportevents/" + se.SportEventId, null, "PUT"),
				new Link("reservation", "/api/reservations/" + se.ReservationId, null, "GET"),
				new Link("sport", "/api/sports/" + se.SportId, null, "GET")

			};
		}

	    public static IEnumerable<Link> GetLinks(this HallDTO h) {
		    return new[] {
			    new Link("self", "/api/halls/" + h.HallId, null, "GET")
		    };
	    }

	    public static IEnumerable<Link> GetLinks(this BuildingDTO b)
	    {
		    return new[] {
			    new Link("self", "/api/buildings/" + b.BuildingId, null, "GET")
		    };
	    }

	    public static IEnumerable<Link> GetLinks(this ReservationDTO r) {
		    return new[] {
					new Link("self", "/api/reservations/"+ r.ReservationId, null, "GET"),
			    new Link("self", "/api/reservations/"+ r.ReservationId, "deleteReservation", "DELETE"),
			    new Link("self", "/api/reservations/", "addReservation", "POST"),
			    new Link("self", "/api/reservations/"+ r.ReservationId, "putReservation", "PUT"),
			    new Link("sportEvent", "/api/sportevents/" + r.SportEventId, null, "GET" ),
			    new Link("hall", "/api/halls/" + r.HallId, null, "GET")
				};
	    }

	    public static IEnumerable<Link> GetLinks(this SportDTO s) {
				return new[] {
					new Link("self", "/api/sports/" + s.SportId, null, "GET")
				};
			}
	}
}
