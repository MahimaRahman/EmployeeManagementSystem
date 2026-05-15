using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class PayrollRecord
{
    public int PayrollId { get; set; }

    public int EmployeeId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public int WorkingDays { get; set; }

    public int PresentDays { get; set; }

    public decimal GrossSalary { get; set; }

    public decimal NetSalary { get; set; }

    public DateTime GeneratedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
