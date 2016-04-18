using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class AlexDonateController : Controller
    {
        // List of donors (Admin page)
        public ActionResult Donate_list_admin()
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            var donation = from p in nDHDBContext.tblDonations
                           orderby p.DonationId descending
                           select p;

            return View(donation);
        }

        // List of donors (Admin page)
        public ActionResult Donate_list_order_name_admin()
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            var donation = from p in nDHDBContext.tblDonations
                           orderby p.Name ascending
                           select p;

            return View(donation);
        }

        // List of donors (Admin page)
        public ActionResult Donate_list_order_amount_admin()
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            var donation = from p in nDHDBContext.tblDonations
                           orderby p.Amount ascending
                           select p;

            return View(donation);
        }

        // Donate Delete (Admin page)
        [HttpGet]
        public ActionResult Donate_delete_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDonation donation = nDHDBContext.tblDonations.Single(don => don.DonationId == id);

            return View(donation);
        }

        // Donate Delete Confirmation (Admin page)
        [HttpPost, ActionName("Donate_delete_admin")]
        public ActionResult Donate_delete_conf_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDonation donation = nDHDBContext.tblDonations.Single(don => don.DonationId == id);


            nDHDBContext.Entry(donation).State = EntityState.Deleted;
            nDHDBContext.SaveChanges();

            return RedirectToAction("Donate_list_admin");
        }

        // Donate Edit (Admin page)
        [HttpGet]
        public ActionResult Donate_edit_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDonation donation = nDHDBContext.tblDonations.Single(don => don.DonationId == id);

            return View(donation);
        }

        [HttpPost]
        public ActionResult Donate_edit_admin(tblDonation donation)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            nDHDBContext.Entry(donation).State = System.Data.Entity.EntityState.Modified;
            nDHDBContext.SaveChanges();

            return RedirectToAction("Donate_list_admin");

        }

        // Donate Create (Unregistered or registered user)
        [HttpGet]
        public ActionResult Donate_create_user()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Donate_create_user(FormCollection formCollection)
        {
            tblDonation donation = new tblDonation();
            HDHDBContext nDHDBContext = new HDHDBContext();

            donation.Name = formCollection["Name"];
            donation.Amount = decimal.Parse(formCollection["Amount"]);
            donation.Email = formCollection["Email"];
            donation.Address = formCollection["Address"];
            donation.Phone = formCollection["Phone"];
            donation.Regdate = DateTime.Now;

            nDHDBContext.tblDonations.Add(donation);
            nDHDBContext.SaveChanges();

            return RedirectToAction("Donate_create_button_user");
        }

        // Donate Button Pay-Pal (Unregistered or registered user)

        public ActionResult Donate_create_button_user()
        {
            return View();
        }
    }
}