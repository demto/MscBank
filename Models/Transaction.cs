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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime? TransactionTimeStamp { get; set; }

        public BankAccountBase parentAccount { get; set; }

        public int BankAccountBaseId { get; set; }
    }
}