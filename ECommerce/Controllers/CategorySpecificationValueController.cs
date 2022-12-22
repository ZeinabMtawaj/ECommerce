using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using ECommerce.Models.ViewModels;
using AutoMapper;
using Ecommerce.Models;
using ApplicationDbContext.Models;

namespace Ecommerce.Controllers
{
    public class CategorySpecificationValueController : BaseController
    {
        public CategorySpecificationValueController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
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


    }
}
