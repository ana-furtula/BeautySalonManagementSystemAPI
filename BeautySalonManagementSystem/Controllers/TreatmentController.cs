using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
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
            return new JsonResult(treatment);
        }

        [HttpPost]
        public IActionResult Post([FromBody]  Treatment treatment)
        {
            try
            {
                dbContext.Treatments.Add(treatment);
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
