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
using SportUnite.Api.Models.ViewModels.SportApi;
using SportUnite.Domain;
using SportUnite.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportUnite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/sports")]
    public class SportsController : Controller
    {
        private readonly ISportManager _sportManager;
        private readonly IMapper _mapper;

        // GET: api/values
        public SportsController(ISportManager sportManager, IMapper mapper)
        {
            _sportManager = sportManager;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetSport([FromQuery]int offset = 0, [FromQuery]int limit = 10, [FromQuery]string name = "")
        {
            var sports = _sportManager.SportsList().Where(b => b.Name.IndexOf(name, StringComparison.CurrentCultureIgnoreCase) != -1).Skip(offset).Take(limit);

            var sportMapped = _mapper.Map<IEnumerable<SportDTO>>(sports).Select(s => new HALResponse(s).AddLinks(s.GetLinks()));

            return Ok(new HALResponse(null).AddLinks(new[] {
							new Link("self", "/api/sports", null, "GET")
						}).AddEmbeddedCollection("sports", sportMapped)); ;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
				    var sport = _sportManager.Read(id);

						if (sport == null) {
		        
						}

            var sportMap = _mapper.Map<SportDTO>(sport);
            return this.HAL(sportMap, sportMap.GetLinks());
        }
    }
}
