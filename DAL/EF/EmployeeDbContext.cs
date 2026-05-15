using System;
using System.Collections.Generic;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public partial class EmployeeDbContext : DbContext
{
    public EmployeeDbContext()
    {
    }

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PayrollRecord> PayrollRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261CAB77BD7B");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Attendances_Employees");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED49AE19B6");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1105F8849C");

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534976DFFB0").IsUnique();

            entity.Property(e => e.BasicSalary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Employee");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Departments");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("PK__LeaveReq__609421EEAEA2964F");

            entity.Property(e => e.LeaveType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReviewedAt).HasColumnType("datetime");
            entity.Property(e => e.ReviewedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_LeaveRequests_Employees");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12A5BACDEB");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Notifications_Employees");
        });

        modelBuilder.Entity<PayrollRecord>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("PK__PayrollR__99DFC672E3944255");

            entity.Property(e => e.GeneratedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GrossSalary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NetSalary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.PayrollRecords)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_PayrollRecords_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
