using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class AddressRepo : BaseRepo<Address>, IAddressRepo
    {
        public AddressRepo(ECommerceDBContext db) : base(db) 
        {

        }

    }
}
