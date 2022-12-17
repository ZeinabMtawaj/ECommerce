using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class CategorySpecificationValueRepo : BaseRepo<CategorySpecificationValue>, ICategorySpecificationValueRepo
    {
        public CategorySpecificationValueRepo(ECommerceDBContext db) : base(db)
        {

        }

    }
}


