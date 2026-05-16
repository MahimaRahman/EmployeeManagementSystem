using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BLL.Validations
{
    public class ValidLeaveEndDateAttribute : ValidationAttribute
    {
        private readonly string _startDateProperty;

        public ValidLeaveEndDateAttribute(string startDateProperty)
        {
            _startDateProperty = startDateProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDateProperty);

            if (startDateProperty == null)
            {
                return new ValidationResult("Invalid start date property");
            }

            var startDateValue = startDateProperty.GetValue(validationContext.ObjectInstance);

            if (startDateValue == null || value == null)
            {
                return new ValidationResult("Start date and end date are required");
            }

            DateOnly startDate = (DateOnly)startDateValue;
            DateOnly endDate = (DateOnly)value;

            if (startDate == default)
            {
                return new ValidationResult("Start date is required");
            }

            if (endDate == default)
            {
                return new ValidationResult("End date is required");
            }

            if (endDate < startDate)
            {
                return new ValidationResult(ErrorMessage ?? "End date cannot be before start date");
            }

            return ValidationResult.Success;
        }
    }
}