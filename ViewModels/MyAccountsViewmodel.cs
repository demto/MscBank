using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.ViewModels {
    public class MyAccountsViewmodel {

        public ApplicationUser User { get; set; }

        public BankAccountBase MyAccount { get; set; }

        public BankCard Card { get; set; }

        public IEnumerable<BankAccountBase> MyAccounts { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}