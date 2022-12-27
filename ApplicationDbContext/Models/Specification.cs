using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Specification: BaseEntity
    {
        public Specification()
        {

        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<CategorySpecificationValue>? CategorySpecificationValues { get; set; }
        public virtual ICollection<ProductSpecificationValue>? ProductSpecificationValues { get; set; }
    }
}
