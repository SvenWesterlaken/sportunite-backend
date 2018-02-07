using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain {
    public class Address : AbstractAttributes {
        [Key]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Voeg een straatnaam toe")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "Voeg een huisnummer toe")]
        public int HouseNumber { get; set; }
        public string Suffix { get; set; }
        [Required(ErrorMessage = "Voeg een postcode toe")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Voeg een stad toe")]
        public string City { get; set; }
        [Required(ErrorMessage = "Voeg een provincie toe")]
        public string State { get; set; }
        [Required(ErrorMessage = "Voeg een land toe")]
        public string Country { get; set; }
        public virtual int BuildingId { get; set; }
		public virtual Building Building { get; set; }
    }
}
