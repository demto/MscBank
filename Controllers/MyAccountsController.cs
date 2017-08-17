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
    }
}