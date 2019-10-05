using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Repositories
{
    public class FriendRepository : Repository<Friends, int>
    {
        public FriendRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}