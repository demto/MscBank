using MScBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Utils.BankAccountUtils {

    public class FindObjectType {        

        public string FindType(object obj) {

            if(obj is CurrentAccount) {
                return "Current Account";
            }else if(obj is SavingsAccount){
                return "Savings Account";
            }else if(obj is CreditCard) {
                return "Credit Card";
            }else if(obj is Loan) {
                return "Loan";
            }else if(obj is Mortgage) {
                return "Mortgage";
            } else {
                return "This is not an existing type of account";
            }
        }
    }
}