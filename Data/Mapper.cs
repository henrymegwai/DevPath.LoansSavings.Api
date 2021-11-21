
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Utilities;
using BlinkCash.Data.Entities;
using System;

namespace BlinkCash.Data
{
    public static class Mapper
    {
        public static PlanDto Map(this Plan model)
        {
            if (model == null)
                return null;

            return new PlanDto
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedDate = model.ModifiedDate,
                ModifiedBy = model.ModifiedBy,
                DailyInterest = model.DailyInterest,
                MaturityDate = model.MaturityDate,
                DateForDebit = model.DateForDebit,
                DebitFrequency = model.DebitFrequency,
                FrequentAmountSaved = model.FrequentAmountSaved,
                InitialAmountSaved = model.InitialAmountSaved,
                InterestRate = model.InterestRate,
                IsNonInterest = model.IsNonInterest,
                Name = model.Name,
                PaystackTransactionRef = model.PaystackTransactionRef,
                SavingsType = model.SavingsType,
                TargetAmount = model.TargetAmount,
                Tenor = model.Tenor,
                TotalAmountSaved = model.TotalAmountSaved,
                TotalInterestAccrued = model.TotalInterestAccrued,
                IsDeleted = model.IsDeleted,
                RecordStatus = model.RecordStatus,
                PlanId = model.PlanId,
                PaymentType = model.PaymentType,
                DateCreated = model.CreatedDate.ToString(),
                PlanStatus = model.PlanStatus
            };
        }
        public static Plan Map(this PlanDto model)
        {
            if (model == null)
                return null;

            return new Plan
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedDate = model.ModifiedDate,
                ModifiedBy = model.ModifiedBy,
                DailyInterest = model.DailyInterest,
                MaturityDate = model.MaturityDate,
                DateForDebit = model.DateForDebit,
                DebitFrequency = model.DebitFrequency,
                FrequentAmountSaved = model.FrequentAmountSaved,
                InitialAmountSaved = model.InitialAmountSaved,
                InterestRate = model.InterestRate,
                IsNonInterest = model.IsNonInterest,
                Name = model.Name,
                PaystackTransactionRef = model.PaystackTransactionRef,
                SavingsType = model.SavingsType,
                TargetAmount = model.TargetAmount,
                Tenor = model.Tenor,
                TotalAmountSaved = model.TotalAmountSaved,
                TotalInterestAccrued = model.TotalInterestAccrued,
                IsDeleted = model.IsDeleted,
                UserId = model.UserId,
                RecordStatus = model.RecordStatus,
                PlanId = model.PlanId,
                PaymentType = model.PaymentType,
                PlanStatus = model.PlanStatus
            };

        }


        public static PlanHistory MapPlanHistory(this PlanDto model)
        {
            if (model == null)
                return null;

            return new PlanHistory
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedDate = model.ModifiedDate,
                ModifiedBy = model.ModifiedBy,
                DailyInterest = model.DailyInterest,
                MaturityDate = model.MaturityDate,
                DateForDebit = model.DateForDebit,
                DebitFrequency = model.DebitFrequency,
                FrequentAmountSaved = model.FrequentAmountSaved,
                InitialAmountSaved = model.InitialAmountSaved,
                InterestRate = model.InterestRate,
                IsNonInterest = model.IsNonInterest,
                Name = model.Name,
                PaystackTransactionRef = model.PaystackTransactionRef,
                SavingsType = model.SavingsType,
                TargetAmount = model.TargetAmount,
                Tenor = model.Tenor,
                TotalAmountSaved = model.TotalAmountSaved,
                TotalInterestAccrued = model.TotalInterestAccrued,
                IsDeleted = model.IsDeleted,
                RecordStatus = model.RecordStatus,
                PlanId = model.PlanId,
                PaymentType = model.PaymentType
            };
        }

        public static PlanHistoryDto MapPlanHistory(this PlanHistory model)
        {
            if (model == null)
                return null;

            return new PlanHistoryDto
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedDate = model.ModifiedDate,
                ModifiedBy = model.ModifiedBy,
                DailyInterest = model.DailyInterest,
                MaturityDate = model.MaturityDate,
                DateForDebit = model.DateForDebit,
                DebitFrequency = model.DebitFrequency,
                FrequentAmountSaved = model.FrequentAmountSaved,
                InitialAmountSaved = model.InitialAmountSaved,
                InterestRate = model.InterestRate,
                IsNonInterest = model.IsNonInterest,
                Name = model.Name,
                PaystackTransactionRef = model.PaystackTransactionRef,
                SavingsType = model.SavingsType,
                TargetAmount = model.TargetAmount,
                Tenor = model.Tenor,
                TotalAmountSaved = model.TotalAmountSaved,
                TotalInterestAccrued = model.TotalInterestAccrued,
                IsDeleted = model.IsDeleted,
                RecordStatus = model.RecordStatus,
                PlanId = model.PlanId,
                PaymentType = model.PaymentType
            };
        }

        public static BankDto Map(this Bank model)
        {
            if (model == null)
                return null;

            return new BankDto
            {
                Code = model.Code,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                Name = model.Name,
                RecordStatus = model.RecordStatus

            };
        }

        public static Bank Map(this BankDto model)
        {
            if (model == null)
                return null;

            return new Bank
            {
                Code = model.Code,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                Name = model.Name,
                RecordStatus = model.RecordStatus
            };
        }

        public static UserBankDto Map(this UserBank model)
        {
            if (model == null)
                return null;

            return new UserBankDto
            {
                AccountName = model.AccountName,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                AccountNumber = model.AccountNumber,
                RecordStatus = model.RecordStatus,
                BankId = model.BankId,
                UserId = model.UserId

            };
        }

        public static UserBank Map(this UserBankDto model)
        {
            if (model == null)
                return null;

            return new UserBank
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                AccountNumber = model.AccountNumber,
                RecordStatus = model.RecordStatus,
                BankId = model.BankId,
                UserId = model.UserId
            };
        }

        public static CardRequestDto Map(this CardRequest model)
        {
            if (model == null)
                return null;

            return new CardRequestDto
            {

                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus,
                Address = model.Address,
                Address2 = model.Address2,
                CardType = model.CardType,
                City = model.City,
                DeliveryStatus = model.DeliveryStatus,
                Landmark = model.Landmark,
                LGA = model.LGA,
                PaymentReference = model.PaymentReference,
                PaymentType = model.PaymentType,
                State = model.State,
                UserId = model.UserId

            };
        }

        public static CardRequest Map(this CardRequestDto model)
        {
            if (model == null)
                return null;

            return new CardRequest
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus,
                Address = model.Address,
                Address2 = model.Address2,
                CardType = model.CardType,
                City = model.City,
                DeliveryStatus = model.DeliveryStatus,
                Landmark = model.Landmark,
                LGA = model.LGA,
                PaymentReference = model.PaymentReference,
                PaymentType = model.PaymentType,
                State = model.State,
                UserId = model.UserId

            };
        }


        public static WithDrawalSettingDto Map(this WithDrawalSetting model)
        {
            if (model == null)
                return null;

            return new WithDrawalSettingDto
            {

                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus,
                BankId = model.BankId,
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber,
                Bank = model.Bank != null ? model.Bank.Map() : new BankDto { }

            };
        }

        public static WithDrawalSetting Map(this WithDrawalSettingDto model)
        {
            if (model == null)
                return null;

            return new WithDrawalSetting
            {

                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus,
                BankId = model.BankId,
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber
            };
        }
        public static StandingOrderDto Map(this StandingOrder model)
        {
            if (model == null)
                return null;

            return new StandingOrderDto
            {
                Amount = model.Amount,
                ReferenceId = model.ReferenceId,
                StandOrderType = model.StandOrderType,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus

            };
        }

        public static StandingOrder Map(this StandingOrderDto model)
        {
            if (model == null)
                return null;

            return new StandingOrder
            {
                Amount = model.Amount,
                ReferenceId = model.ReferenceId,
                StandOrderType = model.StandOrderType,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus
            };
        }


        public static SavingsConfigurationDto Map(this SavingsConfiguration model)
        {
            if (model == null)
                return null;

            return new SavingsConfigurationDto
            {
                Id = model.Id,
                Config = model.Config

            };
        }

        public static SavingsConfiguration Map(this SavingsConfigurationDto model)
        {
            if (model == null)
                return null;

            return new SavingsConfiguration
            {

                Id = model.Id,
                Config = model.Config
            };
        }

        public static LoanConfigurationDto Map(this LoanConfiguration model)
        {
            if (model == null)
                return null;

            return new LoanConfigurationDto
            {
                Id = model.Id,
                Config = model.Config

            };
        }

        public static LoanConfiguration Map(this LoanConfigurationDto model)
        {
            if (model == null)
                return null;

            return new LoanConfiguration
            {

                Id = model.Id,
                Config = model.Config
            };
        }
        public static Transaction Map(this TransactionDto model)
        {
            if (model == null)
                return null;

            return new Transaction
            {
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber,
                BankCode = model.BankCode,
                BankName = model.BankName,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                GatewayErrorValidation = model.GatewayErrorValidation,
                GatewayReference = model.GatewayReference,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                PayerName = model.PayerName,
                WalletId = model.WalletId,
                Purpose = model.Purpose,
                RecordStatus = model.RecordStatus,
                TransactionDate = model.TransactionDate.HasValue ? model.TransactionDate.Value : default(DateTime),
                TransactionFlowStaging = (TransactionFlowStaging)model.TransactionFlowStaging,
                TransactionFlowStagingDescription = model.TransactionFlowStagingDescription,
                TransactionReference = model.TransactionReference,
                TransactionStatus = (int)model.TransactionStatus,
                TransactionStatusDescription = model.TransactionStatusDescription,
                Route = model.Route,
                Amount = model.Amount,
                DailyInterest = model.DailyInterest,
                IsDeleted = model.IsDeleted,
                PlanId = model.PlanId
            };
        }
        public static TransactionDto Map(this Transaction model)
        {
            if (model == null)
                return null;
            return new TransactionDto
            {
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber,
                BankCode = model.BankCode,
                BankName = model.BankName,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                GatewayErrorValidation = model.GatewayErrorValidation,
                GatewayReference = model.GatewayReference,
                Id = model.Id,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                PayerName = model.PayerName,
                WalletId = model.WalletId,
                Purpose = model.Purpose,
                RecordStatus = model.RecordStatus,
                TransactionDate = model.TransactionDate.HasValue ? model.TransactionDate.Value : default(DateTime),
                TransactionFlowStaging = model.TransactionFlowStaging,
                TransactionFlowStagingDescription = model.TransactionFlowStagingDescription,
                TransactionReference = model.TransactionReference,
                TransactionStatus = (int)model.TransactionStatus,
                TransactionStatusDescription = model.TransactionStatusDescription,
                Amount = model.Amount,
                DailyInterest = model.DailyInterest,
                IsDeleted = model.IsDeleted,
                Route = model.Route,
                PlanId = model.PlanId
            };
        }
        public static NotificationDto Map(this Notification entity)
        {
            if (entity == null)
                return null;

            return new NotificationDto
            {
                AccountName = entity.AccountName,
                Amount = entity.Amount,
                Channel = entity.Channel,
                Naration = entity.Naration,
                NotificationType = entity.NotificationType,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                IsDeleted = entity.IsDeleted,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                RecordStatus = entity.RecordStatus
            };
        }

        public static Notification Map(this NotificationDto entity)
        {
            if (entity == null)
                return null;

            return new Notification
            {
                AccountName = entity.AccountName,
                Amount = entity.Amount,
                Channel = entity.Channel,
                Naration = entity.Naration,
                NotificationType = entity.NotificationType,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                IsDeleted = entity.IsDeleted,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                RecordStatus = entity.RecordStatus
            };
        }
        public static WalletBalanceDto Map(this WalletBalance entity)
        {
            if (entity == null)
                return null;

            return new WalletBalanceDto
            {
                BalanceType = entity.BalanceType,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                IsDeleted = entity.IsDeleted,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Balance = entity.Balance,
                RecordStatus = entity.RecordStatus,
                WalletId = entity.WalletId
            };
        }

        public static WalletBalance Map(this WalletBalanceDto entity)
        {
            if (entity == null)
                return null;

            return new WalletBalance
            {
                BalanceType = entity.BalanceType,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                IsDeleted = entity.IsDeleted,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Balance = entity.Balance,
                RecordStatus = entity.RecordStatus,
                WalletId = entity.WalletId
            };
        }

        public static WalletDto Map(this Wallet entity)
        {
            if (entity == null)
                return null;

            return new WalletDto
            {
                Balance = entity.Balance,
                CurrencyId = entity.CurrencyId,
                DailyLimit = entity.DailyLimit,
                Id = entity.Id,
                IsActive = entity.IsActive,
                UserId = entity.UserId,
                TransactionThreshhold = entity.TransactionThreshhold,
                WalletType = entity.WalletType,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                RecordStatus = entity.RecordStatus,
                BookBalance = entity.BookBalance,
                PlanId = entity.PlanId,
                WalletNumber = entity.WalletNumber
            };
        }
        public static CurrencyDto Map(this Currency x)
        {
            if (x == null)
                return null;

            return new CurrencyDto
            {
                CurrencyCode = x.CurrencyCode,
                Id = x.Id,
                Name = x.Name,
                NumericCode = x.NumericCode
            };
        }
    }
}
