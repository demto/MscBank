using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime? TransactionTimeStamp { get; set; }

        public BankAccountBase parentAccount { get; set; }

        public int BankAccountBaseId { get; set; }
    }
}