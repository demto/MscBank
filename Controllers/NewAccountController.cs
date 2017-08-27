using MScBank.Models;
using MScBank.Utils.UserAccountUtils;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MScBank.ViewModels;
using MScBank.Utils.BankAccountUtils;

namespace MScBank.Controllers
{
    public class NewAccountController : Controller
    {
        
        // GET: NewAccount
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: NewCurrentAccount
        
        public ActionResult DebitAccountForm() {
            
            return View();
        }

        public ActionResult SavingsAccountForm() {

            var viewModel = new NewAccountViewModel {
                Rate = 1
            };

            return View();
        }

        public ActionResult CreditCardForm() {

            var viewModel = new NewAccountViewModel {
                Rate = 20
            };

            return View(viewModel);
        }

        public ActionResult LoanForm() {

            var viewModel = new NewAccountViewModel {
                Rate = 10
            };

            return View(viewModel);
        }

        public ActionResult MortgageForm() {

            var viewModel = new NewAccountViewModel {
                Rate = 2
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult CreateCurrentAccount(CurrentAccount account) {

            if (!ModelState.IsValid) {

                return View("DebitAccountForm");
            }

            try {
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
                        ApplicationUserId = uId,
                        Name = account.Name,
                        Type = "Current Account",
                        OpenDate = DateTime.Now
                    };

                    var transaction = new Transaction {
                        Amount = account.Balance,
                        TransactionTimeStamp = DateTime.Now,
                        BankAccountBaseId = account.Id,
                        ToCurrentBalance = account.Balance
                    };

                    _context.Transactions.Add(transaction);
                    _context.Accounts.Add(newCurrentAccount);
                    _context.SaveChanges();
                }
            } catch (FormatException ex) {

                return View("DebitAccountForm");
            }

            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult CreateSavingsAccount(SavingsAccount account) {

            if (!ModelState.IsValid) {

                return View("SavingsAccountForm");
            }

            var uId = User.Identity.GetUserId();

            using (var _context = new ApplicationDbContext()) {

                var accountId = 0;

                var existingAccounts = _context.Accounts.Where(a => a.Id == accountId);
                var highestAccountId = _context.Accounts.Max(a => a.Id);
                accountId = highestAccountId++;

                var newSavingsAccount = new SavingsAccount {

                    Balance = account.Balance,
                    InterestRate = 1,
                    SortCode = "12-34-56",
                    Transactions = new List<Transaction>(),
                    AccountNumber = highestAccountId.ToString().PadLeft(8, '0'),
                    ApplicationUserId = uId,
                    Name = account.Name,
                    Type = "Savings Account",
                    OpenDate = DateTime.Now
                };

                var transaction = new Transaction {
                    Amount = account.Balance,
                    TransactionTimeStamp = DateTime.Now,
                    BankAccountBaseId = account.Id,
                    ToCurrentBalance = account.Balance
                };

                _context.Transactions.Add(transaction);
                _context.Accounts.Add((SavingsAccount) newSavingsAccount);
                _context.SaveChanges();

            }

            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult CreateCreditCardAccount(NewAccountViewModel model) {

            if (!ModelState.IsValid) {

                return View("CreaditCardForm");
            }

            using (var _context = new ApplicationDbContext()) {

                var uId = User.Identity.GetUserId();
                var user = _context.Users.Single(u => u.Id == uId);

                //if already has a credit card
                if(_context.Accounts.Where(a => a.ApplicationUserId == uId)
                                    .Where(a => a is CreditCard).Count() > 0) {
                    return View("CreditCardForm");
                }
            

                var accountId = 0;

                //generating accoun number
                var existingAccounts = _context.Accounts.Where(a => a.Id == accountId);
                var highestAccountId = _context.Accounts.Max(a => a.Id);
                accountId = highestAccountId++;

                //creating new credit card
                var newCc = new CreditCard {

                    Limit = model.CreditCard.Limit,
                    InterestRate = 20,
                    SortCode = "12-34-56",
                    Transactions = new List<Transaction>(),
                    AccountNumber = highestAccountId.ToString().PadLeft(8, '0'),
                    ApplicationUserId = uId,
                    Name = model.CreditCard.Name,
                    Type = "Credit Card Account",
                    OpenDate = DateTime.Now
                };

                _context.Accounts.Add(newCc);
                _context.SaveChanges();


            }

            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult CreateLoanAccount(Loan loan) {

            if (!ModelState.IsValid) {

                return View("LoanForm");
            }

            if (loan.Term > 7 || loan.Term < 1 || loan.LendingAmount < 1000 || loan.LendingAmount > 25000) {
                return View("LoanForm");
            }
             

            using (var _context = new ApplicationDbContext()) {

                var uId = User.Identity.GetUserId();
                var user = _context.Users.Single(u => u.Id == uId);

                //var accounts = _context.Accounts.Where(a => a.ApplicationUserId == uId);
                //user.MyAccounts.AddRange(accounts);

                //if already has a credit card
                if (_context.Accounts.Where(a => a.ApplicationUserId == uId)
                                     .Where(a => a.Type == "Loan Account")
                                     .Count() > 0) {
                    return View("LoanForm");
                }


                var accountId = 0;

                //var existingAccounts = _context.Accounts.Where(a => a.Id == accountId);
                var highestAccountId = _context.Accounts.Max(a => a.Id);
                accountId = highestAccountId++;

                var newLoan = new Loan {

                    Payment = 1, // needs to be calculated
                    LendingAmount = loan.LendingAmount,
                    Balance = -(loan.LendingAmount),
                    Term = loan.Term,
                    InterestRate= 10,
                    SortCode = "12-34-56",
                    Transactions = new List<Transaction>(),
                    AccountNumber = highestAccountId.ToString().PadLeft(8, '0'),
                    ApplicationUserId = uId,
                    Name = loan.Name,
                    Type = "Loan Account",
                    OpenDate = DateTime.Now
                };

                //save newloan
                _context.Accounts.Add(newLoan);
                _context.SaveChanges();

                //adding borrowed balance to CA
                var ca = _context.Accounts.Where(a => a.ApplicationUserId == uId).First(a => a is CurrentAccount);

                ca.Balance += loan.LendingAmount;

                //creating and adding transaction
                var transaction = new Transfer {
                    Amount = loan.LendingAmount,
                    BankAccountBaseId = loan.Id,
                    ToAccountId = ca.Id,
                    TransactionTimeStamp = DateTime.Now,
                    ToCurrentBalance = _context.Accounts.Where(a => a.ApplicationUserId == uId).First(a => a is CurrentAccount).Balance,
                    FromCurrentBalance = _context.Accounts.Single(a => a.Id == loan.Id).Balance
                };

                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult CreateMortgageAccount(Mortgage mortgage) {

            if (!ModelState.IsValid) {

                return View("MortgageForm");
            }

            if(mortgage.Term > 35 || mortgage.Term < 5 || mortgage.LendingAmount < 5000) {
                return View("MortgageForm");
            }
            
            using (var _context = new ApplicationDbContext()) {


                var uId = User.Identity.GetUserId();
                var user = _context.Users.Single(u => u.Id == uId);

                //if already has a credit card
                if (user.MyAccounts.Where(a => a is Mortgage).Count() > 0) {
                    return View("MortgageForm");
                }

                var accountId = 0;

                var existingAccounts = _context.Accounts.Where(a => a.Id == accountId);
                var highestAccountId = _context.Accounts.Max(a => a.Id);
                accountId = highestAccountId++;

                var newMortgage = new Mortgage {

                    Payment = 1, // needs to be calculated
                    LendingAmount = mortgage.LendingAmount,
                    Balance = -(mortgage.LendingAmount),
                    Term = mortgage.Term,
                    InterestRate = 2,
                    SecurityAddress = mortgage.SecurityAddress,
                    SortCode = "12-34-56",
                    Transactions = new List<Transaction>(),
                    AccountNumber = highestAccountId.ToString().PadLeft(8, '0'),
                    ApplicationUserId = uId,
                    Name = mortgage.Name,
                    Type = "Mortgage Account",
                    OpenDate = DateTime.Now
                };

                _context.Accounts.Add(newMortgage);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "LoggedIn");
        }
    }
}