using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class UserHasNotificationViewModel: BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotificationId { get; set; }

        public virtual NotificationViewModel Notification { get; set; } = null!;
        public virtual UserViewModel User { get; set; } = null!;
    }
}
