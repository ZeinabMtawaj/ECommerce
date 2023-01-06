using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductViewModel: BaseEntity
    {
        public ProductViewModel()
        {
            Photos = new HashSet<PhotoViewModel>();
            ProductOrders = new HashSet<ProductOrderViewModel>();
            ProductSpecificationValues = new HashSet<ProductSpecificationValueViewModel>();
            Ratings = new HashSet<RatingViewModel>();
            WishLists = new HashSet<WishListViewModel>();
        }

        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Price must be number")]
        public decimal? Price { get; set; }
        //[Required]
        [Url]
        public string? Image { get; set; }
        [Required]
        [Remote(action: "IsSKUExist", controller:"ProductController", ErrorMessage = "SKU must be unique")]
        [Display(Name = "SKU")]

        public string? Sku { get; set; }

        [Display(Name="Category")]
        public int? CategoryId { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Maximum 200 characters")]
        public string? Description { get; set; }
        public int? ProductGroupId { get; set; }

        public virtual CategoryViewModel? Category { get; set; }
        public virtual ProductGroupViewModel? ProductGroup { get; set; }
        public virtual TrendViewModel? Trend { get; set; }
        public virtual ICollection<PhotoViewModel> Photos { get; set; }
        public virtual ICollection<ProductOrderViewModel> ProductOrders { get; set; }
        [Display(Name = "Specifications")]
        public virtual ICollection<ProductSpecificationValueViewModel> ProductSpecificationValues { get; set; }
        public virtual ICollection<RatingViewModel> Ratings { get; set; }
        public virtual ICollection<WishListViewModel> WishLists { get; set; }

        public virtual List<String>? SpecsId { get; set; }
        public virtual List<String>? SpecsValue { get; set; }

        public virtual List<String>? AdditionalPhoto { get; set; }

        //public virtual String? CategorName { get; set; }


    }
}
