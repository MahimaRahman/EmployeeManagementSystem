using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class AttendanceDTO
    {
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public DateOnly AttendanceDate { get; set; }

        public string Status { get; set; } = null!;
    }
}