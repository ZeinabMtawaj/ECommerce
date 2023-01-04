﻿using ApplicationDbContext.UOW;
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
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            var items = _uow.ProductRepo.FindAll(x => x.Id > 1, null, null,lambdas);
            ViewBag.cols = this.GetColNames();
            ViewBag.createController = "Product";
            ViewBag.createAction = "Create";
            ViewBag.editController = "Product";
            ViewBag.editAction = "Edit";
            ViewBag.deleteController = "Product";
            ViewBag.deleteAction = "Delete";
            var viewItems = items.Select(item => _mapper.Map<ProductViewModel>(item));
            return View(viewItems);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel pvm = new ProductViewModel();  
            return View(pvm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel obj)
        {
            
            if (ModelState.IsValid)
            {   var specs = obj.SpecsId;
                var specValues = obj.SpecsValue;
                var additional_photos = obj.AdditionalPhoto;
                var newObj = _mapper.Map<Product>(obj);
                _uow.ProductRepo.Add(newObj);
                _uow.SaveChanges();
                var z = newObj.Id;
                if (specs != null)
                {
                    foreach (var spec in specs)
                    {
                        var i = 0;
                        ProductSpecificationValue productSpecificationValue = new ProductSpecificationValue()
                        {
                            ProductId = z,
                            SpecificationId = int.Parse(spec),
                            Value = specValues[i]



                        };
                        i++;
                        _uow.ProductSpecificationValueRepo.Add(productSpecificationValue);
                    }
                    _uow.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {

            var obj = FindToView(id);

            if (obj == null)
            {
                return NotFound();
            }
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.ProductSpecificationValues);
            var product_specs = _uow.ProductRepo.Find(x => x.Id == id.Value, lambdas);
            var specs = product_specs.ProductSpecificationValues;
            var specIds= new List<String>();
            var specValues = new List<String>();
            if (specs != null)
            {
                foreach (var spec in specs)
                {
                    specIds.Add((spec.SpecificationId).ToString());
                    specValues.Add(spec.Value);
                }
                obj.SpecsId = specIds;
                obj.SpecsValue = specValues;
            }
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var specs = obj.SpecsId;
                var specValues = obj.SpecsValue;
                var newObj = _mapper.Map<Product>(obj);
                _uow.ProductRepo.Update(newObj);
                //_uow.SaveChanges();
                var z = newObj.Id;
                var lambdas = new List<Expression<Func<Product, object>>>();
                lambdas.Add(x => x.ProductSpecificationValues);
                var product_specs= _uow.ProductRepo.Find(x=>(x.Id==z),lambdas);
                var productSpecificationValues = product_specs.ProductSpecificationValues;
                //if (productSpecificationValues != null)
                //{
                    var psv = productSpecificationValues.ToList();
                    var ids_list = new List<int>();
                    foreach (var specification in psv)
                    {
                        ids_list.Add(specification.Id);
                    }
                //}
                if (specs != null)
                {
                    foreach (var spec in specs)
                    {
                        var i = 0;
                        var e = int.Parse(spec);
                        var b = specValues[i];
                        ProductSpecificationValue productSpecificationValue = new ProductSpecificationValue()
                        {
                            ProductId = z,
                            SpecificationId = e,
                            Value = b



                        };
                        int index = psv.FindIndex(x => ((x.SpecificationId == e) && (x.ProductId == z)));
                        if (index != -1)
                        {
                            _uow.ProductSpecificationValueRepo.Update(productSpecificationValue);
                            ids_list.RemoveAt(index);


                        }

                        else
                        {
                            _uow.ProductSpecificationValueRepo.Add(productSpecificationValue);
                        }

                        i++;
                    }
                    foreach (var id in ids_list) { 
                        _uow.ProductSpecificationValueRepo.Delete(id);
                    
                    }
                    _uow.SaveChanges();
                }
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
            //res.Add("Trend");
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
