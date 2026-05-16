using System;
using System.ComponentModel.DataAnnotations;
using BLL.Validations;

namespace BLL.DTOs
{
    public class AttendanceDTO
    {
        public int AttendanceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select an employee")]
        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        [ValidAttendanceDate(ErrorMessage = "Attendance date is required")]
        public DateOnly AttendanceDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = null!;
    }
}