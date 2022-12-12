using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
            UserHasNotifications = new HashSet<UserHasNotification>();
            WishLists = new HashSet<WishList>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<UserHasNotification> UserHasNotifications { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
