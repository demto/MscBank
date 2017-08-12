using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public abstract class LendingAccount : BankAccountBase, IInterestBearing
    {
        public double InterestRate { get; set; }

        public decimal Payment { get; set; }
        public void AdjustBalanceWithInterest()
        {
            //must apply interest formula
        }
    }
}