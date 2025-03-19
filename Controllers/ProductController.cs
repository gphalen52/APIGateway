using Microsoft.AspNetCore.Mvc;

[Route("products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static readonly List<object> Products = new()
    {
        new { Id = 1, Name = "Laptop", Price = 1200 },
        new { Id = 2, Name = "Smartphone", Price = 800 },
        new { Id = 3, Name = "Tablet", Price = 500 }
    };

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(Products);
    }
}