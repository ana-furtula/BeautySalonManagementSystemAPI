using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public LoginController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Post(string email, string password)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user == null)
            {
                return new JsonResult("Unknown user!");
            }
            else
            {
                return new JsonResult(new { user.Email, user.FirstName, user.LastName, Role = user.Role.ToString() });
            }
        }
    }
}