using System;
using System.Collections.Generic;

namespace RoyalHotel.Models;

public partial class Room
{
    public decimal RoomId { get; set; }

    public decimal? HotelId { get; set; }

    public string? RoomType { get; set; }

    public decimal? Price { get; set; }

    public decimal? Capacity { get; set; }

    public string? Booked { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<Reservationdetail> Reservationdetails { get; set; } = new List<Reservationdetail>();
}
