using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using AutoMapper;

namespace Customer.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public IActionResult Index()
        {
            TrendController trendCon = new TrendController(_uow, _mapper);
            var trends = trendCon.GetAll();
            return View(trends);
        }
    }
}
