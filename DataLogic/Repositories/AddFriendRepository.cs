using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Repositories
{
   
        public class AddFriendRepository : Repository<FriendRequest, int>
        {
            public AddFriendRepository(ApplicationDbContext context) : base(context)
            {

            }
        
    }
}