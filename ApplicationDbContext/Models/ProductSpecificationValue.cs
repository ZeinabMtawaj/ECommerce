using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class ProductSpecificationValue
    {
        public int Id { get; set; }
        public string? Value { get; set; }
        public int? ProductId { get; set; }
        public int? SpecificationId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Specification? Specification { get; set; }
    }
}
