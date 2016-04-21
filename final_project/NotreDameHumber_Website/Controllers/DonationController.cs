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
    public class DonationController : Controller
    {
        private HDHDBContext db = new HDHDBContext();

        //Index_admin (Admin page)
        public ActionResult Index_admin()
        {
            var donation = from p in db.tblDonations
                           orderby p.DonationId descending
                           select p;

            return View(donation);
        }

        public ActionResult Index_order_name_admin()
        {
            
            var donation = from p in db.tblDonations
                           orderby p.Name ascending
                           select p;

            return View(donation);
        }

        public ActionResult Index_order_amount_admin()
        {
            var donation = from p in db.tblDonations
                           orderby p.Amount descending
                           select p;

            return View(donation);
        }


        // Create_user (Unregistered or registered user)
        public ActionResult Create_user()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_user([Bind(Include = "DonationId,Name,Amount,Email,Address,Phone")] tblDonation tblDonation)
        {
            if (ModelState.IsValid)
            {

                tblDonation.Regdate = DateTime.Now;

                db.tblDonations.Add(tblDonation);
                db.SaveChanges();
                return RedirectToAction("Create_button_user");
            }

            return View(tblDonation);
        }


        // Donate Button Pay-Pal (Unregistered or registered users)
        public ActionResult Create_button_user()
        {
            return View();
        }

        // Edit_admin (Admin page)
        public ActionResult Edit_admin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonation tblDonation = db.tblDonations.Find(id);
            if (tblDonation == null)
            {
                return HttpNotFound();
            }
            return View(tblDonation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_admin([Bind(Include = "DonationId,Name,Amount,Email,Address,Phone,Regdate")] tblDonation tblDonation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDonation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_admin");
            }
            return View(tblDonation);
        }

        // Delete_admin (Admin page)
        public ActionResult Delete_admin(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonation tblDonation = db.tblDonations.Find(id);
            if (tblDonation == null)
            {
                return HttpNotFound();
            }
            return View(tblDonation);
        }

        [HttpPost, ActionName("Delete_admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDonation tblDonation = db.tblDonations.Find(id);
            db.tblDonations.Remove(tblDonation);
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
    }
}
