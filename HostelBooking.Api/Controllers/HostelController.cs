using Microsoft.AspNetCore.Mvc;

namespace HostelBooking.Api.Controllers
{

    [Route("api/[controller]")]
    public class HostelController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "running" });
        }
    }
}
