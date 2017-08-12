using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public abstract class BankAccountBase
    {
        public int AccountID { get; set; }

        public decimal Balance { get; set; }

        public string SortCode { get; set; }

        public string AccountNumber { get; set; }

        public virtual int TransactionId { get; set; }

        public List<Transaction> transactions { get; set; }

        public ApplicationUser Owner { get; set; }

        public int ApplicationUserId { get; set; }
    }
}