using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class PlanHistory: BaseEntity
    {
        public string PlanId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal InitialAmountSaved { get; set; }
        public decimal FrequentAmountSaved { get; set; } 
        public decimal TotalAmountSaved { get; set; }
        public decimal TotalInterestAccrued { get; set; }
        public int Tenor { get; set; }
        public DateTimeOffset MaturityDate { get; set; }
        public decimal DailyInterest { get; set; }
        public decimal InterestRate { get; set; }
        public string PaystackTransactionRef { get; set; }
        public int DateForDebit { get; set; }
        public SavingType SavingsType { get; set; }
        public PaymentType PaymentType { get; set; }
        public int DebitFrequency { get; set; } 
        public bool IsNonInterest { get; set; }
       
    }
}
