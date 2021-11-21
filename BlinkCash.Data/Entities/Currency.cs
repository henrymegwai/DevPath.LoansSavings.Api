using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public partial class Currency : BaseEntity
    {
        public Currency()
        { 
            Transactions = new HashSet<Transaction>();
        }

        public string Name { get; set; }
        public bool Sender { get; set; }
        public bool Reciepient { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Limit { get; set; }
        public decimal MaxAmount { get; set; }
        public string NumericCode { get; set; } 
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
