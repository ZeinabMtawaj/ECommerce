using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductOrderViewModel: BaseEntity
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int? Quantity { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public decimal? Price { get; set; }

        public virtual OrderViewModel? Order { get; set; }
        public virtual ProductViewModel? Product { get; set; }
    }
}
