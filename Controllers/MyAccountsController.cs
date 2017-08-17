using MScBank.Models;
using MScBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScBank.Controllers
{
    public class MyAccountsController : Controller
    {
        // GET: MyAccounts
        public ActionResult Index(ApplicationUser user)
        {

            MyAccountsViewmodel viewModel;

            using(var _context = new ApplicationDbContext()) {

                    viewModel = new MyAccountsViewmodel {

                    User = user,
                    MyAccounts = _context.Accounts.Where(a => a.ApplicationUserId == user.Id).ToList()

                };
            }
            

            return View(viewModel);
        }

        public ActionResult AccountDetails(string uId) {

            using (var _context = new ApplicationDbContext()) {

                var currentUser = (ApplicationUser)_context.Users.Where(u => u.Id == uId);
                var currentUserAccounts = _context.Accounts.Where(a => a.ApplicationUserId == uId).ToList();
                var cards = _context.BankCards.Where(c => c.ParentAccount.Owner == currentUser).ToList();
                var transactions = _context.Transactions.Where(t => t.ParentAccount.Owner == currentUser);

                var viewModel = new MyAccountsViewmodel {
                    User = currentUser,
                    MyAccounts = currentUserAccounts,
                    Card = cards,
                    Transactions = transactions
                };

                return View();
            }
                
        }
    }
}