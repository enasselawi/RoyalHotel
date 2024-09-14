using System.ComponentModel.DataAnnotations;

namespace RoyalHotel.Models
{
    public class Testimonials
    {

        public decimal TestimonialId { get; set; }
        public decimal? UserId { get; set; }
        public string? Content { get; set; }
        public string? IsApproved { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
