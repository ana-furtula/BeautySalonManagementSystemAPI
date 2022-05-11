using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public RegisterController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                var userDb = dbContext.Users.FirstOrDefault(x => x.Email == user.Email);

                if (userDb != null)
                {
                    return new JsonResult("User already exists!");
                }
                else
                {
                    user.Role = Role.USER;
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return new JsonResult("Successful registration!");
                }
            }
            catch (Exception)
            {
                return new JsonResult("Failed");
            }

        }

    }
}