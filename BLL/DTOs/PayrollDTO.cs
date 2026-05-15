using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class PayrollDTO
    {
        public int PayrollId { get; set; }

        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int WorkingDays { get; set; }

        public int PresentDays { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal NetSalary { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}