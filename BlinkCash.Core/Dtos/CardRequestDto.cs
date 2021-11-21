using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class CardRequestDto: BaseDto
    {
        public string UserId { get; set; }
        public CardType CardType { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string Landmark { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PaymentReference { get; set; }
    }

    public class CardRequestResult
    {
        public CardRequestDto[] CardRequests { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
        public int PageSize { get; set; }
    }
}
