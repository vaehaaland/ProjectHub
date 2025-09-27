using Microsoft.AspNetCore.Mvc;

namespace ProjectHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ServerController : ControllerBase
{
  [HttpGet("health")]
  [HttpGet("/health")] // holder på kravet om GET /health
  public IActionResult Health() => Content("OK", "text/plain");
}
