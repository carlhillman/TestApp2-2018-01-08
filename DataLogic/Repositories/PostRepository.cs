using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Repositories
{
    public class PostRepository: Repository<Post, int>
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {

        }
   
    }
}