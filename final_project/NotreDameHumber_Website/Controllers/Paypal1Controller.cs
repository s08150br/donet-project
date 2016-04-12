using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class Paypal1Controller : Controller
    {
       
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Parking");
            }
            var Is = Session["Cart"] as List<Parking>;
            return View(Is);
        }
        public ActionResult GetDataPaypal()
        {
            return View();
        }
    }
}