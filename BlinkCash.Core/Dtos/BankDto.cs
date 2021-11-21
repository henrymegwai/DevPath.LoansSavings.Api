using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class BankDto : BaseDto
    {
        [Required, MaxLength(100, ErrorMessage = "Bank Name exceeds limits")]
        public string Name { get; set; }
        [Required, MaxLength(5, ErrorMessage = "Bank code exceeds limits")]
        public string Code { get; set; }
        
    }
    public class Banks
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }

   

    public class UserBankDto : BaseDto
    {
        public string AccountNumber { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public long BankId { get; set; }
        [Required]
        public string UserId { get; set; } 
    }


    public class BankData
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string code { get; set; }
        public string longcode { get; set; }
        public object gateway { get; set; }
        public bool pay_with_bank { get; set; }
        public bool active { get; set; }
        public bool is_deleted { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class Meta
    {
        public string next { get; set; }
        public object previous { get; set; }
        public int perPage { get; set; }
    }

    public class BankResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<BankData> data { get; set; }
        public Meta meta { get; set; }
    }
}
