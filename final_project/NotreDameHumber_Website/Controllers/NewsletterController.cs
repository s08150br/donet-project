using NotreDameHumber_Website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class NewsletterController : Controller
    {
        private HDHDBContext db = new HDHDBContext();
        // GET: Newsletter
        public ActionResult Thankyou()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "firstName, lastName, Email")]NewsLetter newsletter)
        {
          
                if (ModelState.IsValid)
                {
                    db.NewsLetters.Add(newsletter);
                    db.SaveChanges();
                    return RedirectToAction("Thankyou");
                }
           
          
            return View(newsletter);
        }

    }
}