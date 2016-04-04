using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NotreDameHumber_Website.Models
{
    [Table("NewsLetters")]
    public class Newsletter
    {
        public int Id { get; set;}
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string Email { get; set; }
    }
}