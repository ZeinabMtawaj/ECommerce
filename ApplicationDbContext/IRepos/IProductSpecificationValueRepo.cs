using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDbContext.IRepos
{
    public interface IProductSpecificationValueRepo : IBaseRepo<ProductSpecificationValue>
    {
        //IEnumerable<Student> GetByCourse(int id);
    }
}
