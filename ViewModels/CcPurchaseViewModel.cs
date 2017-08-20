using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.ViewModels {
    public class CcPurchaseViewModel {

        public CreditCard Cc { get; set; }

        public Transaction Transaction { get; set; }
    }
}