using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class BankCard
    {

        public int CardId { get; set; }

        public string CardNumber { get; set; }

        public int PinNumber { get; set; }

        public DateTime ExpiryDate { get; set; }

        public CardStatus status { get; set; }
        
        public virtual CurrentAccount parentAccount { get; set; }

        public int CurrentAccountId { get; set; }

        public enum CardStatus
        {
            Lost,
            Open,
            Blocked
        }
    }
}