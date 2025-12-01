using Microsoft.AspNetCore.Mvc;

namespace HostelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
    }
}

namespace HostelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostelController : ControllerBase
    {
    }
}

namespace HostelBooking.Infrastructure.Interfaces
{
    public interface IHostelRepository
    {
    }
}

namespace HostelBooking.Domain.Entities