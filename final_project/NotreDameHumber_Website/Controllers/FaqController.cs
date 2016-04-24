using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class FaqController : Controller
    {
        private HDHDBContext db = new HDHDBContext();

        // Index_admin (Admin page)
        public ActionResult Index_admin()
        {
            var faq = from p in db.tblFAQs
                      orderby p.QuestionId descending
                      select p;
            
            return View(faq);
        }

        // Index_with_anwers_admin (Admin page)
        public ActionResult Index_with_answers_admin()
        {
            var faq = db.tblFAQs.Where(b => b.Answer !=  null).OrderByDescending(b => b.QuestionId).ToList();

            return View(faq);
        }

        // Index_without_anwers_admin (Admin page)
        public ActionResult Index_without_answers_admin()
        {
            var faq = db.tblFAQs.Where(b => b.Answer == null).OrderByDescending(b => b.QuestionId).ToList();

            return View(faq);
        }

        // Index_user (Unregistered or registered user)
        public ActionResult Index_user()
        {
            var faq = from p in db.tblFAQs
                      where p.Status == "Visible"
                      orderby p.QuestionId descending
                      select p;

            return View(faq);
        }



        // Create_user (Unregistered or registered user)
        public ActionResult Create_user()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_user([Bind(Include = "QuestionId,Name,Category,Question")] tblFAQ tblFAQ)
        {
            if (ModelState.IsValid)
            {
                tblFAQ.Date = DateTime.Now;
                tblFAQ.Status = "Invisible";
                db.tblFAQs.Add(tblFAQ);
                db.SaveChanges();
                return RedirectToAction("Create_thanks_user");
            }

            return View(tblFAQ);
        }


        // Edit_admin (Admin page)
        public ActionResult Edit_admin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = db.tblFAQs.Find(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQ);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_admin([Bind(Include = "QuestionId,Name,Category,Question,Answer,Date,Status")] tblFAQ tblFAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_admin");
            }
            return View(tblFAQ);
        }

        // Thank you page (Unregistered or registered user)
        public ActionResult Create_thanks_user()
        {
            return View();
        }



        // Delete_admin (Admin page)
        public ActionResult Delete_admin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFAQ tblFAQ = db.tblFAQs.Find(id);
            if (tblFAQ == null)
            {
                return HttpNotFound();
            }
            return View(tblFAQ);
        }

        [HttpPost, ActionName("Delete_admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblFAQ tblFAQ = db.tblFAQs.Find(id);
            db.tblFAQs.Remove(tblFAQ);
            db.SaveChanges();
            return RedirectToAction("Index_admin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //-------------------------------------------------------------------


        
    }
}
