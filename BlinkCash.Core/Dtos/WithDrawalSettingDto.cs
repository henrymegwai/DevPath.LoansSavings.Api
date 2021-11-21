using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class WithDrawalSettingDto : BaseDto
    {
        public long BankId { get; set; }
        public string UserId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public BankDto Bank { get; set; }
    }
}
