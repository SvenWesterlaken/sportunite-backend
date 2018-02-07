using System;
using System.Collections.Generic;
using System.Text;

namespace SportUnite.Domain
{
	public class SportObjectSport
    {
	    public int SportObjectId { get; set; }
	    public SportObject SportObject { get; set; }
	    public int SportId { get; set; }
	    public Sport Sport { get; set; }
    }
}
