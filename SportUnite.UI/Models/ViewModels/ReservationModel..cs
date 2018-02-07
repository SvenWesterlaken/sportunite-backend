using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportUnite.Domain;

namespace SportUnite.UI.Models.ViewModels
{
    public class ReservationModel
    {
        public IEnumerable<Hall> Halls { get; set; }
        public IEnumerable<SportEvent> SportEvents { get; set; }

        [Required]
        public Reservation Reservation { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; }

        public IEnumerable<SelectListItem> GetSportEvents()
        {
            return SportEvents
                .Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.SportEventId.ToString()


                });

        }

        public IEnumerable<SelectListItem> GetHalls()
        {
            return Halls
                .Select(e => new SelectListItem
                {
                    Text = e.Name,
                    Value = e.HallId.ToString()


                });

        }


    }


}

