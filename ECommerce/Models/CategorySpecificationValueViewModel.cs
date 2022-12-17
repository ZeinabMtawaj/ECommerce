using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class CategorySpecificationValueViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Value { get; set; }

        public virtual CategoryViewModel? Category { get; set; }
        public virtual SpecificationViewModel? Specification { get; set; }
    }
}
