using System;
using System.Collections.Generic;
using System.Text;

namespace SportUnite.Domain
{
	public class AbstractAttributes
    {

	    public DateTime? DeletedAt { get; set; }

		public DateTime ModifiedAt { get; set; }

		public DateTime CreatedAt { get; set; }
    }
}
