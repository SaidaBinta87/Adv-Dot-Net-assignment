using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1MyCV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Education()
        {
            ViewBag.Message = "Education Information";

            return View();
        }
        public ActionResult ProjectInfo()
        {
            return View();

        }
      

        public ActionResult Reference()
        {

            return View();
        }
    }
}