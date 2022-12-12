using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow) : base(uow)
        {

        }

        public IActionResult Index()
        {
            var a = _uow.ProductRepo.GetAll();
            ViewBag.Msg = "Hello from Index";
            // ViewData["Message"] = 

            TempData["Message"] = "Hello from Students Index (TempData)";
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
