using System;
using System.Collections.Generic;

namespace RoyalHotel.Models;

public partial class Contactformsubmission
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public DateTime? SubmissionDate { get; set; }
}
