using System.Data;

namespace RoyalHotel.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Userlogin> UserLogins { get; set; }
        public IEnumerable<Hotel> Hotels { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public IEnumerable<Reservationdetail> ReservationDetails { get; set; }
        public IEnumerable<Useraccount> UserAccounts { get; set; }

        public IEnumerable<Contactformsubmission> Contactformsubmissions { get;  set; }

    }
}
