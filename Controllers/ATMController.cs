using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScBank.Controllers
{
    public class ATMController : Controller
    {
        // GET: ATM
        public ActionResult Index()
        {
            return View();
        }
    }
}