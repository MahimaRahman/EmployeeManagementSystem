using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class LeaveRequestDTO
    {
        public int LeaveRequestId { get; set; }

        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public string LeaveType { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Reason { get; set; }

        public string Status { get; set; } = null!;

        public string? ReviewedBy { get; set; }

        public DateTime? ReviewedAt { get; set; }
    }
}