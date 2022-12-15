using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;

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


        public IActionResult Index()
        {
            return View();
        }
    }
}
