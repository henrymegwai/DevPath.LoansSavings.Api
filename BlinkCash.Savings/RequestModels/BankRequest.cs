using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Savings.RequestModels
{
    public class BankRequest:Model
    {
        [Required, MaxLength(100, ErrorMessage = "Bank Name exceeds limits")]
        public string Name { get; set; }
        [Required, MaxLength(5, ErrorMessage = "Bank code exceeds limits")]
        public string Code { get; set; }
    }
    public class NotificationRequest : Model
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public NotificationType NotificationType { get; set; }
        [Required]
        public string Naration { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string Channel { get; set; }
    }
    public class AddBankRequest : Model
    {
        [Required, MaxLength(10, ErrorMessage = "Account Number cannot be greater than 10 digits"), MinLength(10, ErrorMessage =" Account Number cannot be less than 10 digits")]
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        [Required]
        public long BankId { get; set; }
    }
    public class UpdateUserBankRequest : Model
    {
        [Required, MaxLength(10, ErrorMessage = "Account Number cannot be greater than 10 digits"), MinLength(10, ErrorMessage = " Account Number cannot be less than 10 digits")]
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        [Required]
        public long BankId { get; set; }
        [Required]
        public long Id { get; set; }
    }

    public class WithDrawalSettingRequest : Model
    {
        [Required]
        public long BankId { get; set; }
        [Required, MaxLength(10, ErrorMessage = "AccountNumber must not exceed 10 character"), MinLength(10, ErrorMessage = "AccountNumber must not be less than 10 character")]
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
         
    }

    public class WithDrawalSettingUpdateRequest : Model
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long BankId { get; set; }
        [Required, MaxLength(10, ErrorMessage = "AccountNumber must not exceed 10 character"), MinLength(10, ErrorMessage = "AccountNumber must not be less than 10 character")]
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }

    }
}
