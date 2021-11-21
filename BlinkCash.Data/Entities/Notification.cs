using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class Notification : BaseEntity
    {
        public decimal Amount { get; set; }

        public NotificationType NotificationType { get; set; }

        public string Naration { get; set; }
        public string  AccountName { get; set; }
        public string Channel { get; set; }
    }
}
