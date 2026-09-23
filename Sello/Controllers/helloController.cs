using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class helloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello, World!");
        }
    }
}
