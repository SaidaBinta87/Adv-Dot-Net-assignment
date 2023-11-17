using ECS.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECS.Controllers
{
    public class OrderController : Controller
    {
        // GET: Product
        public ActionResult Index(int id = 1)
        {

            var db = new EcommerceEntities1();
            int itemperpage = 20;
            var products = db.Products.OrderBy(e => e.productID).Skip((id - 1) * itemperpage).Take(itemperpage).ToList();

            var pagescount = Math.Ceiling(db.Products.Count() / (decimal)itemperpage);
            ViewBag.Pages = pagescount;
            return View(products);


        }

        public ActionResult AddCart(int id)
        {
            EcommerceEntities1 db = new EcommerceEntities1();
            List<Product> cart = null;
            if (Session["cart"] == null)
            {
                cart = new List<Product>();
            }
            else
            {
                cart = (List<Product>)Session["cart"];
            }

            var product = db.Products.Find(id);

            cart.Add(new Product()
            {
                productID = product.productID,
                Pname = product.Pname,
                Category = product.Category,
                Price = product.Price,
            });
            Session["cart"] = cart;

            TempData["Msg"] = "Successfully Added";
            return RedirectToAction("Index");
        }


        public ActionResult Cart()
        {

            var cart = (List<Product>)Session["cart"];
            if (cart != null)
            {
                return View(cart);
            }
            TempData["Msg"] = "Cart Empty";
            return RedirectToAction("Index");

            //order.Date = DateTime.Now;
        }
        public ActionResult Place()
        {
            if (Session["user"] == null)
            {
                // Handle the case where the user is not logged in
                return RedirectToAction("Login");
            }
            int userID = ((User)Session["user"]).UserID;
            int orderSum = 0;
            var cart = (List<Product>)Session["cart"];
            Order order = new Order();
            order.Useid = userID;

            order.Amount = 0;
            EcommerceEntities1 db = new EcommerceEntities1();
            db.Orders.Add(order);
            db.SaveChanges();
            foreach (var p in cart)
            {
                var od = new OrderItem();
                od.Quantity = +1;
               od.OID = order.orderID;
                od.PID = p.productID;
                od.Subtotal = (int)p.Price;
                od.Quantity = 1;
                orderSum += (1 * (int)p.Price);
                db.OrderItems.Add(od);
            }
            order.Amount = orderSum;
            db.SaveChanges();
            Session["cart"] = null;
            TempData["Msg"] = "Order Placed Successfully";
            return RedirectToAction("Index");

        }

    }
}