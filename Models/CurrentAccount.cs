using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MScBank.Models
{
    public class CurrentAccount : BankAccountBase, ICardHolder
    {
        public BankCard BankCard { get; set; }

        public void CancelCard() {

            using(var _context = new ApplicationDbContext()) {
                //_context.BankCards.Remove(this.BankCard);
            }
        }

        public bool HasCard() {
            //using(var _context = new ApplicationDbContext()) {
            //    return _context.BankCards.Single( c => c == null ? false : true);
            //}
            return true;
        }

        public void OrderCard() {
            throw new NotImplementedException();
        }
    }
}