using HostelBooking.Application.Dtos;
using HostelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HostelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostelController : ControllerBase
    {
        private readonly IHostelService _hostelService;

        public HostelController(IHostelService hostelService)
        {
            _hostelService = hostelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHostels()
        {
            var hostels = await _hostelService.GetAllHostelsAsync();
            return Ok(hostels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHostelById(int id)
        {
            var hostel = await _hostelService.GetHostelByIdAsync(id);
            if (hostel == null)
                return NotFound();

            return Ok(hostel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHostel([FromBody] CreateHostelDto hostelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var hostel = await _hostelService.CreateHostelAsync(hostelDto);
                return CreatedAtAction(nameof(GetHostelById), new { id = hostel.Id }, hostel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHostel(int id, [FromBody] UpdateHostelDto hostelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _hostelService.UpdateHostelAsync(id, hostelDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHostel(int id)
        {
            try
            {
                await _hostelService.DeleteHostelAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
