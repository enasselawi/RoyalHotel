using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalHotel.Models;

public partial class Hotel
{
    public decimal HotelId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? Contact { get; set; }

    public string? ImageUrl { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }


    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

}
