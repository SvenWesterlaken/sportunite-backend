using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportUnite.Domain
{
	public class Sport
	{
		[Key]
		public int SportId { get; set; }
		[Required(ErrorMessage = "Een sportnaam is verplicht")]
		public string Name { get; set; }
		public ICollection<SportHall> SportHalls { get; set; }
		public ICollection<SportObjectSport> SportObjectSports { get; set; }
		public ICollection<SportEvent> SportEvents { get; set; }
	}
}
