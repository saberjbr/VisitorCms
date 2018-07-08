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
            ViewBag.Visitors = db.Visitor.Count();
            ViewBag.Orders = db.order.Count();
            ViewBag.Products = db.Product.Count();
            ViewBag.Agents = db.agent.Count();
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

            visitor.Picture = ImageToBase64(photo);
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

            product.Picture = ImageToBase64(photo);
            db.Product.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string user,string pass)
        {
            return View();
        }
        public ActionResult ViewProducts()
        {
            ViewBag.Products = db.Product.ToList();
            return View();
        }
        public ActionResult ViewAgents()
        {
            ViewBag.Agents = db.agent.ToList();
            return View();
        }
        public ActionResult ViewVisitors()
        {
            ViewBag.Visitors = db.Visitor.ToList();
            return View();
        }
        public string ImageToBase64(HttpPostedFileBase cvFile)
        {
            byte[] fileInBytes = new byte[cvFile.ContentLength];
            using (BinaryReader theReader = new BinaryReader(cvFile.InputStream))
            {
                fileInBytes = theReader.ReadBytes(cvFile.ContentLength);
            }
            string fileAsString = Convert.ToBase64String(fileInBytes);
            return ("data:image/jpeg;base64,"+fileAsString);
        }
    }
}