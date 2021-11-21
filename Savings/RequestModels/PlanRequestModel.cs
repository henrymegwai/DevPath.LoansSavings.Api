using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Savings.RequestModels
{
    public class PlanRequestModel : Model
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal TargetAmount { get; set; }
        public decimal InitialAmountSaved { get; set; }
        public decimal FrequentAmountSaved { get; set; }
        public decimal TotalAmountSaved { get; set; }
        public decimal TotalInterestAccrued { get; set; }
        [Required]
        public int Tenor { get; set; }
        public DateTimeOffset MaturityDate { get; set; }
        //public decimal DailyInterest { get; set; }
        //public decimal InterestRate { get; set; }
        public string PaystackTransactionRef { get; set; }
        public int DateForDebit { get; set; }
        [Required]
        public SavingType SavingsType { get; set; }
        public PaymentType PaymentType { get; set; }
        public int DebitFrequency { get; set; }
        public bool IsNonInterest { get; set; }
    }

    public class PlanPatchRequest : Model
    {
        [Required]
        public decimal TargetAmount { get; set; }
        [Required]
        public decimal FrequentAmountSaved { get; set; }
        [Required]
        public int DebitFrequency { get; set; }
    }
   
}

 