using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("gateway")]
[ApiController]
[Authorize] // Protects all endpoints in this controller
public class GatewayController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public GatewayController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("{service}/{*endpoint}")]
    public async Task<IActionResult> RouteRequest(string service, string endpoint)
    {
        var serviceUrls = new Dictionary<string, string>
        {
            { "serviceA", "http://localhost:5001" },
            { "serviceB", "http://localhost:5002" }
        };

        if (!serviceUrls.ContainsKey(service))
        {
            return BadRequest("Invalid service name.");
        }

        string targetUrl = $"{serviceUrls[service]}/{endpoint}";

        try
        {
            var response = await _httpClient.GetAsync(targetUrl);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, response.Content.Headers.ContentType?.ToString() ?? "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}