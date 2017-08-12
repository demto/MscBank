using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MScBank.Models
{
    interface IInterestBearing
    {
        double InterestRate { get; set; }

        void AdjustBalanceWithInterest();
    }
}
