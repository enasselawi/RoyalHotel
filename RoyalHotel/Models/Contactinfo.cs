using System;
using System.Collections.Generic;

namespace RoyalHotel.Models;

public partial class Contactinfo
{
    public decimal Id { get; set; }

    public string? Location { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? OfficeHours { get; set; }
}
