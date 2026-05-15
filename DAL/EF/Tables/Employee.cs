using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public int DepartmentId { get; set; }

    public DateOnly JoinDate { get; set; }

    public decimal BasicSalary { get; set; }

    public string Role { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PayrollRecord> PayrollRecords { get; set; } = new List<PayrollRecord>();
}
