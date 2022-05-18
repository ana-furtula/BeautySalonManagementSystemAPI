using DataAnnotationsExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Email]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
