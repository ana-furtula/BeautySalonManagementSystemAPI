using BeautySalonManagementSystem.Models;
using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NonWorkingDaysController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public NonWorkingDaysController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var days = dbContext.NonWorkingDays.Select(x => x.Date).ToList();
            var dates = new List<Object>();
            foreach (var day in days)
            {
                dates.Add(new
                {
                    Month = day.Month,
                    Year = day.Year,
                    Day = day.Day
                });
            }

            return new JsonResult(dates);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] DateModel data)
        {
            try
            {
                var date = new NonWorkingDay
                {
                    Date = new DateTime(year: data.Year, month: data.Month, day:data.Day)
                };

                if (dbContext.ScheduledAppointments.Where(x => x.Date.Date.CompareTo(date.Date.Date)==0).Any())
                {
                    return BadRequest("There are appointments scheduled or waiting to be scheduled for the chosen day.");
                }

                dbContext.NonWorkingDays.Add(date);
                dbContext.SaveChanges();
                
                return new JsonResult("Successfully saved.");

            } catch (Exception)
            {
                return BadRequest();
            }
            
        }


        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromBody] DateModel data)
        {
            try
            {
                var date = new DateTime(year: data.Year, month: data.Month, day: data.Day);
                var dateFromDb = dbContext.NonWorkingDays.Where(x => x.Date == date).FirstOrDefault();
                if (dateFromDb != null)
                {
                    dbContext.NonWorkingDays.Remove(dateFromDb);
                    dbContext.SaveChanges();
                }

                return new JsonResult("Successfully deleted.");

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

    }
}
