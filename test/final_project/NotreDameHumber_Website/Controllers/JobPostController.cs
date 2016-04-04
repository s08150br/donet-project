using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NotreDameHumber_Website.Models;

namespace NotreDameHumber_Website.Controllers
{
    public class JobPostController : Controller
    {
        // GET: Joppost
        public ActionResult Index()
        {
            JopContext workerContext = new JopContext();
            List<Worker> workers = workerContext.Workers.ToList();
            return View(workers);
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Worker worker = new Worker();
            JopContext workerContext = new JopContext();

            worker.jobTitle = formCollection["jobTitle"];
            worker.location = formCollection["location"];
            worker.email = formCollection["email"];
            worker.shift = Convert.ToInt32(formCollection["shift"]);
            worker.pay = Convert.ToInt32(formCollection["pay"]);
            worker.desciption = formCollection["desciption"];

            workerContext.Workers.Add(worker);
            workerContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //make new object of WorkerContext
            JopContext workerContext = new JopContext();
            Worker worker = workerContext.Workers.Single(x => x.Id == id);

            return View(worker);
        }

        [HttpPost]
        public ActionResult Edit(Worker worker)
        {
            JopContext workerContext = new JopContext();
            workerContext.Entry(worker).State = System.Data.Entity.EntityState.Modified;
            workerContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            JopContext workerContext = new JopContext();
            Worker worker = workerContext.Workers.Single(x => x.Id == id);

            return View(worker);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            JopContext workerContext = new JopContext();
            Worker worker = workerContext.Workers.Single(x => x.Id == id);

            workerContext.Workers.Remove(worker);
            workerContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            JopContext workerContext = new JopContext();
            Worker worker = workerContext.Workers.Single(x => x.Id == id);

            return View(worker);
        }
    }
}