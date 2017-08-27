using Microsoft.AspNet.Identity;
using MScBank.Models;
using MScBank.Utils.UserAccountUtils;
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

                // load in current userId
                string userId = User.Identity.GetUserId();

                // Load in customer
                ApplicationUser currentUser = LoadUser.GetCurrentUser(_context, userId);

                return View(currentUser);
            }
                
        }

        public ActionResult MyDetails() {

            using(var _context = new ApplicationDbContext()) {
                var userId = User.Identity.GetUserId();
                var user = _context.Users.Single(u => u.Id == userId);


            return View(user);
            }
            
        }
    }
}