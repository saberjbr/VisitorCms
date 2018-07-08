using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VisitoringCMS.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateVisitor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVisitor(string str)
        {
            return View();
        }

        public ActionResult CreateAgent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAgent(string str)
        {
            return null;
        }

        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(string str)
        {
            return View();
        }
    }
}