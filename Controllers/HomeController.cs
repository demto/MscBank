using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScBank.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About MSc Bank.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "MSc Bank contact page.";

            return View();
        }

        public ActionResult TAndC() {
            ViewBag.Message = "Terms and conditions";

            return View();
        }
    }
}