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
        public int Id { get; set; }

        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required")]
        [Required(ErrorMessage = "Required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Maximum 20 characters")]
        public string? Name { get; set; }

        [RegularExpression(@"^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$", ErrorMessage = "Should be URL")]
        public string? Image { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Maximum 50 characters")]
        public string? Description { get; set; }
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<CategorySpecificationValueViewModel>? CategorySpecificationValues { get; set; }
        public virtual ICollection<ProductViewModel>? Products { get; set; }
    }
}
