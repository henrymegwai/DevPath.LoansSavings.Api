using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class CardRequestModel : Model
    {
        [Required]
        public CardType CardType { get; set; }
        [Required]
        public string Address { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string LGA { get; set; }
        [Required]
        public string Landmark { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        public string PaymentReference { get; set; }
    }
    public class UpdateCardRequestRequest : Model
    {
        [Required]
        public CardType CardType { get; set; }
        [Required]
        public string Address { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string LGA { get; set; }
        [Required]
        public string Landmark { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        public string PaymentReference { get; set; }
    }
}

 