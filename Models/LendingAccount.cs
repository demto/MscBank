using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public abstract class LendingAccount : BankAccountBase, IInterestBearing
    {
        public double InterestRate { get; set; }

        [Required]
        public decimal Payment { get; set; }

        public void AdjustBalanceWithInterest()
        {
            //must apply interest formula
        }
    }
}