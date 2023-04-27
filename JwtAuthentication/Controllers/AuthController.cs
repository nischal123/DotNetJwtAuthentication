using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthentication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
            {
                return BadRequest("null user");
            }


            if (user.Password == "nick")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));

                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


                string role = user.Username == "nick" ? "Manager" : "Admin";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, role)
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    //claims: new List<Claim>(),
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signInCredentials
                );

                var tokenstring = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthenticatedResponse { Token = tokenstring });
            }

            return Unauthorized();



        }
    }
}
