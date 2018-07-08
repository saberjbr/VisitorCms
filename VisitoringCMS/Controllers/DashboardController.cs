using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitoringCMS.Models;

namespace VisitoringCMS.Controllers
{
    public class DashboardController : Controller
    {
        VisitoringEntities db = new VisitoringEntities();
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
        public ActionResult CreateVisitor(string name_family, string user_name, string email, string password, HttpPostedFileBase photo)
        {
            /*  var category = new Category();
              category.Name = name;
              category.Photo = Convert.ToBase64String(new BinaryReader(photo.InputStream).ReadBytes(photo.ContentLength));
              db.Categories.Add(category);
              db.SaveChanges();*/
            var visitor = new Visitor();
            visitor.Name = name_family;
            visitor.Username = user_name;
            visitor.Email = email;
            visitor.Password = password;
            visitor.Picture = Convert.ToString(new BinaryReader(photo.InputStream).ReadBytes(photo.ContentLength));
            db.Visitor.Add(visitor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

            public ActionResult CreateAgent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAgent(string name, string address, string lat, string lng)
        {
            db.agent.Add(new agent { Name = name, Address = address, Lat = Convert.ToDouble(lat), Lng = Convert.ToDouble(lng) });
            db.SaveChanges();
            return View();
        }

        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(string name, int price, int sale_price, string desc, string stock, HttpPostedFileBase photo)
        {
            var product = new Product();
            product.Name = name;
            product.Price = Convert.ToDecimal(price);
            product.Sale_Price = Convert.ToDecimal(sale_price);
            product.Description = desc;
            if (!String.IsNullOrEmpty(stock))
            {
                product.In_stock = true;
            }
            else
            {
                product.In_stock = false;

            }

            product.Picture = Convert.ToString(new System.IO.BinaryReader(photo.InputStream).ReadBytes(photo.ContentLength));
            db.Product.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}