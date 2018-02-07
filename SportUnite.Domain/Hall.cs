using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain {
    public class Hall : AbstractAttributes {
        [Key]
        public int HallId { get; set; }
        [Required(ErrorMessage = "Voeg een zaal naam toe")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Voeg een grootte toe")]
        public string Size { get; set; }
        [Required(ErrorMessage = "Voeg een prijs toe")]
        [Range(0, 1000, ErrorMessage = "Voeg een prijs toe tussen 0 en 1000")]
        public double Price { get; set; }
        [Required]
        public bool Available { get; set; }
        public int BuildingId { get; set; }
        public virtual Building Building { get; set; }
        public  List<HallOpeningHours> HallOpeningHours { get; set; } = new List<HallOpeningHours>();
        public ICollection<SportHall> SportHalls { get; set; }
        public ICollection<SportObjectHall> SportObjectHalls { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
