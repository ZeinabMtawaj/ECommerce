using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class UserHasNotification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
