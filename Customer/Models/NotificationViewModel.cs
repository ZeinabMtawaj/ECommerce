using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class NotificationViewModel: BaseEntity
    {
        public NotificationViewModel()
        {
            UserHasNotifications = new HashSet<UserHasNotificationViewModel>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Content { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        public string? Title { get; set; }

        public virtual ICollection<UserHasNotificationViewModel> UserHasNotifications { get; set; }
    }
}
