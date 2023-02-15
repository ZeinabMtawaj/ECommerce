using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class WishListRepo : BaseRepo<WishList>, IWishListRepo
    {
        public WishListRepo(ECommerceDBContext db) : base(db)
        {

        }

    }
}
