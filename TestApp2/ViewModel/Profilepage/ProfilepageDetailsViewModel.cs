using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using TestApp2.Models;
using TestApp2.ViewModel.Post;

namespace TestApp2.ViewModel
{
    public class ProfilepageDetailsViewModel
    {
        //bilden
        public string Id { get; set; }
        public byte[] Bild{ get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Display(Name = "Stad", ResourceType = typeof(Resource))]
        public string Stad { get; set; }
        [Display(Name = "Ålder", ResourceType = typeof(Resource))]
        public int Alder { get; set; }
        [Display(Name = "Kön", ResourceType = typeof(Resource))]
        public Kön Kön { get; set; }
        [Display(Name = "Synlighet", ResourceType = typeof(Resource))]
        public Synlighet Synlighet { get; set; }

        public List<PostListViewModel> Posts { get; set; }
    }

}