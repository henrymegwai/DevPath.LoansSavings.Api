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
    public class Wallet : BaseEntity
    {
        public string UserId { get; set; }   
        public string Nuban { get; set; }
        public int CurrencyCode { get; set; }

        [ConcurrencyCheck]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Balance { get; set; }

        [ConcurrencyCheck]
        [Column(TypeName = "decimal(18,4)")]
        public decimal BookBalance { get; set; } 

        [Column(TypeName = "decimal(18,4)")]
        public decimal DailyLimit { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TransactionThreshhold { get; set; }
        public bool IsActive { get; set; }
        public string PlanId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public long CurrencyId { get; internal set; }
        public Currency Currency { get; set; }
        public WalletType WalletType { get; internal set; }
        public string WalletNumber { get; internal set; }
    }
}
