using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;

namespace Ecommerce.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
            
        }


        public IEnumerable<CategoryViewModel> GetAll()
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

        public void Create(CategoryViewModel obj)
        {
            var newObj = _mapper.Map<Category>(obj);
            newObj.CreatedAt = DateTime.Now;
            newObj.UpdatedAt = DateTime.Now;
            _uow.CategoryRepo.Add(newObj);
            _uow.SaveChanges();
            return;
        }
        public void Edit(CategoryViewModel obj)
        {
            var newObj = _mapper.Map<Category>(obj);
            newObj.UpdatedAt = DateTime.Now;
            _uow.CategoryRepo.Update(newObj);
            _uow.SaveChanges();
            return;
        }
        public void Delete(CategoryViewModel obj)
        {
            var newObj = _mapper.Map<Category>(obj);
            _uow.CategoryRepo.Delete(newObj.Id);
            _uow.SaveChanges();
            return;
        }

        public CategoryViewModel? Find(int? id)
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
