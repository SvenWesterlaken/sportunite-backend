using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;
using SportUnite.Api.Extension;
using SportUnite.Api.Models.ViewModels.HallApi;
using SportUnite.Domain;
using SportUnite.Logic;

namespace SportUnite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/halls")]
    public class HallApiController : Controller
    {
	    private readonly IHallManager _hallManager;
	    private readonly IMapper _mapper;

	    public HallApiController(IHallManager hallManager, IMapper mapper)
	    {
		    _hallManager = hallManager;
		    _mapper = mapper;
		}

	    // GET reservations
	    [HttpGet]
	    public IActionResult GetHalls([FromQuery]int offset = 0, [FromQuery]int limit = 10,
			[FromQuery]string name = "", [FromQuery]int? buildingId = null, [FromQuery]bool? available = true,
		    [FromQuery]double? minPrice = null, [FromQuery]double? maxPrice = null)
	    {
		    var halls = _hallManager.Halls().Where(b => b.Name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1
				&& b.Available == available).Skip(offset).Take(limit);

		    if (buildingId != null)
		    {
			    halls = halls.Where(b => b.BuildingId == buildingId);
		    }

		    if (minPrice != null && maxPrice != null)
		    {
			    halls = halls.Where(h => h.Price >= minPrice && h.Price <= maxPrice);
		    }


		    var hallMapped = _mapper.Map<IEnumerable<HallDTO>>(halls).Select(h => new HALResponse(h).AddLinks(h.GetLinks()));

		    return Ok(new HALResponse(null).AddLinks(new[] {
			    new Link("self", "/api/halls", null, "GET")
		    }).AddEmbeddedCollection("halls", hallMapped));
		}

	    // GET specific reservation 
	    [HttpGet("{id}", Name = "GetHall")]
	    public IActionResult GetHall(int id)
	    {
		    var hall = _hallManager.GetHall(id);

		    if (hall == null) {
			    return NotFound();
		    }
				
		    var hallMapped = _mapper.Map<HallDTO>(hall);
		    return this.HAL(hallMapped, hallMapped.GetLinks());
	    }
	}
}