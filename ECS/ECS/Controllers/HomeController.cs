using ECS.EF;
using ECS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult AllOrders()
        {
            var db = new EcommerceEntities1();
            return View(db.Orders.ToList());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var db = new EcommerceEntities1();
            var order = db.Orders.Find(id);
            return View(order);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
      
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                EcommerceEntities1 db = new EcommerceEntities1();
                var user = (from u in db.Users
                            where u.username.Equals(login.Username)
                            && u.passowrd.Equals(login.Password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user;
                    var returnUrl = Request["ReturnUrl"];
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Order");
                }
                TempData["Msg"] = "Username Password Invalid";
            }
            return View(login);

        }

        public ActionResult Process(int id)
        {
            EcommerceEntities1 ctx = new EcommerceEntities1();
            var order = ctx.Orders.Find(id);
            foreach (var od in order.OrderItems)
            {
                
                int Qty = 1000;
                Qty -= od.Quantity;
            }
            
            ctx.SaveChanges();
            TempData["Msg"] = "Orded Placed Successfully";
            return RedirectToAction("AllOrders");

        }

        public ActionResult Cancel(int id)
        {
            EcommerceEntities1 db = new EcommerceEntities1();
            //var order = db.Orders.Find(id);
            //order.Status = "Cancelled by Admin";
            db.SaveChanges();
            TempData["Msg"] = "Orded Cancelled";
            return RedirectToAction("AllOrders");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}