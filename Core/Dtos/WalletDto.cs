using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class WalletDto: BaseDto
    {

        public WalletType WalletType { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public long CurrencyId { get; set; }
        public string PlanId { get; set; }
        public decimal TransactionThreshhold { get; set; }
        public decimal DailyLimit { get; set; }
        public bool IsActive { get; set; }
        public string WalletNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal BookBalance { get; set; }
        public CurrencyDto Currency { get; set; }
    }
}
