using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;

namespace Ecommerce.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
            
        }


        public IEnumerable<CategoryViewModel> GetAll()
        {
            var items = _uow.CategoryRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<CategoryViewModel>(item));
            return viewItems;
        }

        public IEnumerable<String> GetColNames()
        {
            IEnumerable<String> res = new List<String>();
            res.Append("Name");
            return res;
        }



        public IActionResult Index()
        {   
            var items = _uow.CategoryRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<CategoryViewModel>(item));
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                //_uow.ProductRepo.Add( pvm );
                //_uow.SaveChanges();
                return RedirectToAction("Index");
            }
                return View(pvm);
           
        }


        public IActionResult Edit(int id)
        {
            var obj = _uow.ProductRepo.Get(id);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                //_uow.ProductRepo.Update( pvm );
                //_uow.SaveChanges();
                return RedirectToAction("Index");
            }

                return View(pvm);
                        
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _uow.ProductRepo.Delete(id);
            return RedirectToAction("Index");

        }
    }
}
