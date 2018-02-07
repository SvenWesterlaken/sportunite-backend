using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportUnite.Domain
{
    public class Building : AbstractAttributes {


        [Key]
        public int BuildingId { get; set; }
        [Required(ErrorMessage = "Voeg een gebouwnaam toe")]
        public string Name { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
        public virtual Address Address { get; set; }
    }
}
