using EmployeeCRUDApi.Models;
using EmployeeCRUDApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeCRUDApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

   
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                LoginRequest user = await _authService.GetByUserNameAndPassword(loginRequest.UserName, loginRequest.Password);

                if (user != null)
                {
                    // Create and return JWT token here if login is successful
                    string token = GenerateToken(loginRequest.UserName, loginRequest.Password);

                    return Ok(new { Token = token });
                }

                return Unauthorized();
            }

            return BadRequest(ModelState);
        }


        private string GenerateToken(string UserName, string Password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
             {
                  new Claim("name", UserName), // Add the UserName as a claim
                  new Claim("UserRole","user") // Add UserRole 
              };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
           


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
