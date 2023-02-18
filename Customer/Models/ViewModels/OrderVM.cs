using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Customer.Models;

namespace Customer.Models.ViewModels
{
    public class OrderVM
    {
        public List<Product> Products { get; set; }
        public List<ProductOrder> ProductsOrder { get; set; }

    }
}
