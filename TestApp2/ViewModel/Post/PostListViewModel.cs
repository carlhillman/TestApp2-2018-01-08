using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp2.ViewModel.Post
{
    public class PostListViewModel
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}