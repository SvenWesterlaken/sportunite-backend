using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace SportUnite.Domain
{
    class DateLessThanAttribute : ValidationAttribute
    {
        private readonly string _dateToCompare;

        public DateLessThanAttribute(string dateToCompare)
        {
            _dateToCompare = dateToCompare;
            

        }

        protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
        {
            var startDateTime = (DateTime) value;

            var property = validationContext.ObjectType.GetProperty(_dateToCompare);

            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
                
            }

            var comparisonValue = (DateTime) property.GetValue(validationContext.ObjectInstance);

            if (startDateTime > comparisonValue || startDateTime == comparisonValue)
            {
                return new ValidationResult(ErrorMessage = "Eindtijd en begintijd zijn niet geldig");
            }

            return ValidationResult.Success;

            


        }
    }
}
