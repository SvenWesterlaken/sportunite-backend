using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportUnite.Api.Models.ViewModels.BuildingApiController;
using SportUnite.Domain;
using SportUnite.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using SportUnite.Api.Extension;


namespace SportUnite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/buildings")]
    public class BuildingApiController : Controller
    {
        private readonly IBuildingManager _manager;
        private readonly IMapper _mapper;

        public BuildingApiController(IBuildingManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        // GET buildings
        [HttpGet]
        public IActionResult GetBuildings([FromQuery]int offset = 0, [FromQuery]int limit = 10, [FromQuery]string name = "", [FromQuery]string city = "")
        {
            var buildings = _manager.Buildings().Where(b => b.Name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1 && b.Address.City.IndexOf(city, StringComparison.CurrentCultureIgnoreCase) != -1).Skip(offset).Take(limit);

						var buildingsMapped = _mapper.Map<IEnumerable<BuildingDTO>>(buildings).Select(b => new HALResponse(b).AddLinks(b.GetLinks()));

						return Ok(new HALResponse(null).AddLinks(new[] {
							new Link("self", "/api/buildings", null, "GET")
						}).AddEmbeddedCollection("buildings", buildingsMapped));
				}

        // GET specific building
        [HttpGet("{id}", Name = "GetBuilding")]
        public IActionResult GetBuilding(int id)
        {
            var building = _manager.GetBuilding(id);

	        if (building == null) {
		        return NotFound();

	        }
							
            var buildingMapped = _mapper.Map<BuildingDTO>(building);
            return this.HAL(buildingMapped, buildingMapped.GetLinks());
        }
    }
}
