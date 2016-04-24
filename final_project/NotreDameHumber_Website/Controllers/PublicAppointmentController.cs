using NotreDameHumber_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class PublicAppointmentController : Controller
    {
        NotreDameContext ndContext = new NotreDameContext();
        // GET: PublicAppointment
        public ActionResult Appointment()
        {
            ViewBag.Services = ndContext.Services;
            ViewBag.Therapists = ndContext.Therapists;
            return View();
        }
        [HttpPost]
        public ActionResult Appointment(Appointment appointment)
        {
            appointment.Set = "No";
            ndContext.Appointments.Add(appointment);
            ndContext.SaveChanges();
            ViewBag.Message = "Dear " + appointment.Firstname + " " + appointment.Lastname + " you have Successfully made an appointment with us.We will call you within 24 hours to set the appointment with you.";
            ViewBag.Services = ndContext.Services;
            ViewBag.Therapists = ndContext.Therapists;
            return View();
        }
    }
}