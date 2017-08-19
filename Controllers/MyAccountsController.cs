using Microsoft.AspNet.Identity;
using MScBank.Models;
using MScBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                List<Transfer> transfers = _context.Transfers.Where(t => t.ToAccountId == accountId).ToList();
                var myAccounts = _context.Accounts.Where(a => a.ApplicationUserId == currentUserId).ToList();

                transactions.AddRange(transfers);

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

        [HttpPost]
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
            return RedirectToAction("Index", "LoggedIn");
        }
        
        public ActionResult TransferFunds(int accountId) {

            using(var _context = new ApplicationDbContext()) {

                var fromAccount = _context.Accounts.Single(a => a.Id == accountId);
                
                    //var transfer = new Transfer {
                    //     BankAccountBaseId = accountId
                    //};

                var viewModel = new TransferFundsViewModel {
                    FromAccount = fromAccount,
                    FromAccountId = accountId
                };

                    return View(viewModel);
                
            }            
        }
     
        [HttpPost]
        public ActionResult Transfer(TransferFundsViewModel viewModel) {

            using (var _context = new ApplicationDbContext()) {

                //creating return viewmodel
                TransferFundsViewModel backViewModel = new TransferFundsViewModel {

                    FromAccount = _context.Accounts.Single(a => a.Id == viewModel.FromAccountId),

                };

                //validation
                if (!ModelState.IsValid) {                   

                    return View("TransferFunds", backViewModel);
                }

                //checking balance
                decimal currentBalance = _context.Accounts.Single(a => a.Id == viewModel.FromAccountId).Balance;
                
                if(viewModel.Amount > currentBalance) {

                    return View("TransferFunds", backViewModel);
                }

                //making the transaction
                var fromAccount = _context.Accounts.Single(a => a.Id == viewModel.FromAccountId);
                var toAccount = _context.Accounts.Where(a => a.SortCode == viewModel.ToAccountSC)
                                                 .Single(a => a.AccountNumber == viewModel.ToAccountAcNum);

                //can only transfer to debit accounts
                if(toAccount is CurrentAccount || toAccount is SavingsAccount) {
                    fromAccount.Balance -= viewModel.Amount;
                    toAccount.Balance += viewModel.Amount;
                } else {
                    return View("TransferFunds", backViewModel);
                }

                
                //creating and adding transaction
                var transaction = new Transfer {
                    Amount = viewModel.Amount,
                    BankAccountBaseId = viewModel.FromAccountId,
                    ToAccountId = toAccount.Id,
                    TransactionTimeStamp = DateTime.Now
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                //string uId = User.Identity.GetUserId();
                //user = _context.Users.Single(c => c.Id == uId);
            }
            return RedirectToAction("Index", "LoggedIn");            
        }
    }
}