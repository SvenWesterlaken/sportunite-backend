using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace SportUnite.Domain
{
    public class CustomStartDateAttribute : RangeAttribute
    {
        public CustomStartDateAttribute() : base(typeof(DateTime), DateTime.Now.ToString(CultureInfo.CurrentCulture), DateTime.Now.AddYears(1).ToString(CultureInfo.CurrentCulture))
        {
        }

        
        public override string FormatErrorMessage(string name)
        {
            return name + " mag niet in het verleden liggen";
        }
    }
}
