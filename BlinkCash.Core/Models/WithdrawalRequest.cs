using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class WithdrawalRequest : Model
    {
       [Required]
        public string PlanId { get; set; }
        [Required]
        public WithdrawalChoice WithdrawalChoice { get; set; }
        public int BankId { get; set; }
        public decimal Amount { get; set; }
        public string MaturityDate { get; set; }
        public int Tenor { get; set; }
        public double DailyInterest { get; set; }
        public double InterestRate { get; set; }
        public string DateForDebit { get; set; }
        public SavingType SavingType { get; set; }
        public int DebitFrequency { get; set; }
        public bool IsNonInterest { get; set; }
    }

    public class PlanWithdrawalResponse
    {
        public string PlanId { get; set; }

    }

    /*
     "data: {
         planid: string (if it is  a rollover) or null
      }"
     */
}
