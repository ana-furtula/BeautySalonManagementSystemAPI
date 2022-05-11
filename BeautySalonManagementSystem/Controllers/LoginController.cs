using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public LoginController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var userDb = dbContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                return new JsonResult(new { user.Email, user.FirstName, user.LastName, Role = user.Role.ToString() });
            }
        }
    }
}