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

        // GET: SearchDoctor
        public ActionResult Index()
        {
            return View(db.tblDoctors.ToList());
        }

        // GET: SearchDoctor/Details/5
        public ActionResult Details(int? id)
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

        // GET: SearchDoctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SearchDoctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorId,Name,Specialization,Department,Photo,Experience,Info")] tblDoctor tblDoctor)
        {
            if (ModelState.IsValid)
            {
                db.tblDoctors.Add(tblDoctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDoctor);
        }

        // GET: SearchDoctor/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: SearchDoctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorId,Name,Specialization,Department,Photo,Experience,Info")] tblDoctor tblDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDoctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDoctor);
        }

        // GET: SearchDoctor/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: SearchDoctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            db.tblDoctors.Remove(tblDoctor);
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
    }
}
