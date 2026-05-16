using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class PayrollDTO
    {
        public int PayrollId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select an employee")]

        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]

        public int Month { get; set; }

        [Range(2000, 2100, ErrorMessage = "Please enter a valid year")]

        public int Year { get; set; }

        public int WorkingDays { get; set; }

        public int PresentDays { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal NetSalary { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}