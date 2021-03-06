﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public abstract class LendingAccount : BankAccountBase, IInterestBearing
    {
        [Required]
        public double InterestRate { get; set; }
        
        public decimal Payment { get; set; }

        public void AdjustBalanceWithInterest()
        {
            //must apply interest formula
        }
    }
}