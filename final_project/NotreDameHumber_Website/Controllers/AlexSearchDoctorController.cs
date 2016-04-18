using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class AlexSearchDoctorController : Controller
    {
        // List of doctors (Admin page)
        public ActionResult SearchDoctor_list_admin()
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            var doctors = from p in nDHDBContext.tblDoctors
                          orderby p.Name ascending
                          select p;

            return View(doctors);
        }

        // Create doctor (Admin page)
        [HttpGet]
        public ActionResult SearchDoctor_create_admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchDoctor_create_admin(FormCollection formCollection)
        {
            tblDoctor doctor = new tblDoctor();
            HDHDBContext nDHDBContext = new HDHDBContext();

            doctor.Name = formCollection["Name"];
            doctor.Specialization = formCollection["Specialization"];
            doctor.Department = formCollection["Department"];
            doctor.Photo = formCollection["Photo"];
            doctor.Experience = formCollection["Experience"];
            doctor.Info = formCollection["Info"];

            nDHDBContext.tblDoctors.Add(doctor);
            nDHDBContext.SaveChanges();


            return RedirectToAction("SearchDoctor_list_admin");
        }

        // Delete Doctor (Admin page)
        [HttpGet]
        public ActionResult SearchDoctor_delete_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDoctor doctor = nDHDBContext.tblDoctors.Single(f => f.DoctorId == id);

            return View(doctor);
        }

        [HttpPost, ActionName("SearchDoctor_delete_admin")]
        public ActionResult SearchDoctor_delete_conf_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDoctor doctor = nDHDBContext.tblDoctors.Single(f => f.DoctorId == id);

            nDHDBContext.Entry(doctor).State = EntityState.Deleted;
            nDHDBContext.SaveChanges();

            return RedirectToAction("SearchDoctor_list_admin");
        }

        // Doctor Details (Admin page)
        public ActionResult SearchDoctor_details_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDoctor doctor = nDHDBContext.tblDoctors.Single(f => f.DoctorId == id);

            return View(doctor);
        }

        // Edit doctor's profile (Admin page)
        [HttpGet]
        public ActionResult SearchDoctor_edit_admin(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDoctor doctor = nDHDBContext.tblDoctors.Single(f => f.DoctorId == id);

            return View(doctor);
        }

        [HttpPost]
        public ActionResult SearchDoctor_edit_admin(tblDoctor doctor)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            nDHDBContext.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
            nDHDBContext.SaveChanges();

            return RedirectToAction("SearchDoctor_list_admin");
        }

        // List of doctors (Unregistered or registered user)
        public ActionResult SearchDoctor_list_user()
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            var dictors = from p in nDHDBContext.tblDoctors
                          orderby p.Name ascending
                          select p;

            return View(dictors);
        }

        // Doctor Details (Unregistered or registered user)
        public ActionResult SearchDoctor_details_user(int id)
        {
            HDHDBContext nDHDBContext = new HDHDBContext();
            tblDoctor doctor = nDHDBContext.tblDoctors.Single(f => f.DoctorId == id);

            return View(doctor);
        }

    }
}