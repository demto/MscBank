using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class SavingsAccount : BankAccountBase, IInterestBearing
    {
        public double InterestRate { get; set; }

        public void AdjustBalanceWithInterest()
        {
            double rate = 1 + InterestRate / 100;
            this.Balance = Balance * (decimal) rate;
        }
    }
}