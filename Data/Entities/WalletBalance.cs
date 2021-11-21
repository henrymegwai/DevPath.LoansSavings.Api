using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class WalletBalance : BaseEntity
    {
        [ConcurrencyCheck]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Balance { get; set; }
        public BalanceType BalanceType { get; set; }

        public long WalletId { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
