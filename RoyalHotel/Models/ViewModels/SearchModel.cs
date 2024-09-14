using System.ComponentModel.DataAnnotations;

namespace RoyalHotel.Models.ViewModels
{
    public class SearchModel
    {
        [Required]
        [Display(Name = "Check-in Date")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Display(Name = "Check-out Date")]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }

        public List<Room> AvailableRooms { get; set; } = new List<Room>();
    }
}
