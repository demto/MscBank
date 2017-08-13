using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
   
    public abstract class BankAccountBase
    {

        public int Id { get; set; }

        public decimal Balance { get; set; }

        public string SortCode { get; set; }

        public string AccountNumber { get; set; }
        
        public virtual List<Transaction> Transactions { get; set; }

        public int TransactionId { get; set; }
    }
}