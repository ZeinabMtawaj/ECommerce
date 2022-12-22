using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class CategoryViewModel: BaseEntity
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

        [Url]
        //[RegularExpression(@"^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$", ErrorMessage = "Should be URL")]
        public string? Image { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Description must be between 3 and 200 characters", MinimumLength =3)]
        public string? Description { get; set; }


        [Display(Name = "Specifications")]
        public virtual ICollection<CategorySpecificationValueViewModel>? CategorySpecificationValues { get; set; }
        public virtual ICollection<ProductViewModel>? Products { get; set; }
    }
}
