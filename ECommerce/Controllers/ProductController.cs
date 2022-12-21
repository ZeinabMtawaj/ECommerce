using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Linq.Expressions;


namespace Ecommerce.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }


        public IActionResult Index()
        {
            var items = this.GetAllToView();
            ViewBag.cols = this.GetColNames();
            ViewBag.createController = "Product";
            ViewBag.createAction = "Create";
            ViewBag.editController = "Product";
            ViewBag.editAction = "Edit";
            ViewBag.deleteController = "Product";
            ViewBag.deleteAction = "Delete";
            return View(items);
        }



        [HttpGet]
        public IActionResult Create()
        {
            // ILayoutDecider ld = .. based on role
            var categoryController = new CategoryController(_uow, _mapper);
            var categories = categoryController.GetAllToView();
            ProductVM productVM = new ProductVM()
            {
                Product = new ProductViewModel()
               
            };
            return View(productVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM obj)
        {

            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Product>(obj);
                _uow.ProductRepo.Add(newObj);
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
        public IActionResult Edit(ProductViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Product>(obj);
                _uow.ProductRepo.Update(newObj);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            Product? obj = null;
            if (Id != null)
            {
                var lambdas = new List<Expression<Func<Product, object>>>();
                lambdas.Add(x => x.Photos);
                lambdas.Add(x => x.ProductOrders);
                lambdas.Add(x => x.ProductSpecificationValues);
                lambdas.Add(x => x.Ratings);
                lambdas.Add(x => x.WishLists);
                obj = _uow.ProductRepo.Find(x => (x.Id == Id), lambdas);
            }
            if (obj == null)
            {
                TempData["error"] = "Something Went Wront..";
            }
            else
            {
                if ((obj.Photos.Count() != 0) || (obj.ProductOrders.Count() != 0) || (obj.Ratings.Count() != 0) || (obj.WishLists.Count() != 0) || (obj.ProductSpecificationValues.Count() != 0))
                {
                    TempData["error"] = "Something Went Wront..";
                }
                else
                {
                    var lambdas = new List<Expression<Func<Specification, object>>>();
                    _uow.ProductRepo.Delete(Id.Value);
                    _uow.SaveChanges();
                    TempData["success"] = "Deleted Successfully";
                }

            }
            return RedirectToAction("Index");
        }




        public IEnumerable<ProductViewModel> GetAllToView()
        {
            var items = _uow.ProductRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<ProductViewModel>(item));
            return viewItems;
        }

        public List<String> GetColNames()
        {
            List<string> res = new List<string>();
            res.Add("Name");
            res.Add("Description");
            res.Add("Price");
            res.Add("Image");
            res.Add("Category");
            res.Add("Trend");
            res.Add("Photos");
            res.Add("Specification");
            res.Add("Created At");
            res.Add("Updated At");
            return res;
        }


        public ProductViewModel? FindToView(int? id)
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

        public JsonResult IsSKUExist(string sku)
        { var obj = _uow.ProductRepo.Find(x => x.Sku == sku);
            if (obj == null) 
                return Json(false);
            else
                return Json(true);   



        }
    }
}
