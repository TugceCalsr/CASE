using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    [Authorize]
    [HttpGet("secure-data")]
    public IActionResult GetSecureData()
    {
        return Ok("Geldim mi:)");
    }
}
