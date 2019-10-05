using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp2.Models;
using TestApp2.Repositories;
namespace TestApp2.Controllers
{
    public class FriendRequestController : Controller
    {    
        private UserRepository UserRepository;
        private FriendRepository FriendRepository;
        private AddFriendRepository addFriendRepository;
        public FriendRequestController() //konstruktor ska alltid se ut såhär 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            addFriendRepository = new AddFriendRepository(context);
            UserRepository = new UserRepository(context);
            FriendRepository = new FriendRepository(context);


        }
        // GET: FriendRequest
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendRequest(string id)
        {
            FriendRequest FriendRequest = new FriendRequest();

            var userNameId = User.Identity.GetUserId();

            var fromUser = UserRepository.Get(userNameId);

            FriendRequest.From = fromUser;

            var toUser = UserRepository.Get(id.ToString());

            FriendRequest.To = toUser;
            
            var checklist = addFriendRepository.GetAll();

            bool FriendRequestExist = false;
            foreach (var item in checklist)
            {

                if (fromUser.Id.Equals(item.To.Id) && toUser.Id.Equals(item.From.Id) || fromUser.Id.Equals(item.From.Id) && toUser.Id.Equals(item.To.Id))
                {
                    FriendRequestExist = true;
                }
            }

            if (FriendRequestExist == false)
            {
                addFriendRepository.Add(FriendRequest);

                addFriendRepository.Save();

                return View("Error");
            }
           
            else
            {
                return View("AlreadyFriends");
            }
        }

        //visar lista på vänförfrågningar 
        [HttpGet]
        public ActionResult RequestList()
        {   
            var anvandare = User.Identity.GetUserId();

            var requests = addFriendRepository.GetAll().Where(request => request.To.Id == anvandare).ToList();

            //ska hämta till vilken dvs inloggade användaren 
            var anvandaretill = addFriendRepository.GetAll().Where(x => x.To.Id == (anvandare)).Select(u=> u.From);

            var user = from a in UserRepository.GetAll().Where(x=>x.Id.Equals(anvandaretill)) select a;

            return View(requests);
        }


        public ActionResult AcceptFriend(int id)
        {
            var friendRequest = addFriendRepository.Get(id);
            Friends friends = new Friends();
            {
                friends.Friend1 = friendRequest.To;
                friends.Friend2 = friendRequest.From;
            };

            FriendRepository.Add(friends);
            FriendRepository.Save();
            addFriendRepository.Remove(friendRequest.Id);
            addFriendRepository.Save();

            return RedirectToAction("Requestlist", "FriendRequest");
        }

       public ActionResult DeclineFriend(int id)
        {
            var friendRequest = addFriendRepository.Get(Convert.ToInt32(id));
            var friendAccept = UserRepository.Get(User.Identity.GetUserId());

            var Friend = addFriendRepository.GetAll().Single(x => x.From.Id.Equals(friendRequest.From.Id) && x.To.Id.Equals(friendAccept.Id));

            addFriendRepository.Remove(Friend.Id);
            addFriendRepository.Save();

            return RedirectToAction("Requestlist", "FriendRequest");
        }

        [HttpGet]
        public PartialViewResult Notification()
        {
            var requests = addFriendRepository.GetAll().Where(request => request.To.Id == User.Identity.GetUserId()).ToList();
    
            var model = new BoolHolder();
            model.UserGotRequest = requests.Any();
            return PartialView("_Notification", model);
        }
    }
}