using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Google.Rpc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public AppointmentController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var appointments = dbContext.ScheduledAppointments
                            .Include(x => x.User)
                            .Include(x => x.Treatment)
                            .ToList();

            return new JsonResult(appointments);
        }

        [HttpPost]
        public IActionResult Post(string email, int treatmentId, int day, int month, int year, int hour, int minute)
        {
            try
            {
                var date = new DateTime(year, month, day, hour, minute, 0);
                var user = dbContext.Users.FirstOrDefault(x => x.Email == email);
                var treatment = dbContext.Treatments.FirstOrDefault(x => x.Id == treatmentId);

                if (user == null || treatment == null)
                    return BadRequest("User and treatment must be known.");

                if (dbContext.ScheduledAppointments.Where(x => x.Date == date && x.Treatment.Id == treatment.Id).Any() || date.DayOfWeek == 0)
                {
                    return BadRequest("Unavailable");
                }

                var appointment = new ScheduledAppointment()
                {
                    User = user,
                    Treatment = treatment,
                    Date = date
                };

                dbContext.ScheduledAppointments.Add(appointment);
                dbContext.SaveChanges();

                return new JsonResult("Success");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("{userMail}")]
        public IActionResult GetAllForUser(string userMail)
        {
            var appointments = dbContext.ScheduledAppointments
                            .Include(x => x.User)
                            .Include(x => x.Treatment)
                            .Where(x => x.User.Email == userMail)
                            .ToList();

            return new JsonResult(appointments);
        }

        [HttpPost("{appointmentId}")]
        public IActionResult CancelAppointment(int appointmentId)
        {
            try
            {
                var appointment = dbContext.ScheduledAppointments
                          .Include(x => x.User)
                          .Include(x => x.Treatment)
                          .Where(x => x.Id == appointmentId)
                          .FirstOrDefault();

                dbContext.Remove(appointment);
                dbContext.SaveChanges();

                return new JsonResult(appointment);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }



    }
}
