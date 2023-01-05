using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class PhotoRepo : BaseRepo<Photo>, IPhotoRepo
    {
        public PhotoRepo(ECommerceDBContext db) : base(db)
        {

        }

    }
}
