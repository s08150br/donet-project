using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NDHLee.Models;

namespace NDHLee.Controllers
{
    public class VolunteerController : Controller
    {
        VolunteerContext db = new VolunteerContext();

        //VOLUNTEER HOME
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = Session["SentMessage"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(Volunteer volunteer)
        {
            db.Volunteers.Add(volunteer);
            db.SaveChanges();
            Session["SentMessage"] = "Application sent!";

            return RedirectToAction("Index");
        }

        //CHECK FOR UNIQUE EMAIL
        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!db.Volunteers.Any(v => v.Email == email), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Volunteer volunteer)
        {
            VolunteerContext volunteerContext = new VolunteerContext();
            volunteerContext.Volunteers.Add(volunteer);
            volunteerContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Testimonial()
        {
            return View(db.Volunteers.Where(t => t.Testimonial_Publish == true).ToList());
        }

            //TESTIMONIALS
        [HttpGet]
        public PartialViewResult TestimonialsPublicAll()
        {
            List<Volunteer> testimonial = db.Volunteers.Where(t => t.Testimonial_Publish == true).ToList();
            return PartialView("_volunteer_testimonials",testimonial);
        }

        [HttpGet]
        public PartialViewResult TestimonialsPublicFirstAZ()
        {
            List<Volunteer> testimonial = db.Volunteers.Where(t => t.Testimonial_Publish == true).OrderBy(v => v.StartDate).ToList();
            return PartialView("_volunteer_testimonials", testimonial);
        }

        [HttpGet]
        public PartialViewResult TestimonialsPublicLastAZ()
        {
            List<Volunteer> testimonial = db.Volunteers.Where(t => t.Testimonial_Publish == true).OrderBy(v => v.StartDate).ToList();
            return PartialView("_volunteer_testimonials", testimonial);
        }

        //LOGIN
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Login(FormCollection formCollection)
        //{
        //    Volunteer volunteer = new Volunteer();
        //    if
        //}

        [HttpPost]
        public ActionResult Login(Volunteer volunteer)
        {
            return View();
        }

        //ADMIN
        [HttpGet]
        public ActionResult Admin()
        {
            return View(db.Volunteers.ToList());
        }

        //AJAX LIST
        [HttpGet]
        public PartialViewResult RecentApplicant()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == false).OrderByDescending(v => v.ApplicationStart).ToList();
            return PartialView("_volunteer_applicants", volunteer);
        }

        [HttpGet]
        public PartialViewResult EarliestApplicant()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == false).OrderBy(v => v.ApplicationStart).ToList();
            return PartialView("_volunteer_applicants", volunteer);
        }

        [HttpGet]
        public PartialViewResult ApplicantFirstNameAZ()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == false).OrderBy(v => v.FirstName).ToList();
            return PartialView("_volunteer_applicants", volunteer);
        }

        [HttpGet]
        public PartialViewResult ApplicantLastNameAZ()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == false).OrderBy(v => v.LastName).ToList();
            return PartialView("_volunteer_applicants", volunteer);
        }

            //LIST APPROVED VOLUNTEERS
        [HttpGet]
        public PartialViewResult RecentVolunteer()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == true).OrderByDescending(v => v.StartDate).ToList();
            return PartialView("_volunteer_approved", volunteer);
        }

        [HttpGet]
        public PartialViewResult EarliestVolunter()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == true).ToList();
            return PartialView("_volunteer_approved", volunteer);
        }

        [HttpGet]
        public PartialViewResult VolunteerFirstNameAZ()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == true).ToList();
            return PartialView("_volunteer_approved", volunteer);
        }
        
        [HttpGet]
        public PartialViewResult VolunteerLastNameAZ()
        {
            List<Volunteer> volunteer = db.Volunteers.Where(v => v.ApplicationStatus == true).ToList();
            return PartialView("_volunteer_apporoved", volunteer);
        } 

        //EDIT - APPROVAL
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Volunteer volunteer = db.Volunteers.Single(v => v.Id == id);
            return View(volunteer);
        }

        [HttpPost]
        public ActionResult Edit(Volunteer volunteer)
        {
            db.Entry(volunteer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Admin");
        }


        //DELETE
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Volunteer volunteer = db.Volunteers.Single(v => v.Id == id);
            return View(volunteer);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer volunteer = db.Volunteers.Single(v => v.Id == id);
            db.Volunteers.Remove(volunteer);
            db.SaveChanges();
            return RedirectToAction("Admin");
        }

        //ADMIN TESTIMONIAL CONTROL
        [HttpGet]
        public PartialViewResult TestimonialsTrue()
        {
            List<Volunteer> testimonial = db.Volunteers.Where(t => t.Testimonial_Publish == true && t.ApplicationStatus == true).ToList();
            return PartialView("_admin_testimonials", testimonial);
        }

        [HttpGet]
        public PartialViewResult TestimonialsFalse()
        {
            List<Volunteer> testimonial = db.Volunteers.Where(t => t.Testimonial_Publish == false && t.ApplicationStatus == true).ToList();
            return PartialView("_admin_testimonials", testimonial);
        }

        //DETAILS
        [HttpGet]
        public ActionResult Details(int id)
        {
            Volunteer volunteer = db.Volunteers.Single(v => v.Id == id);
            return View(volunteer);
        }

        //USER(volunteer login)
        /// <summary>
        /// Action for registered volunteers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Vol(int id)
        {
            Volunteer volunteer = db.Volunteers.Single(v => v.Id == id);
            return View(volunteer);
        }

        [HttpPost]
        public ActionResult Vol(Volunteer volunteer)
        {
            db.Entry(volunteer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChangesAsync();
            return RedirectToAction("Vol");
        }
    }
}