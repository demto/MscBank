using MScBank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MScBank.ViewModels {
    public class TransferFundsViewModel {

        public BankAccountBase FromAccount { get; set; }

        public int FromAccountId { get; set; }

        [Required]
        [Display(Name ="Account Number")]
        [StringLength (8)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Account number must be numeric!")]
        public string ToAccountAcNum { get; set; }

        [Required]
        [Display(Name ="Sort Code")]
        [StringLength(8)]
        [RegularExpression(@"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}\b", ErrorMessage = "Please make sure you enter a six digit sort code separated by dashes (-)")]
        public string ToAccountSC { get; set; }

        public decimal Amount { get; set; }
    }
}