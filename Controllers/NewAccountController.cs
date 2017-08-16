using MScBank.Models;
using MScBank.Utils.UserAccountUtils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScBank.Controllers
{
    public class NewAccountController : Controller
    {
        private ApplicationUser currentUser;

        // GET: NewAccount
        public ActionResult Index()
        {
            using(var _context = new ApplicationDbContext()) {
                var uId = User.Identity.GetUserId();
                currentUser = LoadUser.GetCurrentUser(_context, uId);
            }
            
            return View();
        }

        // POST: NewCurrentAccount
        
        public ActionResult DebitAccountForm() {
            
            return View();
        }

        [HttpPost]
        public ActionResult CreateCurrentAccount(CurrentAccount account) {

            using (var _context = new ApplicationDbContext()) {
                var uId = User.Identity.GetUserId();
                currentUser = LoadUser.GetCurrentUser(_context, uId);

                currentUser.MyAccounts.Add(new CurrentAccount {

                    Balance = account.Balance,
                    SortCode = "12-34-56",
                    AccountNumber = account.Id.ToString().PadLeft(8, '0'),
                    Transactions = new List<Transaction>(),
                    ApplicationUserId = currentUser.Id,
                });

                _context.SaveChanges();
            }

            return RedirectToAction("Index", "LoggedIn");
        }
    }
}