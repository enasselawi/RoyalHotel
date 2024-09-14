using System.ComponentModel.DataAnnotations;

namespace RoyalHotel.Models.ViewModels
{
    public class SearchViewModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int HotelId { get; set; }
        public List<Room> AvailableRooms { get; set; }
    }


}