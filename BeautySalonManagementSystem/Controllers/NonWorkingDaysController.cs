using BeautySalonManagementSystem.Models;
using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonWorkingDaysController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public NonWorkingDaysController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var days = dbContext.NonWorkingDays.Select(x => x.Date).ToList();

            return new JsonResult(days);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DateModel data)
        {
            try
            {
                var date = new NonWorkingDay
                {
                    Date = new DateTime(year: data.Year, month: data.Month, day:data.Day)
                };

                dbContext.NonWorkingDays.Add(date);
                dbContext.SaveChanges();
                
                return new JsonResult("Successfully saved.");

            } catch (Exception)
            {
                return BadRequest();
            }
            
        }


        [HttpDelete]
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
