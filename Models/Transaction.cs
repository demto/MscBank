using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public decimal Amount { get; set; }
        
        public virtual BankAccountBase ParentAccount { get; set; }

        public int ParentAccountId { get; set; }
    }
}