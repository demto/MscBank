using Microsoft.AspNet.Identity;
using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MScBank.Utils.UserAccountUtils {
    public static class LoadUser {
                        
        public static ApplicationUser GetCurrentUser(ApplicationDbContext _context, string uId) {

            // load in current user
                ApplicationUser currentUser; 
                currentUser = _context.Users.SingleOrDefault(u => u.Id == uId);
                currentUser.MyAccounts = new List<BankAccountBase>();

            // load in current user's accounts
                var accountsFromDb = _context.Accounts.Where(a => a.ApplicationUserId == uId);

                if (accountsFromDb.Any()) {

                    currentUser.MyAccounts.AddRange(accountsFromDb);
                }

            return currentUser;
            
        }
    }
}
