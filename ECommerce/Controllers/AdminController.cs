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
            var categoryController = new CategoryController(_uow, _mapper);
            var items = categoryController.GetAllCategories();
            return View(items);
        }


        
    }
}
