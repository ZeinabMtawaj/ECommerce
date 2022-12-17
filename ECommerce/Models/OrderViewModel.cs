using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class OrderViewModel: BaseEntity
    {
        public OrderViewModel()
        {
            ProductOrders = new HashSet<ProductOrderViewModel>();
        }

        public int Id { get; set; }
        public string? State { get; set; }
        public decimal? Total { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CustomerId { get; set; }

        public virtual UserViewModel? Customer { get; set; }
        public virtual ShippingViewModel? Shipping { get; set; }
        public virtual ICollection<ProductOrderViewModel> ProductOrders { get; set; }
    }
}
