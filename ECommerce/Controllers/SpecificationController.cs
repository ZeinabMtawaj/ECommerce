using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;
using System.Linq.Expressions;

namespace Ecommerce.Controllers
{
    public class SpecificationController : BaseController
    {

        public SpecificationController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public IEnumerable<SpecificationViewModel> GetAll()
        {
            var items = _uow.SpecificationRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<SpecificationViewModel>(item));
            return viewItems;
        }


        public List<string> GetColNames()
        {
            List<string> res = new List<string>();
            res.Add("Name");
            return res;
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
            var newObj = _mapper.Map<Specification>(obj);
            //var element = _uow.CategorySpecificationValueRepo.Find(x=>(x.SpecificationId==newObj.Id),new[] {"Specification"});
            //if (element != null)
            //    return;
            //var element2 = _uow.ProductSpecificationValueRepo.Find(x => (x.SpecificationId == newObj.Id), new[] { "Specification" });
            //if (element2 != null)
            //    return;
            var lambdas = new List<Expression<Func<Specification, object>>>();
            lambdas.Add(x => x.CategorySpecificationValues);
            lambdas.Add(x =>  x.ProductSpecificationValues);
            var element = _uow.SpecificationRepo.Find(x=>(x.Id==newObj.Id), lambdas );
            if ((element.CategorySpecificationValues.Count()==0) || (element.ProductSpecificationValues.Count()==0))
            {
                return;
            }
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
