using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required")]

        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
    }
}