using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlinkCash.Core.Dtos
{
    public class TransactionDto : BaseDto
    {
        public string PlanId { get; set; }
        public string TransactionReference { get; set; }
        public string GatewayReference { get; set; }
        public int TransactionStatus { get; set; }
        public string TransactionStatusDescription { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Purpose { get; set; }
        public string PayerName { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; }
        public decimal DailyInterest { get; set; }
        public string GatewayErrorValidation { get; set; }
        public TransactionFlowStaging TransactionFlowStaging { get; set; }
        public string TransactionFlowStagingDescription { get; set; }
        public Route Route { get; set; }
        public long? WalletId { get; set; }
    }

    public class TransactionResult
    {
        public TransactionDto[] Transactions { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
        public int PageSize { get; set; }
    }

    public class SavingsConfigurationDto
    {
        public int Id { get; set; }
        public string Config { get; set; }
    }
    public class LoanConfigurationDto
    {
        public int Id { get; set; }
        public string Config { get; set; }
    }
}
