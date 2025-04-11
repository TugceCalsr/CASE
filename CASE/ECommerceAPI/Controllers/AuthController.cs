using ECommerceAPI.Helpers;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Demo amaçlı sabit kullanıcı kontrolü
            if (request.Username == "admin" && request.Password == "1234")
            {
                var token = JwtHelper.GenerateJwtToken(request.Username, _config);
                return Ok(new { token });
            }

            return Unauthorized("Kullanıcı adı veya şifre hatalı.");
        }
    }
}
