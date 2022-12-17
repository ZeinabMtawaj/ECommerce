using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductSpecificationValueViewModel: BaseEntity
    {
        public int Id { get; set; }
        public string? Value { get; set; }
        public int? ProductId { get; set; }
        public int? SpecificationId { get; set; }

        public virtual ProductViewModel? Product { get; set; }
        public virtual SpecificationViewModel? Specification { get; set; }
    }
}
