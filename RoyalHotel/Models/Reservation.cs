using System;
using System.Collections.Generic;

namespace RoyalHotel.Models;

public partial class Reservation
{
    public decimal ReservationId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? HotelId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? ReservationDate { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Reservationdetail> Reservationdetails { get; set; } = new List<Reservationdetail>();

    public virtual User? User { get; set; }
}
