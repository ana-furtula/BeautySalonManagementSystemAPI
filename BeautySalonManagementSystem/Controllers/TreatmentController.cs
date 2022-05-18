using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public TreatmentController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var treatments = dbContext.Treatments.ToList();
            return new JsonResult(treatments);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var treatment = dbContext.Treatments.FirstOrDefault(x => x.Id == id);
            if(treatment == null)
            {
                return BadRequest("Treatment does not exist.");
            }
            return new JsonResult(treatment);
        }

        [HttpPost]
        public IActionResult Post([FromBody]  Treatment treatment)
        {
            try
            {
                dbContext.Treatments.Add(treatment);
                dbContext.SaveChanges();
                return new JsonResult(treatment);
            }
            catch (Exception)
            {
                return BadRequest("Server failed to add new treatment. Please, try again!");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var t = dbContext.Treatments.Where(t => t.Id == id).FirstOrDefault();
                dbContext.Remove(t);
                dbContext.SaveChanges();
                return new JsonResult("Successfuly deleted.");
            }
            catch (Exception)
            {
                return BadRequest("Server failed to delete treatment. Please, try again!");
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] Treatment treatment)
        {
            try
            {
                var t = dbContext.Treatments.Where(t => t.Id == treatment.Id).FirstOrDefault();
                t.Name = treatment.Name;
                t.Price = treatment.Price;
                t.Description = treatment.Description;
                
                dbContext.SaveChanges();
                return new JsonResult("Success");
            }
            catch (Exception)
            {
                return BadRequest("Server failed to update treatment. Please, try again!");
            }

        }
    }
}
