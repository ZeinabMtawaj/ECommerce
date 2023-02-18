using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class RatingRepo : BaseRepo<Rating>, IRatingRepo
    {
        public RatingRepo(ECommerceDBContext db) : base(db)
        {

        }

    }
}
