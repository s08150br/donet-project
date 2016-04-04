using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NotreDameHumber_Website.Models
{
    [Table("tblWorker")]
    public class Worker
    {
        public int Id { get; set; }
        public string jobTitle { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public int shift { get; set; }
        public int pay { get; set; }
        public string desciption { get; set; }
    }
}