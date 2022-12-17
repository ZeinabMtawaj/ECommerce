using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;

namespace Ecommerce.Controllers
{
    public class ProductController : BaseController
    {
        private string[] includes = { "CategoryViewModel", "ProductGroupViewModel", "TrendViewModel" };

        public ProductController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            var items = _uow.ProductRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<ProductViewModel>(item));
            return viewItems;
        }


        public String[] GetColNames()
        {
            var names = typeof(Product).GetProperties()
                        .Select(property => property.Name)
                        .ToArray();
            //List<string> res = new List<string>();
            //res =["Name", "Price", "Image", "Quantity", "Sku", "Description", "CreatedDate", "UpdatedDate"];
            return names;
        }

        public void Create(ProductViewModel obj)
        {
            var newObj = _mapper.Map<Product>(obj);
            _uow.ProductRepo.Add(newObj);
            _uow.SaveChanges();
            return;
        }

        public void Edit(ProductViewModel obj)
        {
            var newObj = _mapper.Map<Product>(obj);
            _uow.ProductRepo.Update(newObj);
            _uow.SaveChanges();
            return;
        }

        public void Delete(ProductViewModel obj)
        {
            var newObj = _mapper.Map<Product>(obj);
            //var element = _uow.ProductRepo.Find(x => (x.Id == newObj.Id), includes);
            //if (element!=null)
            //{
            //    foreach (var include in includes) { 
            //        if (element==)
            //    }
            //    return;
            //}
            _uow.ProductRepo.Delete(newObj.Id);
            _uow.SaveChanges();
            return;
        }

        public ProductViewModel? Find(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return null;
            }
            var obj = _uow.ProductRepo.Find(id.Value);
            if (obj == null)
                return null;
            var res = _mapper.Map<ProductViewModel>(obj);
            return res;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
