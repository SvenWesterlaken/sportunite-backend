using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace SportUnite.Domain
{

  
    public class Reservation : AbstractAttributes
    {
	    public int ReservationId { get; set; }
	
        [CustomStartDate]
        [DateLessThan("TimeFinish")]
		[Required(ErrorMessage = "Begintijd is vereist")]
        public DateTime StartTime { get; set; }

        [CustomStartDate]
		[Required(ErrorMessage = "Eindtijd is vereist")]
        public DateTime TimeFinish { get; set; }

        
        [Required]
	    public bool Definite { get; set; }
 
        [Range(1, int.MaxValue)]
        [Required]
	    public int SportEventId { get; set; }
    
	    public SportEvent SportEvent { get; set; }
      
	    public int HallId { get; set; }	
	    public Hall Hall { get; set; }
	    public Invoice Invoice { get; set; }

    

    }
}
