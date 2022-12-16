using System;
using System.Collections.Generic;

namespace ApplicationDbContext.Models
{
    public partial class Product: BaseEntity
    {
        public Product()
        {
            Photos = new HashSet<Photo>();
            ProductOrders = new HashSet<ProductOrder>();
            ProductSpecificationValues = new HashSet<ProductSpecificationValue>();
            Ratings = new HashSet<Rating>();
            WishLists = new HashSet<WishList>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        //public int? Quantity { get; set; }
        public string? Sku { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public int? ProductGroupId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ProductGroup? ProductGroup { get; set; }
        public virtual Trend? Trend { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<ProductSpecificationValue> ProductSpecificationValues { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
