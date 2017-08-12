using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MScBank.Models
{
    interface ICardHolder
    {
        BankCard BankCard { get; set; }

        bool HasCard();

        void CancelCard();

        void OrderCard();

    }
}
