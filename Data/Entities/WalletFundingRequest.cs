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
    public class WalletFundingRequest : BaseEntity
    {

        [Column(TypeName = "decimal(18,4)")]
        public decimal? WalletBalance { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? WalletOpeningBalance { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? WalletClosingBalance { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public long WalletId { get; set; }
        public string ApproveBy { get; set; }
        public DateTime? DateApproved { get; set; }
       
        public FundingRequestType FundingRequestType { get; set; }
        public string TransactionReference { get; set; }

        public bool IsProcessed { get; set; }
        public string ProcessedBy { get; set; }
        public DateTime? ProcessedDate { get; set; }

        public bool IsNotificationRecieved { get; set; }
        public DateTime? NotificationReceivedDate { get; set; }

        public bool IsCompleted { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletedDate { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Narration { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        public decimal BookBalanceOpening { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal BookBalanceClosing { get; set; }
        public DateTime? BookBalanceUpdatedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }
    }
}
