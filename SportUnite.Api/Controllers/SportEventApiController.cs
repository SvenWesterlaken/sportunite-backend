using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;
using SportUnite.Api.Extension;
using SportUnite.Api.Models.ViewModels;
using SportUnite.Domain;
using SportUnite.Logic;


namespace SportUnite.Api.Controllers
{
	[Produces("application/json")]
	[Route("api/sportevents")]
	public class SportEventApiController : Controller
	{
		private readonly ISportEventManager _manager;
	    private readonly IMapper _mapper;

        public SportEventApiController(ISportEventManager manager, IMapper mapper)
		{
			_manager = manager;
		    _mapper = mapper;

		}

		// GET sportevents
		[HttpGet]
		public IActionResult GetSportEvents([FromQuery]int offset = 0, [FromQuery]int limit = 10, [FromQuery]string name = "", [FromQuery]int searchSport = -1, [FromQuery] string searchDate = "")
		{
			var originalItems = _mapper.Map<IEnumerable<SportEventDTO>>(_manager.ViewAllSportEvents());

			var sportEvents = originalItems.Where(s => s.Name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1).Skip(offset).Take(limit);

			if (searchSport != -1)
			{
				sportEvents = sportEvents.Where(s => s.SportId.Equals(searchSport));
			}

			if (searchDate != "")
			{
				var items = searchDate.Split('-'); //items[0] year, items[1] month, items[2] day
				var itemsInt = items.Select(int.Parse).ToArray();
				var dt = new DateTime(itemsInt[0], itemsInt[1], itemsInt[2]);

				sportEvents = sportEvents.Where(s => s.EventStartTime.Date == dt);
			}

			var hallresponselist = sportEvents.Select(se => new HALResponse(se).AddLinks(se.GetLinks()));

			return Ok(new HALResponse(null).AddLinks(new[] {
				new Link("self", "/api/sportevents", null, "GET"),
				new Link("self", "/api/sportevents", "addSportevent", "POST")
			}).AddEmbeddedCollection("sportevents", hallresponselist));
		}

		// GET specific sportevent
		[HttpGet("{id}", Name = "getsportevent")]
		public IActionResult GetSportEvent(int id)
		{
			var se = _manager.ViewSportEvent(id);

			if (se == null)
			{
				return NotFound();
			}

		    var sportEventMapped = _mapper.Map<SportEventDTO>(se);
		    return this.HAL(sportEventMapped, sportEventMapped.GetLinks());
		}

		// POST sportevent
		[HttpPost]
		public IActionResult CreateSportEvent([FromBody] SportEventDTO sportEvent)
		{
			if (sportEvent == null)
			{
				return BadRequest();
			}


		    var se = _mapper.Map<SportEvent>(sportEvent);

            try
		    {
		        _manager.AddSportEvent(se);
		    }

		    catch (Exception)
		    {
		        BadRequest("Add sportevent not allowed");
		    }

		    return CreatedAtRoute("getsportevent", new {id = se.SportEventId}, _mapper.Map<SportEventDTO>(se));
		}

		// PUT sportevent
		[HttpPut("{id}")]
		public IActionResult UpdateSportEvent(int id, [FromBody]SportEventDTO sportEvent)
		{
			if (sportEvent == null || sportEvent.SportEventId != id)
			{
				return BadRequest();
			}

			var sportEventToUpdate = _manager.ViewSportEvent(id);
			if (sportEventToUpdate == null)
			{
				return NotFound();
			}

		

		    var se = _mapper.Map<SportEvent>(sportEvent);
            try
		    {

		        _manager.EditSportEvent(se);
		    }
		    catch(Exception)
		    {
		        return BadRequest("Delete sportevent not allowed");
            }
		    return Ok("SportEvent updated");
		}

		// DELETE sportevent
		[HttpDelete("{id}")]
		public IActionResult DeleteSportEvent(int id)
		{
			var sportEvent = _manager.ViewSportEvent(id);
			if (sportEvent == null)
			{
				return NotFound();
			}

			try
			{
				_manager.DeleteReservation(sportEvent.Reservation);
				_manager.DeleteSportEvent(id);
			}
			catch (Exception e)
			{
				return BadRequest("Delete sportevent not allowed");
			}
		    return Ok("SportEvent deleted");

		}
	}
}

