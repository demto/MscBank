using MScBank.Models;
using MScBank.Utils.BankAccountUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.ViewModels {
    public class NewAccountViewModel {

        public ApplicationUser User { get; set; }

        public double Rate { get; set; }

        public BankAccountBase Account { get; set; }

        public LendingAccount LendingAccount { get; set; }

        public CurrentAccount CurrentAccount { get; set; }

        public SavingsAccount SavingsAccount { get; set; }

        public CreditCard CreditCard { get; set; }

        public Loan Loan { get; set; }

        public Mortgage Mortgage { get; set; }
    }
}