using BeautySalonManagementSystem.Models;
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
        public IActionResult Post([FromBody] LoginModel user)
        {
            var userDb = dbContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (userDb == null)
            {
                return BadRequest("User does not exist!");
            }
            else
            {
                return new JsonResult(new { userDb.Email, userDb.FirstName, userDb.LastName, Role = userDb.Role.ToString() });
            }
        }
    }
}