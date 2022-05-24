using BeautySalonManagementSystem.Models;
using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BeautySalonManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BeautySalonContext dbContext;
        private readonly IConfiguration config;

        public LoginController(BeautySalonContext dbContext, IConfiguration config)
        {
            this.dbContext = dbContext;
            this.config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginModel user)
        {
            IActionResult response = Unauthorized();

            var userDb = dbContext.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);

            if (userDb != null)
            {
                var token = GenerateJSONWebToken(user);
                response = Ok(new { token, user = new { userDb.Email, userDb.FirstName, userDb.LastName, Role = userDb.Role.ToString() } });
            }

            return response;

        }

        private string GenerateJSONWebToken(LoginModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}