using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index(Parking p)

        {
            if (ModelState.IsValid)
            {
                if (Session["Cart"] != null)
                {
                    var Is = Session["Cart"] as List<Parking>;
                    Is.Add(p);
                }
                else
                {
                    Session["Cart"] = new List<Parking>() { p };
                }
                ModelState.Clear();
                RedirectToAction("Index", "Parking");
            }
            return View();
        }

    }
}