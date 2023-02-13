using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class User : IdentityUser<int>
    {
        public User() : base()
        {
            Addresses = new HashSet<Address>();
            Orders = new HashSet<Order>();
            Ratings = new HashSet<Rating>();
            UserHasNotifications = new HashSet<UserHasNotification>();
            WishLists = new HashSet<WishList>();
        }

        //public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }



        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<UserHasNotification> UserHasNotifications { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
