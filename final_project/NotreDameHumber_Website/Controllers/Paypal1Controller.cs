using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NotreDameHumber_Website.Models;

namespace NotreDameHumber_Website.Controllers
{
    public class Paypal1Controller : Controller
    {
        
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Parking1");
            }
            var Is = Session["Cart"] as List<Parking1>;
            return View(Is);
        }
        public ActionResult GetDataPaypal()
        {
            var getData = new GetDataPaypal1();
            var order = getData.InformationOrder(getData.GetPayPalResponse(Request.QueryString["tx"]));
            ViewBag.tx = Request.QueryString["tx"];
            return View();
        }
    }
}