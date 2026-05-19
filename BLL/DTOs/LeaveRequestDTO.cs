using System;
using System.ComponentModel.DataAnnotations;
using BLL.Validations;

namespace BLL.DTOs
{
    public class LeaveRequestDTO
    {
        public int LeaveRequestId { get; set; }

        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "Leave type is required")]
        public string LeaveType { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        [ValidLeaveEndDate("StartDate", ErrorMessage = "End date cannot be before start date")]
        public DateOnly EndDate { get; set; }

        [Required]
        public string? Reason { get; set; }

        public string Status { get; set; } = "Pending";

        public string? ReviewedBy { get; set; }

        public DateTime? ReviewedAt { get; set; }
    }
}