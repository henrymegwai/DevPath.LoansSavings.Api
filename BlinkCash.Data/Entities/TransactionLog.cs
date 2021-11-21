using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Entities
{
    public class TransactionLog : BaseEntity
    {
        public DateTime ResponseTime { get; set; }
        public string TagName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string TransactionReference { get; set; } 
    }
}
