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
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only digits are allowed")]
        public decimal Amount { get; set; }

        public DateTime? TransactionTimeStamp { get; set; }

        public BankAccountBase ParentAccount { get; set; }

        public int BankAccountBaseId { get; set; }

        public decimal FromCurrentBalance { get; set; }

        public decimal ToCurrentBalance { get; set; }

        public string Description { get; set; }
    }
}