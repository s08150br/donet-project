using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace partialview.Controllers
{
    public class CompanyContext : Controller
    {
        // GET: CompanyContext
        public ActionResult Index()
        {
            return View();
        }
    }
}