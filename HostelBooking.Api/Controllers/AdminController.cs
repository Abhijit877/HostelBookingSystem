using HostelBooking.Application.Interfaces;
using HostelBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HostelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginRequest request)
        {
            try
            {
                var admin = await _adminService.LoginAsync(request.Email, request.Password);
                return Ok(new { message = "Admin login successful", admin = new { admin.Id, admin.Name, admin.Email } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("hostels")]
        public async Task<IActionResult> GetAllHostels()
        {
            try
            {
                var hostels = await _adminService.GetAllHostelsAsync();
                return Ok(hostels);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Admin endpoint is working");
        }
    }

    public class AdminLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
