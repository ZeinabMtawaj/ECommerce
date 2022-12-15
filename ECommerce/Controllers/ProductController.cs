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
        public ProductViewModel convertToVM(Product item)
        {
            var product = new ProductViewModel();
            product.Name = item.Name;
            product.Description = item.Description;
            product.Price = item.Price;
            product.Sku = item.Sku;
            product.Quantity = item.Quantity;
            product.Image = item.Image;
            product.CreatedAt = item.CreatedAt;
            product.UpdatedAt = item.UpdatedAt;


            return product;

        }

        public Product convertFromVM(ProductViewModel item)
        {
            var product = new Product();
            product.Name = item.Name;
            product.Description = item.Description;
            product.Price = item.Price;
            product.Sku = item.Sku;
            product.Quantity = item.Quantity;
            product.Image = item.Image;
            //product.CreatedAt = item.CreatedAt;
            //product.UpdatedAt = item.UpdatedAt;


            return product;

        }

        public List<ProductViewModel> get()
        {
            var products = _uow.ProductRepo.GetAll();
            var productsVM = new List<ProductViewModel>();
            foreach (var item in products)
            {
                var product = convertToVM(item);


                productsVM.Add(product);


            }
            return productsVM;
        }

        public IActionResult Index()
        {
            var products = _uow.ProductRepo.GetAll();
           // ViewBag.Msg = "Hello from Index";
            // ViewData["Message"] = 

            //TempData["Message"] = "Hello from Students Index (TempData)";
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create (ProductViewModel pvm)
        {
            if (ModelState.IsValid)
            {
               _uow.ProductRepo.Add( convertFromVM(pvm) );
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pvm);

        }


        public IActionResult Edit(int id)
        {
            var obj = _uow.ProductRepo.Find(id);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductRepo.Update(convertFromVM(pvm));
                _uow.SaveChanges();
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

        public IActionResult Detail() {
            return View();
        }
    }
}
