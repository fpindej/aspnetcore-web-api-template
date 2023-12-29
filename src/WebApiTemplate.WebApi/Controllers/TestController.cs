using Microsoft.AspNetCore.Mvc;

namespace WebApiTemplate.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World!");
    }
    
    [HttpPost]
    public IActionResult Post()
    {
        return Ok("Hello World!");
    }
}