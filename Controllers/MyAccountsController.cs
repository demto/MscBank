using Microsoft.AspNet.Identity;
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

        public ActionResult AccountDetails(int accountId) {

            using (var _context = new ApplicationDbContext()) {

                var currentUserId = User.Identity.GetUserId();
                var currentUser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);
                var currentUserAccount = _context.Accounts.SingleOrDefault(a => a.Id == accountId);
                var card = _context.BankCards.SingleOrDefault(c => c.ParentAccount.Id == accountId);
                var transactions = _context.Transactions.Where(t => t.ParentAccount.Id == accountId).ToList();
                var myAccounts = _context.Accounts.Where(a => a.ApplicationUserId == currentUserId).ToList();

                var viewModel = new MyAccountsViewmodel {
                    User = currentUser,
                    MyAccount = currentUserAccount,
                    Card = card,
                    Transactions = transactions,
                    MyAccounts = myAccounts
                };
                    return View(viewModel);
            }                
        }

        public ActionResult CloseAccount(int accountId) {

            using(var _context = new ApplicationDbContext()) {

                var account2remove = _context.Accounts.Single(a => a.Id == accountId);
                
                if(account2remove is CurrentAccount) {
                    CurrentAccount account = account2remove as CurrentAccount;
                    if (account.HasCard()) {
                        BankCard card = _context.BankCards.Single(c => c.ParentAccount.Id == accountId);
                        _context.BankCards.Remove(card);
                    }
                }
                _context.Accounts.Remove(account2remove);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "MyAccounts");
        }
    }
}