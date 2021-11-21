using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Utilities
{
    public enum RecordStatus
    {
        Active = 1,
        Inactive,
        Deleted,
        Archive
    } 
    public enum WalletType
    {
         Plan, TargetSavings, Account
    } 
     public enum CardType
    {
        Master = 1,
        Verve         
    }  
    
    public enum DeliveryStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("PickedUp")]
        PickedUp,
        [Description("OnDispatch")]
        OnDispatch,
        [Description("Delivered")] 
        Delivered,
        [Description("Returned")] Returned         
    }
    public enum SavingType
    {
        Fixed = 1,
        Target,Regular
         
    }
    public enum StandOrderType
    {
        Loan = 1,
        Plan,
    }
    public enum DebitFrequency
    {
        Daily = 1,
        Weekly, Monthly

    }

    public enum NotificationType
    {
        Credit = 1,
        Debit,

    }
    public enum NotificationCategory
    {
        Credit = 1,
        Debit,

    }
    public enum PaymentType
    {
        Card = 1,
        StandingOrder, RollOver, Wallet
         
    }
    public enum BalanceType
    {
        AvailableBalance,
        BookBalance
    }

    public enum TransactionFlowStaging
    {
        [Description("Initiated")]
        Initiated,
        [Description("Retrying")]
        Retrying,
        [Description("Processing Starts")]
        ProcessingStarts, 
        [Description("Completed")]
        Completed,
        [Description("Wallet Processed")]
        WalletProcessed,
    }

    public enum TransactionStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Processing")]
        Processing,
        [Description("Successful")]
        Successful,
        [Description("Failed")]
        Failed,
        [Description("Retrying")]
        Retrying,
        Initiated
    }
    public enum Route
    {
        BankTransfer = 1,
        Paystack = 2,
        DirectTransfer = 3,
        
    }
    public enum WithdrawalChoice
    {
        Wallet, ExternalBank, RollOver
    } 
    public enum IsNonInterest
    {
        
    }
    public enum PlanStatus
    {
        Active, NotActive, Onhold
    }
    public enum PlanStatusCategory
    {
        All, Active, NotActive, Onhold
    }
    public enum FundingRequestType
    {
         
        Credit = 2,
        Debit = 3,
        Reversal = 4
    }
}
