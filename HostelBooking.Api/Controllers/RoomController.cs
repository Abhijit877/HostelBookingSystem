using HostelBooking.Application.Dtos;
using HostelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HostelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto roomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var room = await _roomService.CreateRoomAsync(roomDto);
                return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] UpdateRoomDto roomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _roomService.UpdateRoomAsync(id, roomDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
