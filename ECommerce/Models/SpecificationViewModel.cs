using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class SpecificationViewModel
    {
        public SpecificationViewModel()
        {
            CategorySpecificationValues = new HashSet<CategorySpecificationValueViewModel>();
            ProductSpecificationValues = new HashSet<ProductSpecificationValueViewModel>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<CategorySpecificationValueViewModel> CategorySpecificationValues { get; set; }
        public virtual ICollection<ProductSpecificationValueViewModel> ProductSpecificationValues { get; set; }
    }
}
