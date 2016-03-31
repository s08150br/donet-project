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
    public class AdminEventsController : Controller
    {
        private HDHDBContext db = new HDHDBContext();

        // GET: AdminEvents
        public ActionResult Index()
        {
            return View(db.AdminEvents.ToList());
        }

        // GET: AdminEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminEvent adminEvent = db.AdminEvents.Find(id);
            if (adminEvent == null)
            {
                return HttpNotFound();
            }
            return View(adminEvent);
        }

        // GET: AdminEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventName,Description,Address,Date,StartTime,EndTime,Fee,ContactEmail,Phone,EventWebsite")] AdminEvent adminEvent)
        {
            if (ModelState.IsValid)
            {
                db.AdminEvents.Add(adminEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminEvent);
        }

        // GET: AdminEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminEvent adminEvent = db.AdminEvents.Find(id);
            if (adminEvent == null)
            {
                return HttpNotFound();
            }
            return View(adminEvent);
        }

        // POST: AdminEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventName,Description,Address,Date,StartTime,EndTime,Fee,ContactEmail,Phone,EventWebsite")] AdminEvent adminEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminEvent);
        }

        // GET: AdminEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminEvent adminEvent = db.AdminEvents.Find(id);
            if (adminEvent == null)
            {
                return HttpNotFound();
            }
            return View(adminEvent);
        }

        // POST: AdminEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminEvent adminEvent = db.AdminEvents.Find(id);
            db.AdminEvents.Remove(adminEvent);
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
