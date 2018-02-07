using System.Collections.Generic;

namespace SportUnite.Api.Models.ViewModels.HallApi
{
    public class HallDTO
    {
	    
	    public int HallId { get; set; }
	 
	    public string Name { get; set; }
	  
	    public string Size { get; set; }
	 
	    public double Price { get; set; }
	    public bool Available { get; set; }
	    public int BuildingId { get; set; }
	    public List<OpeningHoursDTO> OpeningHours { get; set; } = new List<OpeningHoursDTO>();
	    public IEnumerable<ReservationDTO> Reservations;
	    public IEnumerable<SportDTO> Sports { get; set; }



    }
}
