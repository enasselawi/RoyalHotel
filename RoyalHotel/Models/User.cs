using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalHotel.Models;

public partial class User
{
    public decimal UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public decimal? RoleId { get; set; }

    public string? ImageUrl { get; set; }
  

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Useraccount> Useraccounts { get; set; } = new List<Useraccount>();

    public virtual ICollection<Userlogin> Userlogins { get; set; } = new List<Userlogin>();
    public virtual ICollection<Testimonials> Testimonials { get; set; } = new List<Testimonials>(); // Add this line


}
