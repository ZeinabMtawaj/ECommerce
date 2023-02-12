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
            lambdas.Add(x => x.Category);

            var product_detail = _uow.ProductRepo.Find(x => x.Id == id, lambdas);
            var i = 0;
            Specification spec;
/*
            var lambdas_spec = new List<Expression<Func<ProductSpecificationValue, object>>>();
            lambdas_spec.Add(x => x.Specification);
            var lambdas_prod = new List<Expression<Func<Product, object>>>();
            lambdas_prod.Add(x => x.ProductSpecificationValues);
*/
            string name = "";
            if (product_detail.ProductSpecificationValues != null)
            {
                foreach (var item in product_detail.ProductSpecificationValues)
                {/*
                    name = "";
                    var prods = _uow.ProductRepo.FindAll(x => ((x.Name == product_detail.Name) && (x.ProductSpecificationValues !=null)), null, null, lambdas_prod);
*/
                    spec = _uow.SpecificationRepo.Find(x => x.Id == item.SpecificationId);
                    /*foreach (var prod in prods) {
                        var same_product = _uow.ProductSpecificationValueRepo.Find(x => (
                        (x.Specification != null) && (x.Specification.Name == spec.Name) && (x.ProductId == prod.Id))
                        , lambdas_spec);
                        if (same_product != null)
                        {
                            name += same_product.Value + "~";
                        }
                        
                    }

                    name = name.Remove(name.Length - 1, 1);*/
                    product_detail.ProductSpecificationValues.ElementAt(i).Specification = new Specification
                    {
                        Name =spec.Name
                    };
                    i++;

/*                    product_detail.ProductSpecificationValues.ElementAt(i).Value = name;
*/
                }
            }
            return View(product_detail);  
        
        
        }


        public IActionResult Index(bool match) {
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            var products = _uow.ProductRepo.FindAll(x => x.Id >= 1, 40, 0, lambdas);
            return View(products);
        
        }

        public JsonResult getProducts(bool match,int take, int skip) {
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            var products = _uow.ProductRepo.FindAll(x => x.Id>=1, take, skip, lambdas);
            products.ToList().ForEach(x => x.Category = null); 
            return Json(products);  
        
        
        }


       



        
    }
}
