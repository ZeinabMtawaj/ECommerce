using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Photo: BaseEntity
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public int ProductId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
