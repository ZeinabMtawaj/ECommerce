using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDbContext.Models
{
    public partial class UserRole:IdentityRole<int>
    {
        public UserRole() : base()
        { 
        }
    }
}
