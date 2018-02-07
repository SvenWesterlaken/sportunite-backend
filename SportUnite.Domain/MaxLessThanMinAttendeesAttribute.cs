using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace SportUnite.Domain
{
    class MaxLessThanMinAttendeesAttribute : ValidationAttribute
    {
	    private readonly string _maxAttendees;

	    public MaxLessThanMinAttendeesAttribute(string maxAttendees)
	    {
		    _maxAttendees = maxAttendees;


	    }

	    protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
	    {
		    var minAttendees = (int)value;

		    var property = validationContext.ObjectType.GetProperty(_maxAttendees);

		    if (property == null)
		    {
			    throw new ArgumentException("Property with this name not found");

		    }

		    var comparisonValue = (int)property.GetValue(validationContext.ObjectInstance);

		    if (minAttendees > comparisonValue)
		    {
			    return new ValidationResult(ErrorMessage = "Max. aantal deelnemers mag" +
			                                               " niet lager zijn dan min. aantal deelnemers");
		    }

		    return ValidationResult.Success;




	    }
	}
}
