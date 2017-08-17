using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class Loan : LendingAccount
    {
        [Required]
        public int Term { get; set; }

        [Required]
        public decimal LendingAmount { get; set; }
        
    }
}