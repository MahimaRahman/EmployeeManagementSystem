using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
