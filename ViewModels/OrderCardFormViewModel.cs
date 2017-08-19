using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.ViewModels {
    public class OrderCardFormViewModel {

        public BankAccountBase Account { get; set; }
        public int AccountId { get; set; }
        public BankCard Card { get; set; }
    }
}