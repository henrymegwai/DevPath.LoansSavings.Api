using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class PlanSearchResult
    {
        public PlanDto[] Plans { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
        public int PageSize { get; set; }
    }

    public class NotificationResult
    {
        public NotificationResponse[] Notifications { get; set; }
        public int Total { get; set; }
        public int Pages { get; set; }
        public int PageSize { get; set; }
    }
}
