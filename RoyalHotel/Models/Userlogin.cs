using System;
using System.Collections.Generic;

namespace RoyalHotel.Models;

public partial class Userlogin
{
    public decimal LoginId { get; set; }

    public decimal? UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual User? User { get; set; }
}
