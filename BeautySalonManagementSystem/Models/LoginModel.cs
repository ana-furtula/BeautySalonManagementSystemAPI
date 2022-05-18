using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonManagementSystem.Models
{
    public class LoginModel
    {
        [Email]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
