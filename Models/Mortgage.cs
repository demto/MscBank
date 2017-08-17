using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class Mortgage : Loan
    {
        [Required]
        public string SecurityAddress { get; set; }
    }
}