using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class DepartmentSummaryDTO
    {
        public string DepartmentName { get; set; } = null!;

        public int EmployeeCount { get; set; }

        public decimal AverageSalary { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }
    }
}