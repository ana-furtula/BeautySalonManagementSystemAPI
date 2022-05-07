using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return new JsonResult(treatment);
        }

        [HttpPost]
        public IActionResult Post(string name, string description, double price)
        {
            try
            {
                dbContext.Treatments.Add(new Treatment() { Name = name, Description = description, Price = price });
                dbContext.SaveChanges();
                return new JsonResult("Success");
            }
            catch (Exception)
            {
                return new JsonResult("Failed");
            }

        }
    }
}
