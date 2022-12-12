using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Shipping
    {
        public int Id { get; set; }
        public string? State { get; set; }
        public byte[]? Time { get; set; }
        public int AddressId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? OrderId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}
