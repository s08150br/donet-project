using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class AlexFaqController : Controller
    {
        // List of questions (Admin page)
        public ActionResult Faq_list_admin()
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            var faq = from p in nDHDBContext.tblFAQs
                      orderby p.QuestionId descending
                      select p;

            return View(faq);
        }

        // Question Delete (Admin page)
        [HttpGet]
        public ActionResult Faq_delete_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblFAQ faq = nDHDBContext.tblFAQs.Single(f => f.QuestionId == id);

            return View(faq);
        }

        [HttpPost, ActionName("Faq_delete_admin")]
        public ActionResult Faq_delete_conf_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblFAQ faq = nDHDBContext.tblFAQs.Single(f => f.QuestionId == id);

            nDHDBContext.Entry(faq).State = EntityState.Deleted;
            nDHDBContext.SaveChanges();

            return RedirectToAction("Faq_list_admin");
        }

        // Question Create (Unregistered or registered user)
        [HttpGet]
        public ActionResult Faq_create_user()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Faq_create_user(FormCollection formCollection)
        {
            tblFAQ faq = new tblFAQ();
            HDHDBContext nDHDBContext = new HDHDBContext();

            if (formCollection["Name"] == "")
            {
                faq.Name = "Anonym";
            }
            else
            {
                faq.Name = formCollection["Name"];
            }
            faq.Category = formCollection["Category"];
            faq.Question = formCollection["Question"];
            faq.Answer = "Yet to be answered";
            faq.Date = DateTime.Now;
            faq.Status = "Invisible";
            //faq.QuestionId = 99;


            nDHDBContext.tblFAQs.Add(faq);
            nDHDBContext.SaveChanges();

            return RedirectToAction("Faq_create_thanks_user");
        }

        // List of questions (Unregistered or registered user)
        public ActionResult Faq_list_user()
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            var faq = from p in nDHDBContext.tblFAQs
                      where p.Status == "Visible"
                      orderby p.QuestionId descending
                      select p;

            return View(faq);
        }

        // Thank you page (Unregistered or registered user)
        public ActionResult Faq_create_thanks_user()
        {
            return View();
        }

        // Answer to the question (Admin page)
        [HttpGet]
        public ActionResult Faq_edit_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblFAQ faq = nDHDBContext.tblFAQs.Single(f => f.QuestionId == id);

            return View(faq);
        }

        [HttpPost]
        public ActionResult Faq_edit_admin(tblFAQ faq)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            nDHDBContext.Entry(faq).State = System.Data.Entity.EntityState.Modified;
            nDHDBContext.SaveChanges();

            return RedirectToAction("Faq_list_admin");
        }
    }
}