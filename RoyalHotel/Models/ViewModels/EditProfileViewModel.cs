using System.ComponentModel.DataAnnotations;
namespace RoyalHotel.Models.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]  // التحقق من صحة رقم الهاتف
        public string Phone { get; set; }
    }
}
