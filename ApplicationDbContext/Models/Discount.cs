using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Discount : BaseEntity
    {
        public Discount()
        {
            ProductGroups = new HashSet<ProductGroup>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int? Active { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
    }
}
