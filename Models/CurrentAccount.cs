using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class CurrentAccount : BankAccountBase, ICardHolder
    {
        public BankCard BankCard { get; set; }

        public bool HasCard() => BankCard == null ? false : true;


        public void CancelCard()
        {
            if(BankCard != null)
            {
                BankCard = null;
            }
        }

        public void OrderCard()
        {
            if(BankCard == null)
            {
                BankCard = new BankCard();
            }
            else
            {
                throw new ArgumentException("Only one card is allowed for an account!");
            }
        }
        
    }
}