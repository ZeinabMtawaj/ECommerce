using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Specification: BaseEntity
    {
        public Specification()
        {
            CategorySpecificationValues = new HashSet<CategorySpecificationValue>();
            ProductSpecificationValues = new HashSet<ProductSpecificationValue>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<CategorySpecificationValue>? CategorySpecificationValues { get; set; }
        public virtual ICollection<ProductSpecificationValue>? ProductSpecificationValues { get; set; }
    }
}
