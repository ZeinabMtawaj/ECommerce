using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class TrendViewModel: BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }

        public virtual ProductViewModel Product { get; set; } = null!;
    }
}
