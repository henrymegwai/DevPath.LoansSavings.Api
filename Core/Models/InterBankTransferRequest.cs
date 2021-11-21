using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class InterBankTransferRequest
    {
        [Required]
        public decimal Amount { get; set; }
        public string Payer { get; set; }
        [Required]
        public string FromAccountNumber { get; set; }
        [Required]
        public string ToAccountNumber { get; set; }
        public string ReceiverAccountType { get; set; }
        [Required]
        public string ReceiverBankCode { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string Narration { get; set; }
    }


    public class WithdrawalResponse
    {
        public bool error { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
    }

    public class Data
    {
    }


    public class NubanResponse
    {
        public string Nuban { get; set; }
       
    }
    public class NubanBalanceResponse
    {
       public bool Error { get; set; }
        public int Data { get; set; }
        public string Message { get; set; }
        public string Nuban { get; set; }
       
    }
}
