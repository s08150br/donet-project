using NotreDameHumber_Website.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class AdminAppointmentController : Controller
    {
        NotreDameContext ndContext = new NotreDameContext();
        // private int page;

        // GET: AdminAppointment
        public ActionResult AppointmentList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstnameSortParm = String.IsNullOrEmpty(sortOrder) ? "Firstname_desc" : "";
            ViewBag.LastnameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            ViewBag.PreferredDateSortParm = sortOrder == "PreferredDate" ? "PreferredDate_desc" : "PreferredDate";
            ViewBag.PreferredTimeSortParm = sortOrder == "PreferredTime" ? "PreferredTime_desc" : "PreferredTime";
            ViewBag.SetSortParm = String.IsNullOrEmpty(sortOrder) ? "Set_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var appointments = from a in ndContext.Appointments
                               select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                appointments = appointments.Where(a => a.Lastname.Contains(searchString)
                                       || a.Firstname.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Firstname_desc":
                    appointments = appointments.OrderByDescending(a => a.Firstname);
                    break;
                case "lastname_desc":
                    appointments = appointments.OrderByDescending(a => a.Lastname);
                    break;
                case "PreferredDate":
                    appointments = appointments.OrderBy(a => a.PreferredDate);
                    break;
                case "PreferredDate_desc":
                    appointments = appointments.OrderByDescending(a => a.PreferredDate);
                    break;
                case "PreferredTime":
                    appointments = appointments.OrderBy(a => a.PreferredTime);
                    break;
                case "PreferredTime_desc":
                    appointments = appointments.OrderByDescending(a => a.PreferredTime);
                    break;
                case "Set_desc":
                    appointments = appointments.OrderByDescending(a => a.Set);
                    break;
                default:
                    appointments = appointments.OrderBy(a => a.Lastname);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(appointments.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int id)
        {
            Appointment app = ndContext.Appointments.Where(x => x.Id == id).First();
            Service serv = ndContext.Services.Where(x => x.Id == app.Service).First();
            Therapist thera = ndContext.Therapists.Where(x => x.Id == app.Therapist).First();
            ViewBag.ServiceName = serv.Name;
            ViewBag.TherapistName = thera.Lastname;

            return View(ndContext.Appointments.Find(id));
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Services = ndContext.Services;
            ViewBag.Therapists = ndContext.Therapists;
            return View(ndContext.Appointments.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            appointment.Set = "Yes";
            ndContext.Entry(appointment).State = System.Data.Entity.EntityState.Modified;
            ndContext.SaveChanges();
            return RedirectToAction("AppointmentList");
        }
        public ActionResult Delete(int id)
        {
            Appointment app = ndContext.Appointments.Where(x => x.Id == id).First();
            Service serv = ndContext.Services.Where(x => x.Id == app.Service).First();
            Therapist thera = ndContext.Therapists.Where(x => x.Id == app.Therapist).First();
            ViewBag.ServiceName = serv.Name;
            ViewBag.TherapistName = thera.Lastname;

            return View(ndContext.Appointments.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = ndContext.Appointments.Find(id);
            ndContext.Appointments.Remove(appointment);
            ndContext.SaveChanges();

            return RedirectToAction("AppointmentList");
        }
        public ActionResult Create()
        {
            ViewBag.Services = ndContext.Services;
            ViewBag.Therapists = ndContext.Therapists;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            appointment.Set = "No";
            ndContext.Appointments.Add(appointment);
            ndContext.SaveChanges();
            ViewBag.Services = ndContext.Services;
            ViewBag.Therapists = ndContext.Therapists;
            return RedirectToAction("AppointmentList");
        }
    }
}