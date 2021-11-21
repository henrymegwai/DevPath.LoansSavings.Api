using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class StandingOrderDto : BaseDto
    {
        public string ReferenceId { get; set; }
        public StandOrderType StandOrderType { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset ExpiryTime { get; set; }
    }
}
