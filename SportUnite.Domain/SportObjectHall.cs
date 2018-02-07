using System;
using System.Collections.Generic;
using System.Text;

namespace SportUnite.Domain
{
	public class SportObjectHall
    {
	    public int SportObjectId { get; set; }
	    public SportObject SportObject { get; set; }
	    public int HallId { get; set; }
	    public Hall Hall { get; set; }
    }
}
