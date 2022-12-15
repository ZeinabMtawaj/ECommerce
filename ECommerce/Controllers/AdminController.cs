using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;

namespace Ecommerce.Controllers

{
    public class AdminController : BaseController
    {

        public AdminController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }


        public IActionResult CategoryManagement()
        {
            var categoryContorller = new CategoryController(_uow, _mapper);
            var items = categoryContorller.GetAll();
            ViewBag.cols = categoryContorller.GetColNames();
            ViewBag.createController = "Admin";
            ViewBag.createAction = "CreateCategory";
            ViewBag.editController = "Admin";
            ViewBag.editAction = "EditCategory";
            ViewBag.deleteController = "Admin";
            ViewBag.deleteAction = "DeleteCategory";
            return View(items);
        }


        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var categoryContorller = new CategoryController(_uow, _mapper);
                categoryContorller.Create(obj);
                return RedirectToAction("CategoryManagement");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            var categoryContorller = new CategoryController(_uow, _mapper);
            var obj = categoryContorller.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var categoryContorller = new CategoryController(_uow, _mapper);
                categoryContorller.Edit(obj);
                return RedirectToAction("CategoryManagement");
            }
            return View(obj);
        }


        [HttpGet]
        public IActionResult DeleteCategory(int? id)
        {
            var categoryContorller = new CategoryController(_uow, _mapper);
            var obj = categoryContorller.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var categoryContorller = new CategoryController(_uow, _mapper);
                categoryContorller.Delete(obj);
                return RedirectToAction("CategoryManagement");
            }
            return View(obj);
        }













        public IActionResult SpecificationManagement()
        {
            var specificationContorller = new SpecificationController(_uow, _mapper);
            var items = specificationContorller.GetAll();
            ViewBag.cols = specificationContorller.GetColNames();
            ViewBag.createController = "Admin";
            ViewBag.createAction = "CreateSpecification";
            ViewBag.editController = "Admin";
            ViewBag.editAction = "EditSpecification";
            ViewBag.deleteController = "Admin";
            ViewBag.deleteAction = "DeleteSpecification";
            return View(items);
        }



        [HttpGet]
        public IActionResult CreateSpecification()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSpecification(SpecificationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var specificationContorller = new SpecificationController(_uow, _mapper);
                specificationContorller.Create(obj);
                return RedirectToAction("SpecificationManagement");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult EditSpecification(int? id)
        {
            var specificationContorller = new SpecificationController(_uow, _mapper);
            var obj = specificationContorller.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditSpecification(SpecificationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var specificationContorller = new SpecificationController(_uow, _mapper);
                specificationContorller.Edit(obj);
                return RedirectToAction("SpecificationManagement");
            }
            return View(obj);
        }


        [HttpGet]
        public IActionResult DeleteSpecification(int? id)
        {
            var specificationContorller = new SpecificationController(_uow, _mapper);
            var obj = specificationContorller.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSpecification(SpecificationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var specificationContorller = new SpecificationController(_uow, _mapper);
                specificationContorller.Delete(obj);
                return RedirectToAction("SpecificationManagement");
            }
            return View(obj);
        }
    }
}
