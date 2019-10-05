using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestApp2.Models;
using TestApp2.Repositories;
using TestApp2.ViewModel.Post;

namespace TestApp2.Controllers
{
    public class PostsController : ApiController
    {
        private PostRepository PostRepository;
        private UserRepository UserRepository; 
        public PostsController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            PostRepository = new PostRepository(context);
            UserRepository = new UserRepository(context);
        }

        // skickar post
        [HttpPost]
        public void Post(PostViewModel model)
        {
            if(model.Text != "")
            {
                Post post = new Post();
                var anvandareTill = UserRepository.Get(model.To);
                post.To = anvandareTill;

                var anvandareFran = UserRepository.Get(model.From);
                post.From = anvandareFran;

                post.Text = model.Text;
                post.DateTime = DateTime.Now;
                PostRepository.Add(post);
                PostRepository.Save(); 
            }
        }
    }
}
