using ApplicationDbContext.Models;
using ApplicationDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _uow;

        protected readonly IMapper _mapper;
        public BaseController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
    }
}
