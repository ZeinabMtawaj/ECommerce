using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class ProductGroup
    {
        public ProductGroup()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int DiscountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedAt { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
