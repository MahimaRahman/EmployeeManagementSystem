using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;

        public string? Description { get; set; }
    }
}