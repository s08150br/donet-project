using partialview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace partialview.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            CompantContext db = new CompantContext();
            return View(db.Employee.ToList());
        }
        public ActionResult picture()
        {
            CompantContext db = new CompantContext();
            
            return View(db.Employee.Where(m => m.Id == 1).ToList());
        }
        public ActionResult picture2()
        {
            CompantContext db = new CompantContext();

            return View(db.Employee.Where(m => m.Id == 2).ToList());
        }
        public ActionResult picture3()
        {
            CompantContext db = new CompantContext();

            return View(db.Employee.Where(m => m.Id == 3).ToList());
        }
        public ActionResult picture4()
        {
            CompantContext db = new CompantContext();

            return View(db.Employee.Where(m => m.Id == 4).ToList());
        }
        public ActionResult AboutUs()
        {

            return View();
        }
        public ActionResult ContentUs()
        {
            CompantContext db = new CompantContext();

            return View(db.Employee.Where(m => m.Id == 4).ToList());
        }
        public ActionResult FaQ()
        {
            CompantContext db = new CompantContext();

            return View(db.Employee.Where(m => m.Id == 4).ToList());
        }
    }
}

