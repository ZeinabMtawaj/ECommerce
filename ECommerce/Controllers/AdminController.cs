using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;

namespace Ecommerce.Controllers

{
    public class AdminController : BaseController
    {
        public AdminController(IUnitOfWork uow) : base(uow)
        {
        }

        public ProductViewModel convertToVM(Product item)
        {
//
            var product = new ProductViewModel();
            product.Name = item.Name;
            product.Description = item.Description;
            product.Price = item.Price;
            product.Sku = item.Sku;
            product.Quantity = item.Quantity;
            product.Image = item.Image;
            product.CreatedAt = item.CreatedAt;
            product.UpdatedAt = item.UpdatedAt; 


            return product;

        }
        public IActionResult ProductManagement()
        {
            //ProductController pc = new ProductController(_uow);

            var products =  _uow.ProductRepo.GetAll();
            var productsVM = new List<ProductViewModel>();
            foreach (var item in products)
            {
                var product = convertToVM(item);


                productsVM.Add(product);    
                    

            }

            return View(productsVM);
        }


        
    }
}
