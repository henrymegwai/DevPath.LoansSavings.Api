using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class NotificationDto:BaseDto
    {
        public decimal Amount { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Naration { get; set; }
        public string AccountName { get; set; }
        public string Channel { get; set; }
    }

    public class NotificationResponse
    {
        public decimal Amount { get; set; }
        public string NotificationType { get; set; }
        public string Naration { get; set; }
        public string AccountName { get; set; }
        public string Channel { get; set; }
        public long Id { get; set; }
        public string CreatedDate { get; set; }
    }
}
