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

                BankCard cardfromdb;
                BankAccountBase accountfromdb;

                try {
                    cardfromdb = _context.BankCards.Single(c => c.CardNumber == card.CardNumber);
                    accountfromdb = _context.Accounts.Single(a => a.Id == cardfromdb.BankAccountBaseId);
                    
                }catch(Exception ex) {
                    return RedirectToAction("Index");
                }

                
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
    }
}