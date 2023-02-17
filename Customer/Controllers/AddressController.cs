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
            var oldAddresses = _uow.AddressRepo.FindAll(x => x.UserId == userId);
            Dictionary<string, int> indOfAddress = new Dictionary<string, int>();
            Dictionary<int, bool> existed = new Dictionary<int, bool>();
            List <string> addressesToBeAdded = new List<string>();
            int ind = 0;
            foreach (var address in oldAddresses)
            {
                indOfAddress.Add(address.Location, ind++);
            }
            for (int i = 0; i < Addresses.Count(); i++)
            {
                string newAddress = Addresses.ElementAt(i);
                if (newAddress == null || newAddress.Trim() == "")
                    continue;
                if (indOfAddress.ContainsKey(newAddress))
                {
                    var oldAddress = oldAddresses.ElementAt(indOfAddress[newAddress]);
                    existed.Add(oldAddress.Id, true);
                }
                else
                {
                    addressesToBeAdded.Add(newAddress);
                }
            }
            foreach(var address in oldAddresses)
            {
                if (!existed.ContainsKey(address.Id))
                {
                    _uow.AddressRepo.Delete(address);
                }
            }
            this.Create(userId, addressesToBeAdded); // has save changes
            return;
        }

        public IEnumerable<Address> getAddressByUserId(int userId)
        {
            return _uow.AddressRepo.FindAll(x => x.UserId == userId);
        }
    }
}
