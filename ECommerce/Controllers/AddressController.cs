using ApplicationDbContext.UOW;
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

namespace Ecommerce.Controllers
{
    public class AddressController : BaseController
    {
        public AddressController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }


        public void Create(int userID, IEnumerable<string> Addresses)
        {
            foreach(var loc in Addresses)
            {
                if (loc.Trim() == "")
                    continue;
                Address addr = new Address();
                addr.UserId = userID;
                addr.Location = loc;

                _uow.AddressRepo.Add(addr);
            }
            _uow.SaveChanges();
        }

    }
}
