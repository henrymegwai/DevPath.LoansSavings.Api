using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class WithDrawalSetting: BaseEntity
    {
        public long BankId { get; set; }
        public string UserId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public Bank Bank { get; set; }
    }
}
