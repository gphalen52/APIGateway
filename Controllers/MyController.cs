[EnableRateLimiting]
[Route("api/[controller]")]
[ApiController]
public class MyController : ControllerBase
{
    [HttpGet("limited")]
    public IActionResult GetLimitedData()
    {
        return Ok("This request is rate-limited!");
    }
}