using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models {
    public class Transfer : Transaction {
        
        public BankAccountBase ToAccount { get; set; }

        public int ToAccountId { get; set; }
    }
}