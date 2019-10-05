using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp2.Models;
using TestApp2.Repositories;
using TestApp2.ViewModel.Friend;
using TestApp2.ViewModel.Post;

namespace TestApp2.Controllers
{
    //controller som ska bearbeta andra användare samt lägga till denne som vän 
    public class FriendController : Controller
    {
      
        private UserRepository userRepository;
        private AddFriendRepository addFriendRepository;
        private FriendRepository friendRepository;
        private PostRepository postRepository;
        public FriendController() //konstruktor ska alltid se ut såhär 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            addFriendRepository = new AddFriendRepository(context);
            friendRepository = new FriendRepository(context);
            postRepository = new PostRepository(context);
        }

 
        //details för Andra användare sida 
        public ActionResult FriendDetails(string id)
        {

            var user = userRepository.Get(id);
            if(user == null)
            return RedirectToAction("index");

            var postsForUser = postRepository.GetAll().Where(post => post.To.Id == id);

            var posts = postsForUser.Select(post => new PostListViewModel()
            {
                Text = post.Text,
                Date = post.DateTime,
                FromName = post.From.Email,
                ToName = post.To.Email
            });

            var model = new FriendViewModel
            {
                Id = user.Id,
                Bild = user.Bild,
                Email = user.Email,
                Stad = user.Stad,
                Alder = user.Alder,
                Kön = user.Kön,
                Posts = posts.ToList()
            };

            return View(model);

        }

        public PartialViewResult GetPosts(string id)
        {
            var postsForUser = postRepository.GetAll().Where(post => post.To.Id == id).OrderByDescending(post => post.DateTime);

            var posts = postsForUser.Select(post => new PostListViewModel()
            {
                Text = post.Text,
                Date = post.DateTime,
                FromName = post.From.Email,
                ToName = post.To.Email
            });

            return PartialView("_PostTable", posts);
        }


        //visar lista på mina vänner
        [HttpGet]
        public ActionResult FriendList()
        {
            var anvandare = User.Identity.GetUserId();
            //ska hämta till vilken dvs inloggade användaren 
            var anvandaretill = friendRepository.GetAll().Where(x => x.Friend1.Id == (anvandare)).Select(u => u.Friend2);

            var user = from a in userRepository.GetAll().Where(x => x.Id.Equals(anvandaretill)) select a;

            var anvandaretill2 = friendRepository.GetAll().Where(x => x.Friend2.Id == (anvandare)).Select(u => u.Friend1);

            var user2 = from a in userRepository.GetAll().Where(x => x.Id.Equals(anvandaretill2)) select a;

            var total = anvandaretill.Concat(anvandaretill2);

            return View(total.ToList());
        }

    }
}