using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;

namespace Ecommerce.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
            
        }


        public IActionResult Index()
        {
            var items = this.GetAllToView();
            ViewBag.cols = this.GetColNames();
            ViewBag.createController = "Category";
            ViewBag.createAction = "Create";
            ViewBag.editController = "Category";
            ViewBag.editAction = "Edit";
            ViewBag.deleteController = "Category";
            ViewBag.deleteAction = "Delete";
            return View(items);
        }



        [HttpGet]
        public IActionResult Create()
        {
            // ILayoutDecider ld = .. based on role
            var specificationController = new SpecificationController(_uow, _mapper);
            var specs = specificationController.GetAllToView();
            CategoryVM categoryVM = new CategoryVM()
            {
                Category = new CategoryViewModel(),
                Specifications = specs.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(categoryVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Category>(obj);
                _uow.CategoryRepo.Add(newObj);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var obj = this.FindToView(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Category>(obj);
                _uow.CategoryRepo.Update(newObj);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            Category? obj = null;
            if (Id != null)
            {
                obj = _uow.CategoryRepo.Find(Id.Value);
            }
            if (obj == null)
            {
                // TempData["alert-type"] = "danger";
                // TempData["alert"] = "Something Went Wront..";
            }
            else
            {
                // TempData["alert-type"] = "success";
                // TempData["alert"] = "Deleted Successfully";
                _uow.CategoryRepo.Delete(Id.Value);
                _uow.SaveChanges();
            }
            return RedirectToAction("Index");
        }




        public IEnumerable<CategoryViewModel> GetAllToView()
        {
            var items = _uow.CategoryRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<CategoryViewModel>(item));
            return viewItems;
        }

        public List<String> GetColNames()
        {
            List<string> res = new List<string>();
            res.Add("Image");
            res.Add("Name");
            res.Add("Description");
            res.Add("Specifications");
            res.Add("Created At");
            res.Add("Updated At");
            return res;
        }

       
        public CategoryViewModel? FindToView(int? id)
        {
            if (id == null || id.Value == 0)
            {
                return null;
            }
            var obj = _uow.CategoryRepo.Find(id.Value);
            if (obj == null)
                return null;
            var res = _mapper.Map<CategoryViewModel>(obj);
            return res;
        }
    }
}
