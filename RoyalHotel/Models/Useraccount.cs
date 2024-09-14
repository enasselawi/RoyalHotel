using System;
using System.Collections.Generic;

namespace RoyalHotel.Models;

public partial class Useraccount
{
    public decimal AccountId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? Balance { get; set; }
    public string CardNumber { get; set; }  // 16-digit card number
    public DateTime ExpiryDate { get; set; }  // Expiry date of the card
    public string CVV { get; set; }  // 3-4 digit security code

    public virtual User? User { get; set; }
}
