using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class ProfileViewModel : BaseEntity
    {
        public ProfileViewModel()
        {
        }

        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [Required(ErrorMessage = "Required")]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Maximum 10 characters")]
        public string? FirstName { get; set; }
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Maximum 10 characters")]

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public string? Password { get; set; }
        [Display(Name ="Old Password")]
        public string? OldPassword { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name ="Phone Number")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? PhoneNumber { get; set; }

        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
