using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Validations
{
    public class ValidAttendanceDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Attendance date is required");
            }

            DateOnly attendanceDate = (DateOnly)value;

            if (attendanceDate == default)
            {
                return new ValidationResult(ErrorMessage ?? "Attendance date is required");
            }

            if (attendanceDate > DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("Attendance date cannot be in the future");
            }

            return ValidationResult.Success;
        }
    }
}