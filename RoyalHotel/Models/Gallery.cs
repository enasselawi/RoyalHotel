using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalHotel.Models;

public partial class Gallery
{
    public decimal Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }
}
