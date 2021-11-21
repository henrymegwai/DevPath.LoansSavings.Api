using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class StandingOrder :BaseEntity
    {
        public string ReferenceId { get; set; }
        public StandOrderType StandOrderType { get; set; }
        public decimal Amount { get; set; }
    }
}
