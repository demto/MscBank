using MScBank.Models;
using MScBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScBank.Controllers
{
    public class ATMController : Controller
    {
        // GET: ATM
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(BankCard card) {

            //validation
            if (!ModelState.IsValid) {
                return View("Index");
            }

            using(var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.CardNumber == card.CardNumber);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);
                    
                }catch(Exception ex) {
                    return RedirectToAction("Index");
                }

                //validate pin number
                if(cardfromdb.PinNumber != card.PinNumber) {
                    return View("Index");
                } else {

                    var model = new ATMLoggedInViewModel {
                        Account = accountfromdb,
                        BankCard = cardfromdb
                    };

                    return View("LoggedIn", model);
                }
            }
        }

        public ActionResult Balance(ATMLoggedInViewModel viewModel) {

            using (var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.Id == viewModel.BankCard.Id);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);

                } catch (Exception ex) {
                    return RedirectToAction("Index");
                }

                var model = new ATMLoggedInViewModel {
                    Account = accountfromdb,
                    BankCard = cardfromdb
                };

                return View("Balance", model);
                
            }
        
        }

        public ActionResult WithdrawForm(ATMLoggedInViewModel viewModel) {

            using (var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.Id == viewModel.BankCard.Id);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);

                } catch (Exception ex) {
                    return RedirectToAction("Index");
                }

                var model = new ATMLoggedInViewModel {
                    Account = accountfromdb,
                    BankCard = cardfromdb
                };

                return View("WithdrawForm", model);

            }

        }

        [HttpPost]
        public ActionResult Withdraw(ATMLoggedInViewModel viewModel) {

            using (var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.Id == viewModel.BankCard.Id);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);

                } catch (Exception ex) {
                    return RedirectToAction("Index");
                }

                var model = new ATMLoggedInViewModel {
                    Account = accountfromdb,
                    BankCard = cardfromdb
                };

                var transaction = new Transaction {
                    Amount = viewModel.Transaction.Amount,
                    BankAccountBaseId = accountfromdb.Id,
                    Description = viewModel.Transaction.Description,
                    FromCurrentBalance = accountfromdb.Balance,
                    TransactionTimeStamp = DateTime.Now
                };

                if(viewModel.Transaction.Amount < accountfromdb.Balance) {
                    accountfromdb.Balance -= viewModel.Transaction.Amount;
                }else {
                    return View("LoggedIn", model);
                }
                

                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                return View("Balance", model);

            }

        }

        public ActionResult DepositForm(ATMLoggedInViewModel viewModel) {

            using (var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.Id == viewModel.BankCard.Id);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);

                } catch (Exception ex) {
                    return RedirectToAction("Index");
                }

                var model = new ATMLoggedInViewModel {
                    Account = accountfromdb,
                    BankCard = cardfromdb
                };

                return View("DepositForm", model);

            }

        }

        [HttpPost]
        public ActionResult Deposit(ATMLoggedInViewModel viewModel) {

            using (var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.Id == viewModel.BankCard.Id);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);

                } catch (Exception ex) {
                    return RedirectToAction("Index");
                }

                var model = new ATMLoggedInViewModel {
                    Account = accountfromdb,
                    BankCard = cardfromdb
                };

                var transaction = new Transaction {
                    Amount = viewModel.Transaction.Amount,
                    BankAccountBaseId = accountfromdb.Id,
                    Description = viewModel.Transaction.Description,
                    FromCurrentBalance = accountfromdb.Balance,
                    TransactionTimeStamp = DateTime.Now
                };

                
                accountfromdb.Balance += viewModel.Transaction.Amount;
               
                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                return View("Balance", model);

            }

        }

        public ActionResult ChangePin(ATMLoggedInViewModel viewModel) {
            using (var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.Id == viewModel.BankCard.Id);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);

                } catch (Exception ex) {
                    return View("LoggedIn");
                }

                var model = new ATMLoggedInViewModel {
                    Account = accountfromdb,
                    BankCard = cardfromdb
                };

                return View("ChangePin", model);

            }
        }

        public ActionResult ProcessChangePIn(ATMLoggedInViewModel viewModel) {

            using (var _context = new ApplicationDbContext()) {

                //get card and account from db
                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.Id == viewModel.BankCard.Id);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);

                } catch (Exception ex) {
                    return RedirectToAction("Index");
                }

                var model = new ATMLoggedInViewModel {
                    Account = accountfromdb,
                    BankCard = cardfromdb
                };
                
                //if(viewModel.BankCard.OldPinNumber != cardfromdb.PinNumber) {
                //    return View("LoggedIn", model);
                //} else {
                //    cardfromdb.PinNumber = viewModel.BankCard.PinNumber;
                //}
                
                _context.SaveChanges();

                return View("Balance", model);
            }
        }

        public ActionResult Exit() {
            return RedirectToAction("Index");
        }
    }
}