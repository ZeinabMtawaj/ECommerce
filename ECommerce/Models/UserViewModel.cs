using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class UserViewModel: BaseEntity
    {
        public UserViewModel()
        {
            Addresses = new HashSet<AddressViewModel>();
            Orders = new HashSet<OrderViewModel>();
            Ratings = new HashSet<RatingViewModel>();
            UserHasNotifications = new HashSet<UserHasNotificationViewModel>();
            WishLists = new HashSet<WishListViewModel>();
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email {get; set; }

        public string? Password { get; set; } 

        public string? PhoneNumber { get; set; }
        
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }

        public string? Photo { get; set; }

        public virtual ICollection<AddressViewModel> Addresses { get; set; }
        public virtual ICollection<OrderViewModel> Orders { get; set; }
        public virtual ICollection<RatingViewModel> Ratings { get; set; }
        public virtual ICollection<UserHasNotificationViewModel> UserHasNotifications { get; set; }
        public virtual ICollection<WishListViewModel> WishLists { get; set; }
    }
}
