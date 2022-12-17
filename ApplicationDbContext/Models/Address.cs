using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Address:BaseEntity
    {
        public int Id { get; set; }
        public string Location { get; set; } = null!;
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Shipping? Shipping { get; set; }
    }
}
