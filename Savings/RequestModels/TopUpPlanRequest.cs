using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Savings.RequestModels
{
    public class TopUpPlanRequest : Model
    {
        [Required]
        public string PlanId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        public string PaystackTransactionRef { get; set; }
    }
}
