using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using ECommerce.Models.ViewModels;
using AutoMapper;
using Ecommerce.Models;
using ApplicationDbContext.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers
{
    public class CategorySpecificationValueController : BaseController
    {
        public CategorySpecificationValueController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {
        }


        public void Create(CategoryVM categoryVM)
        {
            if (categoryVM.SpecificationValues == null)
                return;
            int cnt = categoryVM.SpecificationValues.Count();
            for (int i = 0; i < cnt; i++)
            {
                string specValue = categoryVM.SpecificationValues.ElementAt(i);
                if (specValue == null || specValue.Length == 0)
                    continue;
                string specIdValue = categoryVM.SpecificationIds.ElementAt(i);
                int specId = int.Parse(specIdValue);
                var categorySpecificationValue = new CategorySpecificationValue()
                {
                    Value = specValue,
                    SpecificationId = specId,
                    CategoryId = categoryVM.Category.Id
                };
                _uow.CategorySpecificationValueRepo.Add(categorySpecificationValue);
            }
            _uow.SaveChanges();
            return ;
        }
        public void Edit(CategoryVM categoryVM)
        {
            var catSpecVals = this.GetSpecVals(categoryVM.Category.Id);
            Dictionary<int, int> valOfSpec = new Dictionary<int, int>();
            int ind = 0;
            foreach (var catSpecVal in catSpecVals)
            {
                valOfSpec.Add(catSpecVal.SpecificationId, ind++);
            }
            for (int i = 0; i < categoryVM.SpecificationIds.Count(); i++)
            {
                string specIdValue = categoryVM.SpecificationIds.ElementAt(i);
                int specId = int.Parse(specIdValue);
                string specVal = categoryVM.SpecificationValues.ElementAt(i);


                if (specVal == null || specVal.Length == 0)
                {
                    if (valOfSpec.ContainsKey(specId))
                    {
                        int index = valOfSpec[specId];
                        _uow.CategorySpecificationValueRepo.Delete(catSpecVals.ElementAt(index).Id);
                    }
                }
                else
                {
                    if (valOfSpec.ContainsKey(specId))
                    {
                        int index = valOfSpec[specId];
                        var catSpecVal = catSpecVals.ElementAt(index);
                        if (specVal != catSpecVal.Value)
                        {
                            catSpecVal.Value = specVal;
                            _uow.CategorySpecificationValueRepo.Update(catSpecVal);
                        }
                    }
                    else
                    {
                        var categorySpecificationValue = new CategorySpecificationValue()
                        {
                            Value = specVal,
                            SpecificationId = specId,
                            CategoryId = categoryVM.Category.Id
                        };
                        _uow.CategorySpecificationValueRepo.Add(categorySpecificationValue);
                    }

                }
            }
            _uow.SaveChanges();
            return;
        }
        public IEnumerable<CategorySpecificationValue> GetSpecVals(int id)
        {
            return _uow.CategorySpecificationValueRepo.FindAll(x => x.CategoryId == id);
        }
        public List<string> GetAllSpecVals(int id)
        {
            List<string> res = new List<string>();
            var catSpecVals = _uow.CategorySpecificationValueRepo.FindAll(x => x.CategoryId == id);
            Dictionary<int, string> valOfSpec = new Dictionary<int, string>();
            foreach (var catSpecVal in catSpecVals)
            {
                valOfSpec.Add(catSpecVal.SpecificationId, catSpecVal.Value);
            }
            var specController = new SpecificationController(_uow, _mapper, _userManager, _signInManager);
            var specs = specController.GetAll();
            foreach(var spec in specs)
            {
                string val = "";
                if(valOfSpec.ContainsKey(spec.Id))
                    val = valOfSpec[spec.Id];
                res.Add(val);
            }
            return res;
        }

        public void Delete(int id)
        {
            // for now the simple one
            _uow.CategorySpecificationValueRepo.Delete(id);
        }

    }
}
