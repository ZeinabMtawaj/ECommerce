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
            /*         var lambdas_spec = new List<Expression<Func<ProductSpecificationValue, object>>>();
                     lambdas_spec.Add(x => x.Specification);*/
            var i = 0;
            Specification spec;
            var lambdas_spec = new List<Expression<Func<Specification, object>>>();
            lambdas.Add(x => x.ProductSpecificationValues);
            string name = "";
            if (product_detail.ProductSpecificationValues != null)
            {
                foreach (var item in product_detail.ProductSpecificationValues)
                {
                    name = "";
                    spec = _uow.SpecificationRepo.Find(x => x.Id == item.SpecificationId);
                    var Specs_for_same_product = _uow.SpecificationRepo.FindAll(x =>(
                    ( x.Name == spec.Name) && (x.ProductSpecificationValues !=null) &&(x.ProductSpecificationValues.Any(z=>z.ProductId==product_detail.Id))
                    ),null, null, lambdas_spec);

                    
                    foreach (var spec_same in Specs_for_same_product)
                    {
                        name +=spec_same.Name+"~";

                    }
                    name = name.Remove(name.Length - 1, 1);
                    product_detail.ProductSpecificationValues.ElementAt(i).Specification = new Specification
                    {
                        Name =name
                    };
                    i++;

                }
            }
            return View(product_detail);  
        
        
        }




       



        
    }
}
