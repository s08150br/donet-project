using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Models
{
    public class Parking1
    {
        [Required(ErrorMessage = "Do not be left empty")]
        [RegularExpression("\\d+", ErrorMessage = "Please enter number")]
        [Range(1,100000, ErrorMessage = "ID must be number")]
        public int item_number { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter your name")]
        public string item_name { get; set; }

        //[Required(ErrorMessage = "Do not be left empty")]
        //[Range(1.0, 100.99, ErrorMessage = " 1-100 USD")]
        [HiddenInput(DisplayValue =false)]
        public double amount { get; set; }

        [Required(ErrorMessage = "How many hours do you want to stay ?")]
        [RegularExpression("\\d+", ErrorMessage = "must be a number")]
        [Range(1, 24, ErrorMessage = "The number must be between 1-24")]
        public int quantity { get; set; }
        public int hours { get; set; }
    }
}
