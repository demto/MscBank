using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Utils.BankAccountUtils {
    public class BankRates {
        
        public enum InterestRate {

            Savings = 1,
            Loan = 10,
            CreditCard = 20,
            Mortgage = 2
        }
    }
}