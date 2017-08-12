using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class Mortgage : Loan
    {
        public string SecurityAddress { get; set; }
    }
}