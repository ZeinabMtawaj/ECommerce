using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class AddressViewModel: BaseEntity
    {
        public int Id { get; set; }
        public string Location { get; set; } = null!;
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }

        public virtual UserViewModel User { get; set; } = null!;
        public virtual ShippingViewModel? Shipping { get; set; }
    }
}
