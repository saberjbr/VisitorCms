using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitoringCMS.Models;
using Newtonsoft.Json;
namespace VisitoringCMS.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        VisitoringEntities db = new VisitoringEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetVisitorAgents(string visitor_id)
        {
            //  Visitor v = db.Visitor.ToList().FirstOrDefault();
            // var json = Json(v);
            // var vi = JsonConvert.DeserializeObject<Visitor>(visitor);
            var id = Convert.ToInt32(visitor_id);
            var agents = db.agent.Where(x => x.VisitorID == id).Select(x=>new {Id=x.Id,Name=x.Name,Address=x.Address,Lat=x.Lat,Lng=x.Lng });
     
            return Json(agents, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProducts()
        {
            return Json(db.Product.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitVisit(Visitor v,agent agnt)
        {
            return null;
        }
        public JsonResult SubmitOrder(Visitor v,agent agnt,string order)
        {
            return null;
        }
        public JsonResult Login(string username,string password)
        {
            return Json(db.Visitor.Where(x=>x.Username==username&&x.Password==password).Select(x=>new {Visitor_Id=x.Id}).FirstOrDefault(),JsonRequestBehavior.AllowGet);
        }

    }
}