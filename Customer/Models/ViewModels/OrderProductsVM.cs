using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Customer.Models;

namespace Customer.Models.ViewModels
{
    public class OrderProductsVM
    {
        public List<Product> Products { get; set; }
       // public List<ProductOrder> ProductsOrders { get; set; }

        public List<Order> Orders { get; set; }

    }
}
