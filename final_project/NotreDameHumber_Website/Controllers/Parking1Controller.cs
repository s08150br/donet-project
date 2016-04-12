using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotreDameHumber_Website.Models;

namespace NotreDameHumber_Website.Controllers
{
    public class Parking1Controller : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Parking1 p)
        {
            if (ModelState.IsValid)
            {
                if (Session["cart"] != null)
                {
                    var ls = Session["cart"] as List<Parking1>;
                    ls.Add(p);
                }
                else
                {
                    Session["cart"] = new List<Parking1>() { p };
                }
                ModelState.Clear();// clear data from Form
                RedirectToAction("Index", "Parking1"); // Anti F5 submit
            }
            return View(); // model validate is false
        }
    }
}