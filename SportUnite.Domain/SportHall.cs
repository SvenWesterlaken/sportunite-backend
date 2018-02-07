using System;
using System.Collections.Generic;
using System.Text;


namespace SportUnite.Domain
{
    public class SportHall
    {
	    public int SportId { get; set; }
	    public Sport Sport { get; set; }
	    public int HallId { get; set; }
	    public Hall Hall { get; set; }

    }
}
