﻿using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;
using System.Linq.Expressions;
using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers
{
    public class SpecificationController : BaseController
    {

        public SpecificationController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }
      


        [HttpGet]
        public JsonResult getOtherSpecifications(int categoryId)
        {
            var lambdas = new List<Expression<Func<Specification, object>>>();
            lambdas.Add(x => x.CategorySpecificationValues);
            var specifications = _uow.SpecificationRepo.FindAll(x => !(x.CategorySpecificationValues.Any(item => item.CategoryId == categoryId)), null, null, lambdas);
            specifications.ToList().ForEach(specification => specification.CategorySpecificationValues = null);
            return Json(specifications);

        }



        public IActionResult Index()
        {
            var items = this.GetAllToView();
            ViewBag.cols = this.GetColNames();
            ViewBag.createController = "Specification";
            ViewBag.createAction = "Create";
            ViewBag.editController = "Specification";
            ViewBag.editAction = "Edit";
            ViewBag.deleteController = "Specification";
            ViewBag.deleteAction = "Delete";

            ViewBag.user = User;
            return View(items);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SpecificationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Specification>(obj);
                _uow.SpecificationRepo.Add(newObj);
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
        public IActionResult Edit(SpecificationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var newObj = _mapper.Map<Specification>(obj);
                _uow.SpecificationRepo.Update(newObj);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            Specification? obj = null;
            if (Id != null)
            {
                var lambdas = new List<Expression<Func<Specification, object>>>();
                lambdas.Add(x => x.CategorySpecificationValues);
                lambdas.Add(x => x.ProductSpecificationValues);
                obj = _uow.SpecificationRepo.Find(x => (x.Id == Id), lambdas);
            }
            if (obj == null)
            { 
                TempData["error"] = "Something Went Wront..";
            }
            else
            {
                if ((obj.CategorySpecificationValues.Count() != 0) || (obj.ProductSpecificationValues.Count() != 0))
                {
                    TempData["error"] = "Something Went Wront..";
                }
                else
                {
                    var lambdas = new List<Expression<Func<Specification, object>>>();
                    _uow.SpecificationRepo.Delete(Id.Value);
                    _uow.SaveChanges();
                    TempData["success"] = "Deleted Successfully";
                }

            }
            return RedirectToAction("Index");
        }



        public IEnumerable<SpecificationViewModel> GetAllToView()
        {
            var items = _uow.SpecificationRepo.GetAll();
            var viewItems = items.Select(item => _mapper.Map<SpecificationViewModel>(item));
            return viewItems;
        }
        public IEnumerable<Specification> GetAll()
        {
            var items = _uow.SpecificationRepo.GetAll();
            return items;
        }

        public List<string> GetColNames()
        {
            List<string> res = new List<string>();
            res.Add("Name");
            res.Add("Created At");
            res.Add("Updated At");
            return res;
        }

      

        public SpecificationViewModel? FindToView(int? id)
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



        [HttpGet]
        public IActionResult GetSpecs(string specValsString)
        {
            string [] specVals = JsonConvert.DeserializeObject<string[]>(specValsString);
            var specVM = new SpecificationVM();
            var specs = this.GetAllToView();
            int cnt = 0;

            foreach (var spec in specs)
            {
                specVM.SpecificationIds.Add(spec.Id);
                specVM.SpecificationNames.Add(spec.Name);
                string specVal = "";
                if (cnt < specVals.Length)
                    specVal = specVals[cnt];
                specVM.SpecificationValues.Add(specVal);
                cnt++;
            }
           
            return PartialView("~/Views/Shared/_SpecsLoad.cshtml", specVM);
        }
    }
}
