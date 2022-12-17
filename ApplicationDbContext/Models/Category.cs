using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Category:BaseEntity
    {
        public Category()
        {
            CategorySpecificationValues = new HashSet<CategorySpecificationValue>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<CategorySpecificationValue> CategorySpecificationValues { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
