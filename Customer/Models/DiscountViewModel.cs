using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class DiscountViewModel: BaseEntity
    {
        public DiscountViewModel()
        {
            ProductGroups = new HashSet<ProductGroupViewModel>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int? Active { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }

        public virtual ICollection<ProductGroupViewModel> ProductGroups { get; set; }
    }
}
