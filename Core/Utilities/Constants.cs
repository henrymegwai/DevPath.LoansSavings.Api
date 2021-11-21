using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Utilities
{
    
        public static class Constants
        {
            public const string TransactionRefCode = "BLC";        
 
            public static T ParseEnum<T>(string value)
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
        }
}
