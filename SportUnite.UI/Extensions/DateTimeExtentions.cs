using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.UI.Extentions
{
    public static class DateTimeExtentions
    {
		public static string FormatDate(this DateTime dateTime)
		{
			return dateTime.ToString("d MMMM, yyyy");
		}

	    public static string FormatTime(this DateTime dateTime)
	    {
			return dateTime.ToString("HH:mm");
		}
	}
}
