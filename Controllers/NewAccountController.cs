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
        
        // GET: NewAccount
        public ActionResult Index()
        {
            //using(var _context = new ApplicationDbContext()) {
            //    var uId = User.Identity.GetUserId();
            //    var currentUser = LoadUser.GetCurrentUser(_context, uId);
            //}
            
            return View();
        }

        // POST: NewCurrentAccount
        
        public ActionResult DebitAccountForm() {
            
            return View();
        }

        [HttpPost]
        public ActionResult CreateCurrentAccount(CurrentAccount account) {

            var uId = User.Identity.GetUserId();     

            using (var _context = new ApplicationDbContext()) {

                var accountId = 0;

                var existingAccounts = _context.Accounts.Where(a => a.Id == accountId);
                var highestAccountId = _context.Accounts.Max(a => a.Id);
                accountId = highestAccountId++;

                var newCurrentAccount = new CurrentAccount {

                    Balance = account.Balance,
                    SortCode = "12-34-56",
                    Transactions = new List<Transaction>(),
                    AccountNumber = highestAccountId.ToString().PadLeft(8, '0'),
                    ApplicationUserId = uId
                };

                _context.Accounts.Add(newCurrentAccount);
                _context.SaveChanges();

               
            }
                
            return RedirectToAction("Index", "LoggedIn");
        }
    }
}