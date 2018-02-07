using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportUnite.Api.Models.ViewModels.ReservationApi;
using SportUnite.Domain;
using SportUnite.Logic;
using AutoMapper;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using SportUnite.Api.Extension;


namespace SportUnite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/reservations")]
    public class ReservationApiController : Controller
    {
        private readonly ISportEventManager _manager;
        private readonly IMapper _mapper;

        public ReservationApiController(ISportEventManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        // GET specific reservation 
        [HttpGet("{id}", Name = "getreservation")]
        public IActionResult GetReservation(int id)
        {
            var reservation = _manager.GetReservation(id);

	        if (reservation == null) {
		        return NotFound();
	        }
						
            var reservationMapped = _mapper.Map<ReservationDTO>(reservation);
						return this.HAL(reservationMapped, reservationMapped.GetLinks());
        }

        // POST reservation
        [HttpPost]
        public IActionResult CreateReservation([FromBody] ReservationDTO reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

  
            var reservationMapped = _mapper.Map<Reservation>(reservation);

            try
            {

                _manager.AddReservation(reservationMapped);
            }

            catch (Exception)
            {
                return BadRequest("Add reservation not allowed");
                }

            return CreatedAtRoute("getreservation", new {id = reservation.ReservationId},
                _mapper.Map<ReservationDTO>(reservationMapped));
        }

        // PUT reservation
        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] ReservationDTO reservation)
        {
            if (reservation== null || reservation.ReservationId != id)
            {
                return BadRequest();
            }

            var reservationToUpdate = _manager.GetReservation(id);
            if (reservationToUpdate == null)
            {
                return NotFound();
            }

            var reservationMapped = _mapper.Map<Reservation>(reservation);

            try
            {
                _manager.UpdateReservation(reservationMapped);
            }
           catch(Exception)
                {
                    return BadRequest("Update reservation not allowed");
                }


            return Ok("Reservation updated");
        }

        // DELETE reservation
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _manager.GetReservation(id);
            if (reservation == null)
            {
                return NotFound();
            }

            try
            {
                _manager.DeleteReservation(reservation);
            }
            catch (Exception)
            {
                BadRequest("Delete reservation not allowed");
            }
            return Ok("Reservation deleted");

        }
    }
}
