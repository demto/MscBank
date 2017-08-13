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

                var account = new CurrentAccount {

                    Balance = 2000,
                    SortCode = "123456",
                    AccountNumber = "12345678"
                };

                var account2 = new SavingsAccount {

                    Balance = 4000,
                    SortCode = "123456",
                    AccountNumber = "12345678"
                };

                
                var accounts = new List<BankAccountBase>();
                    accounts.Add(account);
                accounts.Add(account2);


                // load in current user
                string userId = User.Identity.GetUserId();

                var currentUser = _context.Users.SingleOrDefault(u => u.Id == userId);
                currentUser.MyAccounts = new List<BankAccountBase>();

                // load in current user's accounts
                var accountsFromDb = _context.Accounts.Where(a => a.ApplicationUserId == userId);

                if (accountsFromDb.Any()) {

                    currentUser.MyAccounts.AddRange(accountsFromDb);
                } else {
                    currentUser.MyAccounts.AddRange(accounts);
                    
                }               

                return View(currentUser);

            }
                
        }
    }
}