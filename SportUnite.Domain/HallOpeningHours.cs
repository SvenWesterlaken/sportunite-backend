using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain {
    public class HallOpeningHours
    {
        [Key]
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        [Key]
        public int OpeningHoursId { get; set; }
        public OpeningHours OpeningHours { get; set; }
    }
}
