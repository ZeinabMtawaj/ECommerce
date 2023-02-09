using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Customer.Models;
using Customer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Linq.Expressions;


namespace Customer.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public Product getCategory( int id)
        {
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            var item = _uow.ProductRepo.Find(x => x.Id == id, lambdas);

            return item;
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.ProductSpecificationValues);
            lambdas.Add(x => x.Photos);
            lambdas.Add(x => x.Ratings);
            var product_detail = _uow.ProductRepo.Find(x => x.Id == id, lambdas);

            return View(product_detail);  
        
        
        }


       



        
    }
}
