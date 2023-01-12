using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class TrendRepo : BaseRepo<Trend>, ITrendRepo
    {
        public TrendRepo(ECommerceDBContext db) : base(db)
        {

        }

    }
}
