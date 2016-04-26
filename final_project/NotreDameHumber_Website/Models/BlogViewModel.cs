using NotreDameHumber_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTemp.Models
{
    public class BlogViewModel
    {
        public Blog Blogs { get; set; }
        public List<Comment> Comments { get; set; }
        
    }
}