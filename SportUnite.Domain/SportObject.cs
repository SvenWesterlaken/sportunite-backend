using System;
using System.Collections.Generic;
using System.Text;

namespace SportUnite.Domain
{
	public class SportObject
    {
	    public int SportObjectId { get; set; }
        public string Name { get; set;  }
	    public ICollection<SportObjectHall> SportObjectHalls { get; set; }
	    public ICollection<SportObjectSport> SportObjectSports { get; set; }
	}
}
