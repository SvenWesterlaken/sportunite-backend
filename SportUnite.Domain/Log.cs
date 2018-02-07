using System;
using System.Collections.Generic;
using System.Text;

namespace SportUnite.Domain
{
	public class Log
    {
	    public int LogId { get; set; }
	    public DateTime Date { get; set; }
	    public string User { get; set; }
	    public string Action { get; set; }
    }
}
