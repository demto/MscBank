using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class CreditCard : LendingAccount
    {
        public decimal Limit { get; set; }
    }
}