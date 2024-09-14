using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalHotel.Models;

public partial class Homenavc
{
    public decimal Id { get; set; }

    public string? Heading1 { get; set; }

    public string? Heading2 { get; set; }

    public string? Description { get; set; }

    public string? BackgroundImage { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }

}
