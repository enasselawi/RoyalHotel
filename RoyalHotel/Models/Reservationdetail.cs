using System;
using System.Collections.Generic;

namespace RoyalHotel.Models;

public partial class Reservationdetail
{
    public decimal DetailId { get; set; }

    public decimal? ReservationId { get; set; }

    public decimal? RoomId { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public decimal? RoomPrice { get; set; }

    public virtual Reservation? Reservation { get; set; }

    public virtual Room? Room { get; set; }
}
