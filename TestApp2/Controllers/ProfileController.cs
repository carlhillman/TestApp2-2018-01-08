using Microsoft.AspNet.Identity;
using System;

using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp2.Models;
using TestApp2.Repositories;
using TestApp2.ViewModel;
using TestApp2.ViewModel.Post;
using TestApp2.ViewModel.Profilepage;
namespace TestApp2.Controllers
{ 
    public class ProfileController : Controller
    {
        private UserRepository userRepository;
        private PostRepository postRepository;
        public ProfileController() //konstruktor ska alltid se ut såhär 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
        }
        // GET: Profile
        //ska visa lista på användare som söks   
        public ActionResult Index(string searchString)
        {
            var user = from a in userRepository.GetAll().Where(x => x.Synlighet == Synlighet.Synlig) select a;
            if (!String.IsNullOrEmpty(searchString))
            {
              user = user.Where(s => s.Email.Contains(searchString) && s.Synlighet == Synlighet.Synlig);
            }
            return View(user);    
         
        }
        //details för minSida
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            var user = userRepository.Get(userId);

            var postList = postRepository.GetAll().Where(post => post.To.Id == userId).OrderByDescending(post => post.DateTime);
            var posts = postList.Select(post => new PostListViewModel()
            {
                Text = post.Text,
                Date = post.DateTime,
                FromName = post.From.Email,
                ToName = post.To.Email
            });


            var model = new ProfilepageDetailsViewModel
            {
                Id = userId,
                Email = user.Email,
                Stad = user.Stad,
                Alder = user.Alder,
                Kön = user.Kön,
                Synlighet = user.Synlighet,
                Posts = posts.ToList()
            };

            return View(model);
  
        }
    
        //visar profilbilden 
        public ActionResult ProfilBild(string id) 
        {
            var user = userRepository.Get(id);         
            if (user.Bild == null)
                return View("Edit");
            return File(user.Bild, user.ContentType); 
        }
     
        //edit
        public ActionResult Edit()
        {
            var id = User.Identity.GetUserId();
            ApplicationUser user = userRepository.Get(id);

            ProfilepageEditViewModel model = new ProfilepageEditViewModel
            {
                Id = id,
                Stad = user.Stad,
                Alder = user.Alder,
                Bild = user.Bild,
                Synlighet = user.Synlighet
            };

            return View(model); 
        }


        //ändrar och lägger till en bild
        [HttpPost]
        public ActionResult Edit(ProfilepageEditViewModel userEditModel, HttpPostedFileBase upload)
        {  
            if (ModelState.IsValid == false)
                return View();
            string id = User.Identity.GetUserId();
            ApplicationUser user = userRepository.Get(id);

            user.Alder = userEditModel.Alder;
            user.Stad = userEditModel.Stad;
            user.Synlighet = userEditModel.Synlighet;
     
            userRepository.Edit(user);

            //kollar bild inte är null 
            if(upload!= null && upload.ContentLength > 0  && upload.ContentType == "image/jpeg")
            {

                user.ContentType = upload.ContentType;
        
                //använder en binaryreader
                using (var reader = new BinaryReader(upload.InputStream))
                {
                  
                    user.Bild = reader.ReadBytes(upload.ContentLength);
                    userEditModel.Bild = user.Bild;
                  
                }
                userRepository.Save();

            }
            else
            {
                return View("ImageError");
            }
            return RedirectToAction("Details");

        }

        //public PartialViewResult GetPosts(string id)
        //{
        //    var postsForUser = postRepository.GetAll().Where(post => post.To.Id == id).OrderByDescending(post => post.DateTime);

        //    var posts = postsForUser.Select(post => new PostListViewModel()
        //    {
        //        Text = post.Text,
        //        Date = post.DateTime,
        //        FromName = post.From.Email,
        //        ToName = post.To.Email
        //    });

        //    return PartialView("_PostTable", posts);
        //}

    }
}

