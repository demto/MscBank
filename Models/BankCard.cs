using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class BankCard
    {
        
        public int Id { get; set; }

        public string CardNumber { get; set; }

        [Required]
        [RegularExpression("\\d{4}", ErrorMessage = "PIN number must be numeric and 4 digits!")]
        public int PinNumber { get; set; }

        [RegularExpression("\\d{4}", ErrorMessage = "PIN number must be numeric and 4 digits!")]
        public int? OldNum { get; set; }

        public DateTime ExpiryDate { get; set; }

        public CardStatus Status { get; set; }

        public string NameOnCard { get; set; }

        public virtual BankAccountBase ParentAccount { get; set; }
        
        public int BankAccountBaseId { get; set; }

        public enum CardStatus
        {
            Lost,
            Open,
            Blocked
        }
        
    }
}