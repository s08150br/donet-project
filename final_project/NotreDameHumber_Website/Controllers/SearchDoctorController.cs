using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Index_admin(string SearchBy, string search)
        {
            if (SearchBy == "Name")
            {
                return View(db.tblDoctors.Where(x => x.Name.Contains(search) || search == null).OrderBy(x => x.Name).ToList());
            }
            else if (SearchBy == "Specialization")
            {
                return View(db.tblDoctors.Where(x => x.Specialization.Contains(search) || search == null).OrderBy(x => x.Name).ToList());
            }
            else
            {
                return View(db.tblDoctors.Where(x => x.Department.Contains(search) || search == null).OrderBy(x => x.Name).ToList());
            }
        }


        // Index_user (Unregistered or registered user)
        public ActionResult Index_user(string SearchBy, string search)
        {
            if (SearchBy == "Name")
            {
                return View(db.tblDoctors.Where(x => x.Name.Contains(search) || search == null).OrderBy(x => x.Name).ToList());
            }
            else if (SearchBy == "Specialization")
            {
                return View(db.tblDoctors.Where(x => x.Specialization.Contains(search) || search == null).OrderBy(x => x.Name).ToList());
            }
            else
            {
                return View(db.tblDoctors.Where(x => x.Department.Contains(search) || search == null).OrderBy(x => x.Name).ToList());
            }
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

        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create_admin([Bind(Include = "DoctorId,Name,Specialization,Department,Photo,Experience,Info")] tblDoctor tblDoctor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tblDoctors.Add(tblDoctor);
        //        db.SaveChanges();
        //        return RedirectToAction("Index_admin");
        //    }

        //    return View(tblDoctor);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_admin(HttpPostedFileBase file, string name, string specialization, string department, string experience, string info, tblDoctor doctor)
        {
            HDHDBContext db = new HDHDBContext();
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    doctor.Name = name;
                    doctor.Specialization = specialization;
                    doctor.Department = department;
                    doctor.Experience = experience;
                    doctor.Info = info;
                    doctor.Photo = "~/Images/D_photos/" + fileName;

                    db.tblDoctors.Add(doctor);
                    db.SaveChanges();
                    var path = Path.Combine(Server.MapPath("~/Images/D_photos"), fileName);
                    file.SaveAs(path);

                }
                return RedirectToAction("Index_admin");
            }
            return View(doctor);
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


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit_admin([Bind(Include = "DoctorId,Name,Specialization,Department,Photo,Experience,Info")] tblDoctor tblDoctor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tblDoctor).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index_admin");
        //    }
        //    return View(tblDoctor);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_admin(HttpPostedFileBase file, string name, string specialization, string department, string experience, string info, tblDoctor doctor)
        {
            HDHDBContext db = new HDHDBContext();
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    doctor.Name = name;
                    doctor.Specialization = specialization;
                    doctor.Department = department;
                    doctor.Experience = experience;
                    doctor.Info = info;
                    doctor.Photo = "~/Images/D_photos/" + fileName;

                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    var path = Path.Combine(Server.MapPath("~/Images/D_photos"), fileName);
                    file.SaveAs(path);

                }
                return RedirectToAction("Index_admin");
            }
            return View(doctor);
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


        //[HttpPost, ActionName("Delete_admin")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblDoctor tblDoctor = db.tblDoctors.Find(id);
        //    db.tblDoctors.Remove(tblDoctor);
        //    db.SaveChanges();
        //    return RedirectToAction("Index_admin");
        //}


        [HttpPost, ActionName("Delete_admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDoctor tblDoctor = db.tblDoctors.Find(id);
            db.tblDoctors.Remove(tblDoctor);

            string fullPath = Request.MapPath(tblDoctor.Photo);

            System.IO.File.Delete(fullPath);
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
