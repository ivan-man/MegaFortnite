using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MegaFortnite.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("ok");
        }
    }
}
