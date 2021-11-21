using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class WalletBalanceDto : BaseDto
    {
        public decimal Balance { get; set; }
        public BalanceType BalanceType { get; set; }

        public long WalletId { get; set; }

        public WalletDto Wallet { get; set; }
    }
}
