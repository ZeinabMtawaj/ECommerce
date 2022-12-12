using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public int Rating1 { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ProductViewModel Product { get; set; } = null!;
        public virtual UserViewModel User { get; set; } = null!;
    }
}
