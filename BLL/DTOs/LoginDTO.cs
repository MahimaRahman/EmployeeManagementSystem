using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}