using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}