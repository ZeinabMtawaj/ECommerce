using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class SpecificationRepo : BaseRepo<Specification>, ISpecificationRepo
    {
        public SpecificationRepo(ECommerceDBContext db) : base(db) 
        {

        }

    }
}
