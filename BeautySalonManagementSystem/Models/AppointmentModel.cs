namespace BeautySalonManagementSystem.Models
{
    public class AppointmentModel
    {
        public string Email { get; set; }
        public int TreatmentId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
