using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class UserViewModel : BaseEntity
    {
        public UserViewModel()
        {
            Addresses = new HashSet<AddressViewModel>();
            Orders = new HashSet<OrderViewModel>();
            Ratings = new HashSet<RatingViewModel>();
            UserHasNotifications = new HashSet<UserHasNotificationViewModel>();
            WishLists = new HashSet<WishListViewModel>();
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
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Required")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? PhoneNumber { get; set; }

        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }


        public virtual ICollection<AddressViewModel> Addresses { get; set; }
        public virtual ICollection<OrderViewModel> Orders { get; set; }
        public virtual ICollection<RatingViewModel> Ratings { get; set; }
        public virtual ICollection<UserHasNotificationViewModel> UserHasNotifications { get; set; }
        public virtual ICollection<WishListViewModel> WishLists { get; set; }
    }
}
