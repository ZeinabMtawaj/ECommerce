using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Rating: BaseEntity
    {
        public int Id { get; set; }
        public int Rating1 { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
