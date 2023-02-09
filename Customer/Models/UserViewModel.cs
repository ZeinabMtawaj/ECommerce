using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
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

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<AddressViewModel> Addresses { get; set; }
        public virtual ICollection<OrderViewModel> Orders { get; set; }
        public virtual ICollection<RatingViewModel> Ratings { get; set; }
        public virtual ICollection<UserHasNotificationViewModel> UserHasNotifications { get; set; }
        public virtual ICollection<WishListViewModel> WishLists { get; set; }
    }
}
