using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class CategoryRepo : BaseRepo<Category>, ICetegoryRepo
    {
        public CategoryRepo(ECommerceDBContext db) : base(db) 
        {

        }

    }
}
