using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class DepartmentSummaryData
    {
        public string DepartmentName { get; set; }

        public int EmployeeCount { get; set; }

        public decimal AverageSalary { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }
    }
}