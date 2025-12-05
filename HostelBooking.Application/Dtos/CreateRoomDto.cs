using HostelBooking.Domain.Enums;

namespace HostelBooking.Application.Dtos
{
    public class CreateRoomDto
    {
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int HostelId { get; set; }
    }
}
