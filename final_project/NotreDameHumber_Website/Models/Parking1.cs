using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotreDameHumber_Website.Models
{
    public class Parking1
    {
        [Required(ErrorMessage = "PleaseXXXX")]
        [RegularExpression("\\d+", ErrorMessage = "Please2XXXX")]
        [Range(1000, 2000, ErrorMessage = "ID oo 1000-2000")]
        public int item_number { get; set; }
        [Required(ErrorMessage = "PleaseXXXX")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Plaease BBB 100")]
        public string item_name { get; set; }
        [Required(ErrorMessage = "PleaseXXXX")]
        [Range(1.0, 100.0, ErrorMessage = "ID vv 1-100 USD")]
        public double amount { get; set; }
        [Required(ErrorMessage = "PleaseXXXX")]
        [RegularExpression("\\d+", ErrorMessage = "Please2XXXX")]
        [Range(1, 100, ErrorMessage = "ID XX 1-100")]
        public int quantity { get; set; }
    }
}