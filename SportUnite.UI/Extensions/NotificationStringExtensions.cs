using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportUnite.UI.Extensions
{
	public static class NotificationStringExtensions
	{
		public static string AddMessage(this string name, string type) => type + ", " + name + " is succesvol toegevoegd";

		public static string UpdateMessage(this string name, string type) => type + ", " + name + " is succesvol gewijzigd";

		public static string RemoveMessage(this string name, string type) => type + ", " + name + " is succesvol verwijderd";

		public static string InUseRemoveMessage(this string name, string type) => type + ", " + name + " kan niet verwijderd worden omdat deze nog in gebruik is";

		public static string ErrorRemoveMessage(this string name, string type) => type + ", " + name + " kon niet verwijderd worden";

		public static string ErrorUpdateMessage(this string name, string type) => type + ", " + name + " kon niet gewijzigd worden";

		public static string ErrorAddMessage(this string name, string type) => type + ", " + name + " kon niet toegevoegd worden";
	}
}
