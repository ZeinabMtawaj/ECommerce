using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class SpecificationViewModel: BaseEntity
    {
        public SpecificationViewModel()
        {
            CategorySpecificationValues = new HashSet<CategorySpecificationValueViewModel>();
            ProductSpecificationValues = new HashSet<ProductSpecificationValueViewModel>();
        }
        public int Id { get; set; }


        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Maximum 20 characters")]
        public string? Name { get; set; }
        public virtual ICollection<CategorySpecificationValueViewModel> CategorySpecificationValues { get; set; }
        public virtual ICollection<ProductSpecificationValueViewModel> ProductSpecificationValues { get; set; }
    }
}
