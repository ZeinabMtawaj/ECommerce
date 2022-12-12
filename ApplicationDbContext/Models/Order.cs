using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }
        public string? State { get; set; }
        public decimal? Total { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CustomerId { get; set; }

        public virtual User? Customer { get; set; }
        public virtual Shipping? Shipping { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
