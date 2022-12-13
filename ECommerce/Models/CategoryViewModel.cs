using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            CategorySpecificationValues = new HashSet<CategorySpecificationValueViewModel>();
            Products = new HashSet<ProductViewModel>();
        }

        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<CategorySpecificationValueViewModel> CategorySpecificationValues { get; set; }
        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}
