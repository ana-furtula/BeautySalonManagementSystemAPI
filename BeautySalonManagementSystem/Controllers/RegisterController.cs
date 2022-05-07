using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;

        public RegisterController(BeautySalonContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Post(string email, string password, string firstName, string lastName)
        {
            try
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Email == email);

                if (user != null)
                {
                    return new JsonResult("User already exists!");
                }
                else
                {
                    dbContext.Users.Add(new User() { Email = email, Password = password, FirstName = firstName, LastName = lastName, Role = Role.USER });
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