using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Models;
using TestApp2.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using TestApp2.ViewModel.Post;

namespace TestApp2.ViewModel.Friend
{
    public class FriendViewModel 

    {
        public string Id { get; set; }
        public byte[] Bild {get; set; }
        public string Email { get; set; }
        public string Stad { get; set; }
        public int Alder { get; set; }
        public Kön Kön { get; set; }
        
        public List<PostListViewModel> Posts { get; set; }

    }
}