using BeautySalonManagementSystem.Models;
using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Google.Rpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public AppointmentController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("scheduled")]
        [Authorize]
        public IActionResult GetAccepted()
        {
            var appointments = dbContext.ScheduledAppointments
                            .Include(x => x.User)
                            .Include(x => x.Treatment)
                            .Where(x => x.State == AppointmentState.ACCEPTED)
                            .OrderBy(x => x.Date)
                            .ToList();
            var appos = new List<Object>();
            foreach (var a in appointments)
            {
                appos.Add(new
                {
                    id = a.Id,
                    user = a.User,
                    date = a.Date.ToShortDateString(),
                    time = a.Date.ToShortTimeString(),
                    treatment = a.Treatment,
                    status = a.State.ToString()
                });
            }
            return new JsonResult(appos);
        }

        [HttpGet("required")]
        [Authorize]
        public IActionResult GetRequired()
        {
            var appointments = dbContext.ScheduledAppointments
                            .Include(x => x.User)
                            .Include(x => x.Treatment)
                            .Where(x => x.State == AppointmentState.REQUIRED)
                            .OrderBy(x => x.Date)
                            .ToList();
            var appos = new List<Object>();
            foreach (var a in appointments)
            {
                appos.Add(new
                {
                    id = a.Id,
                    user = a.User,
                    date = a.Date.ToShortDateString(),
                    time = a.Date.ToShortTimeString(),
                    treatment = a.Treatment,
                    status = a.State.ToString()
                });
            }
            return new JsonResult(appos);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AppointmentModel appointment)
        {
            try
            {
                var date = new DateTime(appointment.Year, appointment.Month, appointment.Day, appointment.Hour, appointment.Minute, 0);
                var user = dbContext.Users.FirstOrDefault(x => x.Email == appointment.Email);
                var treatment = dbContext.Treatments.FirstOrDefault(x => x.Id == appointment.TreatmentId);

                if (user == null || treatment == null)
                    return BadRequest("User and treatment must be known.");

                if (dbContext.ScheduledAppointments.Where(x => x.Date == date && x.Treatment.Id == treatment.Id).Any() || date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    return BadRequest("Unavailable");
                }

                var newAppointment = new ScheduledAppointment()
                {
                    User = user,
                    Treatment = treatment,
                    Date = date,
                    State = AppointmentState.REQUIRED
                };

                dbContext.ScheduledAppointments.Add(newAppointment);
                dbContext.SaveChanges();

                return new JsonResult("Success");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost("freeAppointmentTime/{treatmentId}")]
        [Authorize]
        public IActionResult GetFreeAppointmentTime([FromBody] DateModel dateTime, int treatmentId)
        {
            try
            {
                var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);

                var trApp = dbContext.ScheduledAppointments
                    .Include(x => x.Treatment)
                    .Where(x => x.Treatment.Id == treatmentId && x.Date.Date.CompareTo(date.Date) == 0)
                    .ToList();

                var wH = dbContext.WorkingHours.ToList();

                var freeTerms = new List<string>();

                if(!trApp.Any())
                {
                    foreach(var w in wH)
                    {
                        freeTerms.Add(w.Time);
                    }
                    return new JsonResult(freeTerms);
                }


                foreach(var h in wH)
                {
                    if(!trApp.Where(x => x.Date.ToShortTimeString().Equals(h.Time)).Any())
                    {
                        freeTerms.Add(h.Time);
                    }
                }

                return new JsonResult(freeTerms);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("accept")]
        [Authorize]
        public IActionResult AcceptAppointment([FromBody] int appointmentId)
        {
            try
            {
                var appointment = dbContext.ScheduledAppointments
                            .Include(x => x.User)
                            .Include(x => x.Treatment)
                            .Where(x => x.Id == appointmentId)
                            .FirstOrDefault();

                if (appointment == null)
                {
                    return BadRequest();
                }

                appointment.State = AppointmentState.ACCEPTED;

                dbContext.SaveChanges();

                return new JsonResult("Success");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("reject")]
        [Authorize]
        public IActionResult RejectAppointment([FromBody] int appointmentId)
        {
            try
            {
                var appointment = dbContext.ScheduledAppointments
                            .Include(x => x.User)
                            .Include(x => x.Treatment)
                            .Where(x => x.Id == appointmentId)
                            .FirstOrDefault();

                if (appointment == null)
                {
                    return BadRequest();
                }

                var notification = new Notification()
                {
                    User = appointment.User,
                    Message = $"{appointment.User.FirstName}, Vaš zahtev za zakazivnje termina " +
                    $"{appointment.Date.ToShortDateString()} u {appointment.Date.ToShortTimeString()} časova, " +
                    $"za uslugu {appointment.Treatment.Name.ToUpper()}, je odbijen. Molimo Vas da pokušate da" +
                    $" zakažete u nekom drugom terminu.",
                    State = NotificationState.UNREAD
                };

                dbContext.Notifications.Add(notification);

                dbContext.Remove(appointment);
                dbContext.SaveChanges();

                return new JsonResult("Success");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("{userMail}")]
        [Authorize]
        public IActionResult GetAllForUser(string userMail)
        {

            var appointments = dbContext.ScheduledAppointments
                            .Include(x => x.User)
                            .Include(x => x.Treatment)
                            .Where(x => x.User.Email == userMail)
                            .OrderBy(x => x.Date)
                            .ToList();

            var appos = new List<Object>();

            foreach (var a in appointments)
            {
                appos.Add(new
                {
                    id = a.Id,
                    user = a.User,
                    date = a.Date.ToShortDateString(),
                    time = a.Date.ToShortTimeString(),
                    treatment = a.Treatment,
                    status = a.State.ToString()
                });
            }
            return new JsonResult(appos);
        }

        [HttpDelete("{appointmentId}")]
        [Authorize]
        public IActionResult DeleteAppointment(int appointmentId)
        {
            try
            {
                var appointment = dbContext.ScheduledAppointments
                          .Include(x => x.User)
                          .Include(x => x.Treatment)
                          .Where(x => x.Id == appointmentId)
                          .FirstOrDefault();

                if (appointment == null)
                {
                    return BadRequest();
                }

                var notification = new Notification()
                {
                    User = appointment.User,
                    Message = $"{appointment.User.FirstName}, vaš termin {appointment.Date.ToShortDateString()}" +
                    $" u {appointment.Date.ToShortTimeString()} časova, za uslugu " +
                    $"{appointment.Treatment.Name.ToUpper()}, je otkazan. Molimo Vas da pokušate da zakažete" +
                    $" u nekom drugom terminu.",
                    State = NotificationState.UNREAD
                };

                dbContext.Notifications.Add(notification);

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
