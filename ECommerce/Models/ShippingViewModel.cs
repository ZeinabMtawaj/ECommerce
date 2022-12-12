using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ShippingViewModel
    {
        public int Id { get; set; }
        public string? State { get; set; }
        public byte[]? Time { get; set; }
        public int AddressId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? OrderId { get; set; }

        public virtual AddressViewModel Address { get; set; } = null!;
        public virtual OrderViewModel? Order { get; set; }
    }
}
