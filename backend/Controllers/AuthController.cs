using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly string? _jwtSecret;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _jwtSecret = config["Jwt:Secret"];
        }

        [HttpPost("signup")]
        public IActionResult SignUp(Users user)
        {
            // Check if the username or email already exists
            if (_userService.UserExists(user.Email, user.Email))
            {
                return Conflict(new { message = "Username or email already exists" });
            }

            // Add user to database
            _userService.AddUser(user);

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Return user id, initials, and access token
            var response = new
            {
                UserId = user.Id,
                Initials = $"{user.FirstName[0]}{user.LastName[0]}",
                AccessToken = token
            };

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var user = _userService.Authenticate(loginModel.Email, loginModel.Password);

            if (user == null)
                return Unauthorized();

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Return user id, initials, and access token
            var response = new
            {
                UserId = user.Id,
                Initials = $"{user.FirstName[0]}{user.LastName[0]}",
                AccessToken = token
            };

            return Ok(response);
        }

        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
