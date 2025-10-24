using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoryCollab.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SampleController : ControllerBase
{
    [Authorize] // giriş şart
    [HttpGet("me")]
    public IActionResult Me() => Ok(new { ok = true });

    [Authorize(Roles = "Admin")] // sadece admin
    [HttpGet("admin-only")]
    public IActionResult AdminOnly() => Ok(new { secret = "top-secret" });
}
