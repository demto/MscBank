﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    
    public abstract class BankAccountBase
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public string SortCode { get; set; }

        public string AccountNumber { get; set; }
        
        public virtual List<Transaction> Transactions { get; set; }

        public ApplicationUser Owner { get; set; }

        public string ApplicationUserId { get; set; }
        
        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime OpenDate { get; set; }
    }
}