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
            return View(db.tblFAQs.ToList());
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
                return RedirectToAction("Index_user");
            }

            return View(tblFAQ);
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
            return RedirectToAction("Index");
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




        

        

        // GET: Faq/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Faq/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionId,Name,Category,Question,Answer,Date,Status")] tblFAQ tblFAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblFAQ);
        }

        
    }
}
