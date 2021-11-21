using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class WithdrawalAccount:BaseEntity
    {
        public string UserId { get; set; }
        public string AccountNumber { get; set; }
        public long BankId { get; set; }
    }
}
