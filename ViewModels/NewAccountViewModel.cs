using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.ViewModels {
    public class NewAccountViewModel {

        public ApplicationUser User { get; set; }

        public List<BankAccountBase> MyAccounts { get; set; }
    }
}