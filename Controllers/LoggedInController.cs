using Microsoft.AspNet.Identity;
using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScBank.Controllers
{
    public class LoggedInController : Controller
    {
        // GET: LoggedIn
        public ActionResult Index()
        {
            using(var _context = new ApplicationDbContext()) {

                string userId = User.Identity.GetUserId();

                var currentUser = _context.Users.SingleOrDefault(u => u.Id == userId);

                return View(currentUser);

            }
                
        }
    }
}