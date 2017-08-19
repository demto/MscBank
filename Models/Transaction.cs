using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime? TransactionTimeStamp { get; set; }

        public BankAccountBase ParentAccount { get; set; }

        public int BankAccountBaseId { get; set; }
    }
}