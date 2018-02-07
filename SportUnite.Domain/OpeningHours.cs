using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain {
    public class OpeningHours : AbstractAttributes {

        [Key]
        public int OpeningHoursId { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public string OpeningTime { get; set; }
        [Required]
        public string ClosingTime { get; set; }
        public  List<HallOpeningHours> HallOpeningHours { get; set; } = new List<HallOpeningHours>();
    }
}
