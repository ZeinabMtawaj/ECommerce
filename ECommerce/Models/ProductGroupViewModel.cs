using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductGroupViewModel: BaseEntity
    {
        public ProductGroupViewModel()
        {
            Products = new HashSet<ProductViewModel>();
        }

        public int Id { get; set; }
        public int DiscountId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public string UpdatedAt { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }

        public virtual DiscountViewModel Discount { get; set; } = null!;
        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}
