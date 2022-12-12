using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductViewModel
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
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public int? Quantity { get; set; }
        public string? Sku { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public int? ProductGroupId { get; set; }

        public virtual CategoryViewModel? Category { get; set; }
        public virtual ProductGroupViewModel? ProductGroup { get; set; }
        public virtual TrendViewModel? Trend { get; set; }
        public virtual ICollection<PhotoViewModel> Photos { get; set; }
        public virtual ICollection<ProductOrderViewModel> ProductOrders { get; set; }
        public virtual ICollection<ProductSpecificationValueViewModel> ProductSpecificationValues { get; set; }
        public virtual ICollection<RatingViewModel> Ratings { get; set; }
        public virtual ICollection<WishListViewModel> WishLists { get; set; }
    }
}
