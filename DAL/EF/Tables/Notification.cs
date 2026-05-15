using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Role { get; set; }

    public string Message { get; set; } = null!;

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Employee? Employee { get; set; }
}
