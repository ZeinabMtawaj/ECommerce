using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Customer.Models;
using Customer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace Customer.Controllers
{
    public class AddressController : BaseController
    {
        public AddressController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
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

        public void Update(int userId, IEnumerable<string> Addresses)
        {

        }

        public IEnumerable<Address> getAddressByUserId(int userId)
        {
            return _uow.AddressRepo.FindAll(x => x.UserId == userId);
        }
    }
}
