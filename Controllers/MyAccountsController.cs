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
        
        public ActionResult CloseAccount(int accountId) {
            
            using(var _context = new ApplicationDbContext()) {

                var account2remove = _context.Accounts.Single(a => a.Id == accountId);

                _context.Accounts.Remove(account2remove);

                if (account2remove is CurrentAccount) {
                    CurrentAccount account = account2remove as CurrentAccount;
                    if (account.HasCard()) {
                        BankCard card = _context.BankCards.Single(c => c.ParentAccount.Id == accountId);
                        _context.BankCards.Remove(card);
                       
                    }
                }

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

                var uId = User.Identity.GetUserId();

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
                    TransactionTimeStamp = DateTime.Now,
                    CurrentBalance = _context.Accounts.Where(a => a.ApplicationUserId == uId).First(a => a is CurrentAccount).Balance
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                //string uId = User.Identity.GetUserId();
                //user = _context.Users.Single(c => c.Id == uId);
            }
            return RedirectToAction("Index", "LoggedIn");            
        }
        

        public ActionResult PayCreditCard(int accountId) {

            using (var _context = new ApplicationDbContext()) {

                var uId = User.Identity.GetUserId();


                var fromAccount = _context.Accounts.Single(a => a.Id == accountId);
                var toACcount = _context.Accounts.Where(a => a is CreditCard).Single(a => a.ApplicationUserId == uId);

                //var transfer = new Transfer {
                //     BankAccountBaseId = accountId
                //};

                var viewModel = new TransferFundsViewModel {
                    FromAccount = fromAccount,
                    FromAccountId = accountId,
                    ToAccountAcNum = toACcount.AccountNumber,
                    ToAccountSC = toACcount.SortCode
                };

                return View("PayCredit", viewModel);
            }
        }

        public ActionResult PayLoan(int accountId) {

            using (var _context = new ApplicationDbContext()) {

                var uId = User.Identity.GetUserId();


                var fromAccount = _context.Accounts.Single(a => a.Id == accountId);
                var toACcount = _context.Accounts.Where(a => a.Type == "Loan Account").Single(a => a.ApplicationUserId == uId);

                //var transfer = new Transfer {
                //     BankAccountBaseId = accountId
                //};

                var viewModel = new TransferFundsViewModel {
                    FromAccount = fromAccount,
                    FromAccountId = accountId,
                    ToAccountAcNum = toACcount.AccountNumber,
                    ToAccountSC = toACcount.SortCode
                };

                return View("PayCredit", viewModel);
            }
        }

        public ActionResult PayMortgage(int accountId) {

            using (var _context = new ApplicationDbContext()) {

                var uId = User.Identity.GetUserId();


                var fromAccount = _context.Accounts.Single(a => a.Id == accountId);
                var toACcount = _context.Accounts.Where(a => a.Type == "Mortgage Account").Single(a => a.ApplicationUserId == uId);

                //var transfer = new Transfer {
                //     BankAccountBaseId = accountId
                //};

                var viewModel = new TransferFundsViewModel {
                    FromAccount = fromAccount,
                    FromAccountId = accountId,
                    ToAccountAcNum = toACcount.AccountNumber,
                    ToAccountSC = toACcount.SortCode
                };

                return View("PayCredit", viewModel);
            }
        }

        [HttpPost]
        public ActionResult Pay (TransferFundsViewModel model) {

            using(var _context = new ApplicationDbContext()) {

                //creating backviewmodel
                var uId = User.Identity.GetUserId();
                int aId = model.FromAccountId;

                var fromAccount = _context.Accounts.Single(a => a.Id == aId);
                var toAccount = _context.Accounts.Where(a => a.SortCode == model.ToAccountSC).Single(a => a.AccountNumber == model.ToAccountAcNum);

                var backViewModel = new TransferFundsViewModel {
                    FromAccount = fromAccount,
                    FromAccountId = aId,
                    ToAccountAcNum = toAccount.AccountNumber,
                    ToAccountSC = toAccount.SortCode
                };

                //validating
                if (!ModelState.IsValid) {

                    
                    return View("PayCredit", backViewModel);
                }

                //checking balance
                decimal currentBalance = _context.Accounts.Single(a => a.Id == model.FromAccountId).Balance;

                if (model.Amount > currentBalance) {

                    return View("PayCredit", backViewModel);
                }

                //make the transaction

                fromAccount.Balance -= model.Amount;
                toAccount.Balance += model.Amount;


                //creating and adding transaction

                var transaction = new Transfer {
                    Amount = model.Amount,
                    BankAccountBaseId = model.FromAccountId,
                    ToAccountId = toAccount.Id,
                    TransactionTimeStamp = DateTime.Now,
                    CurrentBalance = _context.Accounts.Where(a => a.ApplicationUserId == uId).First(a => a is CurrentAccount).Balance
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "LoggedIn");
        }

        public ActionResult OrderCard(int accountId) {

            using (var _context = new ApplicationDbContext()) {

                var account = _context.Accounts.Single(a => a.Id == accountId);

                var viewModel = new OrderCardFormViewModel {
                    Account = account,
                    AccountId = accountId
                };

                return View("OrderCardForm", viewModel);
            }
        }

        public ActionResult processCardOrder(OrderCardFormViewModel model) {

            using(var _context = new ApplicationDbContext()) {

                string uId = User.Identity.GetUserId();
                var user = _context.Users.Single(u => u.Id == uId);
                var account = (CurrentAccount) _context.Accounts.Single(a => a.Id == model.AccountId);


                var highestCardId = 0;
                var newCardId = 0;

                if(_context.BankCards.Any()){

                    if(_context.BankCards.Max(c => c.Id) > 0) {

                        highestCardId = _context.BankCards.Max(c => c.Id);
                        newCardId = highestCardId++;
                    }
                }

                BankCard newCard = new BankCard {
                    BankAccountBaseId = model.AccountId,
                    ExpiryDate = DateTime.Today.AddYears(2),
                    Status = BankCard.CardStatus.Open,
                    NameOnCard = user.FullName,
                    PinNumber = account.Id,
                    CardNumber = newCardId.ToString().PadLeft(16, '0')
                };

                _context.BankCards.Add(newCard);
                _context.SaveChanges();

                    return RedirectToAction("Index", "LoggedIn");
            }
            
        }
    }
}