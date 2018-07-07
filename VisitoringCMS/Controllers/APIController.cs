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
            var agents = db.agent.Where(x => x.VisitorID == id).Select(x => new { Id = x.Id, Name = x.Name, Address = x.Address, Lat = x.Lat, Lng = x.Lng });

            return Json(agents, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProducts()
        {
            return Json(db.Product.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubmitVisit(string Visitor_id, string Agent_id, string Lat, string Lng)
        {
            if (Visitor_id != null && Agent_id != null && Lat != null && Lng != null)
            {
                var visit = new visit();
                visit.Visitor_id = Convert.ToInt32(Visitor_id);
                visit.Agent_id = Convert.ToInt32(Agent_id);
                var agent = db.agent.Where(x => x.Id == visit.Agent_id).FirstOrDefault();
                if (distance(Convert.ToDouble(agent.Lat), Convert.ToDouble(agent.Lng), Convert.ToDouble(Lat), Convert.ToDouble(Lng), 'M') < 1000)
                {
                    visit.Time = DateTime.Now;
                    db.visit.Add(visit);
                    db.SaveChanges();
                    return Json(new { message = "submited" }, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubmitOrder(string visitor_id, string agent_id, string products_id, string products_count)
        {
            try
            {
                var v_id = Convert.ToInt32(visitor_id);
                var a_id = Convert.ToInt32(agent_id);
                var order = new order
                {
                    Visitor_id = v_id,
                    Agent_Id = a_id,
                    Time = DateTime.Now,
                };
                db.order.Add(order);
                db.SaveChanges();
                var order_id = order.Id;

                var products = products_id.Split(',');
                var conts = products_count.Split(',');
                List<Order_Products> productList = new List<Order_Products>();
                for (int i = 0; i < products.Length; i++)
                {
                    productList.Add(new Order_Products
                    {
                        Order_id = order_id,
                        Product_id = Convert.ToInt32(products[i]),
                        Count = Convert.ToInt32(conts[i])
                    });
                }
                foreach (var temp in productList)
                {
                    db.Order_Products.Add(temp);
                    db.SaveChanges();
                }
                return Json(new { message = "submited" }, JsonRequestBehavior.AllowGet);

            }
            catch
            {
             return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);
            }

             return Json(new { message = "error" }, JsonRequestBehavior.AllowGet); 
        }
        public JsonResult Login(string username, string password)
        {
            return Json(db.Visitor.Where(x => x.Username == username && x.Password == password).Select(x => new { Visitor_Id = x.Id }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


    }
}