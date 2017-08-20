using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.ViewModels {
    public class ATMLoggedInViewModel {

        public BankCard BankCard { get; set; }

        public BankAccountBase Account { get; set; }
    }
}