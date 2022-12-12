using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Notification
    {
        public Notification()
        {
            UserHasNotifications = new HashSet<UserHasNotification>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Title { get; set; }

        public virtual ICollection<UserHasNotification> UserHasNotifications { get; set; }
    }
}
