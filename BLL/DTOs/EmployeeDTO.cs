using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? Phone { get; set; }

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public DateOnly JoinDate { get; set; }

        [Range(1, 1000000)]
        public decimal BasicSalary { get; set; }

        [Required]
        public string Role { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}