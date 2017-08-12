using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class Loan : LendingAccount
    {
        public int Term { get; set; }

        public decimal LendingAmount { get; set; }
        
    }
}