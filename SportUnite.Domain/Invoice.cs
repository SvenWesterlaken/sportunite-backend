using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportUnite.Domain
{
	public class Invoice
    {
	    public int InvoiceId { get; set; }
	    public double Price { get; set; }
	    public DateTime DateTime { get; set; }
	    public int ReservationId { get; set; }
		public Reservation Reservation { get; set; }
    }
}
