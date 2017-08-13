using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MScBank.Controllers
{

    // this is only a test which worked fine so will leave here as a sample for future reference

    public class AddAccountController : Controller
    {
        // GET: AddAccount
        public ActionResult Index()
        {
            var account = new CurrentAccount {

                Balance = 2000,
                SortCode = "123456",
                AccountNumber = "12345678"
            };

            var customer = new ApplicationUser {
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(2000, 1, 18),
                MyAccounts = new List<BankAccountBase>(),
                Email = "asdf@asdf.com",
                Address = "afadasd"
            };
            

            customer.MyAccounts.Add(account);

            return View(customer);
        }
    }
}