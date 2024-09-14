namespace RoyalHotel.Models.ViewModels
{
    public class PaymentViewModel
    {

        public int RoomId { get; set; }
        public decimal RoomPrice { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalCost { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
    }
}
