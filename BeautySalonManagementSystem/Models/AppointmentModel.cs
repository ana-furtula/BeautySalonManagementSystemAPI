using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonManagementSystem.Models
{
    public class AppointmentModel
    {
        [Email]
        [Required]
        public string Email { get; set; }
        public int TreatmentId { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Hour { get; set; }
        [Required]
        public int Minute { get; set; }
    }
}
