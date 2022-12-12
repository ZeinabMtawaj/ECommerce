using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDbContext.IRepos
{
    public interface IProductRepo : IBaseRepo<Product>
    {
        //IEnumerable<Student> GetByCourse(int id);
    }
}
