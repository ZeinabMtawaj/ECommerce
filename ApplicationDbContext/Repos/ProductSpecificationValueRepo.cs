using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class ProductSpecificationValueRepo : BaseRepo<ProductSpecificationValue>, IProductSpecificationValueRepo
    {
        public ProductSpecificationValueRepo(ECommerceDBContext db) : base(db)
        {

        }

    }
}


