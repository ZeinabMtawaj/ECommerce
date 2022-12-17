using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class CategorySpecificationValue: BaseEntity
    {
        public int Id { get; set; }
        public string? Value { get; set; }
        public int? CategoryId { get; set; }
        public int? SpecificationId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Specification? Specification { get; set; }
    }
}
