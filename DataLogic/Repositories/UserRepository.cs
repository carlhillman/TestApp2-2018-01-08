using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Repositories
{
    public class UserRepository : Repository <ApplicationUser, string>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }  
}