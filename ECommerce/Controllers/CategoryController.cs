﻿using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager) : base(uow, mapper, userManager, signInManager, roleManager)
        {
            
        }


        public IActionResult Index()
        {
            getUserFromSession();
            var items = this.GetAllToView();
            ViewBag.cols = this.GetColNames();
            ViewBag.createController = "Category";
            ViewBag.createAction = "Create";
            ViewBag.editController = "Category";
            ViewBag.editAction = "Edit";
            ViewBag.deleteController = "Category";
            ViewBag.deleteAction = "Delete";

            ViewBag.Page = "Category";
            ViewBag.Token = Request.Cookies[".AspNetCore.Identity.Application"];
            return View(items);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var items = _uow.CategoryRepo.GetAll();
            return Json(items);
        }

       

        [HttpGet]
        public IActionResult Create()
        {
            getUserFromSession();
            // ILayoutDecider ld = .. based on role
            CategoryVM categoryVM = new CategoryVM()
            {
                Category = new CategoryViewModel()
            };
            ViewBag.Page = "Category";
            ViewBag.Token = Request.Cookies[".AspNetCore.Identity.Application"];
            return View(categoryVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryVM obj)
        {
            
            //return View("~/Views/Category/testing.cshtml", obj);
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Category>(obj);
                _uow.CategoryRepo.Add(newObj);
                _uow.SaveChanges();
                obj.Category.Id = newObj.Id;
                var categorySpecificationValueController = new CategorySpecificationValueController(_uow, _mapper, _userManager, _signInManager, _roleManager);
                categorySpecificationValueController.Create(obj);
                return RedirectToAction("Index");
            }
            getUserFromSession();
            ViewBag.Page = "Category";
            ViewBag.Token = Request.Cookies[".AspNetCore.Identity.Application"];
            //return View("~/Views/Category/testing.cshtml", obj);
            return View(obj);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            getUserFromSession();
            var obj = this.FindToView(id);
            if (obj == null)
            {
                return NotFound();
            }
            var catSpecValController = new CategorySpecificationValueController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            var specVals = catSpecValController.GetAllSpecVals(obj.Id);
            CategoryVM categoryVM = new CategoryVM()
            {
                Category = obj,
                SpecificationValues = specVals
            };
            ViewBag.Page = "Category";
            ViewBag.Token = Request.Cookies[".AspNetCore.Identity.Application"];
            return View(categoryVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryVM obj)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Category>(obj);
                newObj.Id = obj.Category.Id;
                _uow.CategoryRepo.Update(newObj);
                _uow.SaveChanges();
                var categorySpecificationValueController = new CategorySpecificationValueController(_uow, _mapper, _userManager, _signInManager, _roleManager);
                categorySpecificationValueController.Edit(obj);
                return RedirectToAction("Index");
            }
            getUserFromSession();
            ViewBag.Page = "Category";
            ViewBag.Token = Request.Cookies[".AspNetCore.Identity.Application"];
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            Category? obj = null;
            if (Id != null)
            {
                var lambdas = new List<Expression<Func<Category, object>>>();
                lambdas.Add(x => x.Products);
                lambdas.Add(x => x.CategorySpecificationValues);
                obj = _uow.CategoryRepo.Find(x => (x.Id == Id), lambdas);
            }
            if (obj == null)
            {
                TempData["error"] = "Something Went Wront..";
            }
            else
            {
                if ((obj.Products.Count() != 0) || (obj.CategorySpecificationValues.Count() != 0))
                {
                    TempData["error"] = "Something Went Wront..";
                }
                else
                {
                    _uow.CategoryRepo.Delete(Id.Value);
                    _uow.SaveChanges();
                    TempData["success"] = "Deleted Successfully";
                }

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
            res.Add("Name");
            res.Add("Image");
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


        [HttpGet]
        public IActionResult GetSpecs(string catId)
        {
            int categoryId = int.Parse(catId);
            var catSpecValController = new CategorySpecificationValueController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            var specVals = catSpecValController.GetAllSpecVals(categoryId);
            var specVM = new SpecificationVM();
            var specificationController = new SpecificationController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            var specs = specificationController.GetAll();
            int cnt = 0;
            foreach (var spec in specs)
            {
                specVM.SpecificationIds.Add(spec.Id);
                specVM.SpecificationNames.Add(spec.Name);
                string specVal = "";
                if (cnt < specVals.Count())
                    specVal = specVals[cnt];
                specVM.SpecificationValues.Add(specVal);
                cnt++;
            }
            return PartialView("~/Views/Shared/_SpecsLoadView.cshtml", specVM);
        }



        public IEnumerable<Category> getAllCategories()
        {
            return _uow.CategoryRepo.GetAll();
        }



    }
}
