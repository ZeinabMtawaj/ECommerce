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

        public ProductController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public IEnumerable<SpecificationViewModel> GetAll()
        {
            var items = _uow.SpecificationRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<SpecificationViewModel>(item));
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

        public void Create(SpecificationViewModel obj)
        {
            var newObj = _mapper.Map<Specification>(obj);
            _uow.SpecificationRepo.Add(newObj);
            _uow.SaveChanges();
            return;
        }

        public void Edit(SpecificationViewModel obj)
        {
            var newObj = _mapper.Map<Specification>(obj);
            _uow.SpecificationRepo.Update(newObj);
            _uow.SaveChanges();
            return;
        }

        public void Delete(SpecificationViewModel obj)
        {
            if (_uow.Find() == null)
            {

                return;
            }
            var newObj = _mapper.Map<Specification>(obj);
            _uow.SpecificationRepo.Delete(newObj.Id);
            _uow.SaveChanges();
            return;
        }

        public SpecificationViewModel? Find(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return null;
            }
            var obj = _uow.SpecificationRepo.Find(id.Value);
            if (obj == null)
                return null;
            var res = _mapper.Map<SpecificationViewModel>(obj);
            return res;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
