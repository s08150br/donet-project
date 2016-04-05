using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotreDameHumber_Website.Controllers
{
    public class ShowJobController : Controller
    {
        HDHDBContext db = new HDHDBContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult All()
        {
            System.Threading.Thread.Sleep(4000); // show update CD time 4second
            List<Job> model = db.Job.ToList();
            return PartialView("_Job", model);
            //  return PartialView("_Student",db.Studets.ToList());
        }
        public PartialViewResult TopThree()
        {
            System.Threading.Thread.Sleep(4000);
            List<Job> model = db.Job.OrderByDescending(x => x.TotalMarks).Take(3).ToList();
            return PartialView("_Job", model);
        }
        public PartialViewResult BottomThree()
        {
            System.Threading.Thread.Sleep(4000);
            List<Job> model = db.Job.OrderBy(x => x.TotalMarks).Take(3).ToList();
            return PartialView("_Job", model);
        }
    }
}