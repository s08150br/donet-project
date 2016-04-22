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
    public class SearchDoctorController : Controller
    {
        private HDHDBContext db = new HDHDBContext();

        // Index_admin (Admin page)
        public ActionResult Index_admin()
        {
            var doctors = from p in db.tblDoctors
                          orderby p.Name ascending
                          select p;

            return View(doctors);
        }


        // Index_user (Unregistered or registered user)
        public ActionResult Index_user()
        {
            var doctors = from p in db.tblDoctors
                          orderby p.Name ascending
                          select p;

            return View(doctors);
        }

        // Details_user (Unregistered or registered user)
        public ActionResult Details_user(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            if (tblDoctor == null)
            {
                return HttpNotFound();
            }
            return View(tblDoctor);
        }

        // Details_admin (Unregistered or registered user)
        public ActionResult Details_admin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            if (tblDoctor == null)
            {
                return HttpNotFound();
            }
            return View(tblDoctor);
        }


        // Create_admin (Admin page)
        public ActionResult Create_admin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_admin([Bind(Include = "DoctorId,Name,Specialization,Department,Photo,Experience,Info")] tblDoctor tblDoctor)
        {
            if (ModelState.IsValid)
            {
                db.tblDoctors.Add(tblDoctor);
                db.SaveChanges();
                return RedirectToAction("Index_admin");
            }

            return View(tblDoctor);
        }



        // Edit_admin (Admin page)
        public ActionResult Edit_admin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            if (tblDoctor == null)
            {
                return HttpNotFound();
            }
            return View(tblDoctor);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_admin([Bind(Include = "DoctorId,Name,Specialization,Department,Photo,Experience,Info")] tblDoctor tblDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_admin");
            }
            return View(tblDoctor);
        }



        // Delete_admin (Admin page)
        public ActionResult Delete_admin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            if (tblDoctor == null)
            {
                return HttpNotFound();
            }
            return View(tblDoctor);
        }


        [HttpPost, ActionName("Delete_admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            db.tblDoctors.Remove(tblDoctor);
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


        //--------------------------------------------------------------------

        
    }
}
