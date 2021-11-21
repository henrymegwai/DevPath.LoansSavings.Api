using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Managers
{
    public class PlanManager : IPlanManager
    {private readonly IAccountService _accountService; 
        private readonly IBankManager _bankManager; 
        private readonly IWithdrawalSettingManager _withdrawalSettingManager;
        private readonly IPlanRepository _planRepository; 
        private readonly IPayStackService _paystackService;
        private readonly IResponseService _responseService;
        private readonly IUtilityService _utilityService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IStandingOrderRepository _standingOrderRepository;
        public PlanManager(IPlanRepository planRepository, 
            IResponseService responseService, 
            IUtilityService utilityService,
            ITransactionRepository transactionRepository, 
            IStandingOrderRepository standingOrderRepository,
            IPayStackService paystackService, 
            IAccountService accountService,
            IWithdrawalSettingManager withdrawalSettingManager, IBankManager bankManager
             )
        {
            _planRepository = planRepository;
            _responseService = responseService;
            _utilityService = utilityService;
            _transactionRepository = transactionRepository;
            _standingOrderRepository = standingOrderRepository;
            _paystackService = paystackService;
            _accountService = accountService;
            _withdrawalSettingManager = withdrawalSettingManager;
            _bankManager = bankManager;
        }
        public async Task<ExecutionResponse<string>> CreatePlan(PlanDto model)
        {
            if(model.TargetAmount <= default(long))
            {
                return _responseService.ExecutionResponse<string>("Target Amount is required", null, false);
            }
            if (model.Tenor <= default(int))
            {
                return _responseService.ExecutionResponse<string>("Tenor is required", null, false);
            }
            //Verify Paystack Reference
            if (model.PaymentType == PaymentType.Card && string.IsNullOrEmpty(model.PaystackTransactionRef))
            {
                return _responseService.ExecutionResponse<string>("Paystack Transaction Reference is required", null, false);
            }
            if (model.PaymentType == PaymentType.Card && !string.IsNullOrEmpty(model.PaystackTransactionRef))
            {
                var paystackResponse = await _paystackService.VerifyTransaction(model.PaystackTransactionRef);
                if (paystackResponse.status != true && paystackResponse.message != "Verification successful")
                    return _responseService.ExecutionResponse<string>("Paystack Transaction Reference is invalid", null, false);
            }
            model.CreatedBy = _utilityService.UserName();
            model.UserId = _utilityService.UserId();
            model.PlanId = Guid.NewGuid().ToString();
            var result = await _planRepository.CreatePlan(model);
            if (result != null)
            {
                var plan = await TopUpPlan(result.PlanId, model.TargetAmount, model.PaymentType, model.PaystackTransactionRef);
                //create Wallet for this plan
                 
                var createTransaction = await _transactionRepository.Create(new TransactionDto { PlanId = model.PlanId, Amount = model.InitialAmountSaved, PayerName = model.CreatedBy, Purpose = model.Name, TransactionDate = DateTime.Now, TransactionStatus = (int)TransactionStatus.Successful, TransactionStatusDescription = TransactionStatus.Successful.ToString() });
                return _responseService.ExecutionResponse<string>($"Plan {model.Name} has been created successfully", result.PlanId, true);
            }
            return _responseService.ExecutionResponse<string>("Request failed. Please try again", null, false);
           
        }

        public async Task<ExecutionResponse<string>> DeletePlan(long planId)
        {
            if (planId <= default(long))
                return _responseService.ExecutionResponse<string>("Plan is invalid", null, false);

            var payoutBank = await _planRepository.GetPlan(planId);
            if (payoutBank == null)
                throw new Exception("Plan does not exist");

            var result = await _planRepository.DeletePlan(planId);
            if (!result)
                return _responseService.ExecutionResponse<string>("Request failed. Please try again", null, false);

            return _responseService.ExecutionResponse<string>("Plan has been deleted successfully", null, true);
        }

        public async Task<ExecutionResponse<PlanDto>> GetPlan(long planId)
        {
            if (planId <= default(long))
                return _responseService.ExecutionResponse<PlanDto>("Plan is invalid", null, false);
            ;
            var plan = await _planRepository.GetPlan(planId);
            if (plan == null)
                throw new Exception("Plan does not exist");

            return _responseService
                .ExecutionResponse<PlanDto>("Successful", plan, true);
        }

        public async Task<ExecutionResponse<PlanDto>> GetPlanByName(string planName)
        {
            if (string.IsNullOrEmpty(planName))
                return _responseService.ExecutionResponse<PlanDto>("Plan name is required", null, false);

            string userId = _utilityService.UserId();
            var plan = await _planRepository.GetPlanName(planName, userId);
            if (plan == null)
                throw new Exception("Plan does not exist");

            return _responseService
                .ExecutionResponse<PlanDto>("Successful", plan, true);
        }

        public async Task<ExecutionResponse<PlanDto>> GetPlanByPlanId(string planId)
        {
            if (string.IsNullOrEmpty(planId))
                return _responseService.ExecutionResponse<PlanDto>("PlanId is required", null, false); 
             
            var plan = await _planRepository.GetPlanByPlanId(planId);
            if (plan == null)
                throw new Exception("Plan does not exist");

            return _responseService
                .ExecutionResponse<PlanDto>("Successful", plan, true);
        }

        public async Task<ExecutionResponse<PlanSearchResult>> GetPlans(PlanSearchVm model, int Page, int PageSize)
        {
            string userId = _utilityService.UserId();
            var plans = await _planRepository.GetPlans(model, Page, PageSize);
            int totalplansCount = await _planRepository.Count();
            var result = new PlanSearchResult { Plans = plans, Pages = Page, PageSize = PageSize, Total = totalplansCount };
            return _responseService
                .ExecutionResponse<PlanSearchResult>("Successful", result, true);

        }

        public async Task<ExecutionResponse<PlanDto>> TopUpPlan(string planId, decimal Amount, PaymentType PaymentType, string PaystackTransactionRef)
        {
            if (string.IsNullOrEmpty(planId))
                return _responseService.ExecutionResponse<PlanDto>("Plan Id is required", null, false);

            var userName = _utilityService.UserName();
            //Retrieve Plan with PlanId
            var plan = await _planRepository.GetPlanByPlanId(planId);
            if (plan == null)
                return _responseService.ExecutionResponse<PlanDto>("You have no exiting plan.", null, false);
            if(PaymentType == PaymentType.StandingOrder) 
            {
                //Update Plan Status to On hold
                plan.PlanStatus = PlanStatus.Onhold;
                await _planRepository.UpdatePlanStatus(plan, plan.Id);

                //Create Standing Order
                var standOrd = new StandingOrderDto { Amount = Amount, CreatedBy = "", StandOrderType = StandOrderType.Plan, RecordStatus = RecordStatus.Active, ReferenceId = planId, ExpiryTime = DateTimeOffset.Now.AddMinutes(60) };
                var standingOrder = await _standingOrderRepository.CreateStandingOrder(standOrd);
                var createTransaction = await _transactionRepository.Create(new TransactionDto { CreatedDate = DateTime.Now, PlanId = plan.PlanId, Amount = Amount, TransactionDate = DateTime.Now, Purpose = "", Route = Route.BankTransfer, CreatedBy = userName, GatewayReference = PaystackTransactionRef, TransactionStatus = (int)TransactionStatus.Initiated , TransactionReference = Extensions.GenerateTnxReference(Guid.NewGuid(), Constants.TransactionRefCode) });
            }
            if (PaymentType == PaymentType.Card)
            {
                if(string.IsNullOrEmpty(PaystackTransactionRef))
                    return _responseService.ExecutionResponse<PlanDto>("Paystack Transaction Reference is required for Card Payment Type.", null, false);

                //verify paymentreference and update plan, create transaction and Credit Plan Wallet
                var paystackResponse = await _paystackService.VerifyTransaction(PaystackTransactionRef);
                if (paystackResponse.status == true && paystackResponse.message == "Verification successful") 
                {
                    plan.TotalAmountSaved += Amount;
                    await _planRepository.UpdatePlanTotalAmountSaved(plan, plan.Id);
                    var createTransaction = await _transactionRepository.Create(new TransactionDto {CreatedDate = DateTime.Now, PlanId = plan.PlanId, Amount = Amount, TransactionDate = DateTime.Now, Purpose = "Create Savings Plan", Route = Route.Paystack, CreatedBy = userName, GatewayReference = PaystackTransactionRef, TransactionStatus = (int)TransactionStatus.Initiated, TransactionReference = Extensions.GenerateTnxReference(Guid.NewGuid(),Constants.TransactionRefCode) });
                }
                return _responseService.ExecutionResponse<PlanDto>($"TopUp was not successful: {paystackResponse.message}", null, false);
            }
            return _responseService.ExecutionResponse<PlanDto>("TopUp was not successful", null, false);
        }

        public async Task<ExecutionResponse<PlanDto>> UpdatePlan(PlanDto model, long planId)
        {
            if (planId <= default(long))
                return _responseService.ExecutionResponse<PlanDto>("Plan is invalid", null, false);

            var payoutBank = await _planRepository.GetPlan(planId);
            if (payoutBank == null)
                throw new Exception("Plan does not exist");

            model.ModifiedBy = _utilityService.UserName();
            var result = await _planRepository.UpdatePlan(model, planId);

            if (result == null)
                return _responseService.ExecutionResponse<PlanDto>("Request failed. Please try again",
                null, false);

            return _responseService
                .ExecutionResponse<PlanDto>("Successful", result, true);
        }

        public async Task<ExecutionResponse<PlanWithdrawalResponse>> WithDrawFromPlan(WithdrawalRequest model)
        {
            try
            {
                string userId = _utilityService.UserId();
                var plan = await _planRepository.GetPlanByPlanId(model.PlanId);
                if (plan != null && model.WithdrawalChoice == WithdrawalChoice.Wallet)
                {
                    if (!(plan.MaturityDate >= DateTime.Now)) 
                    {
                    
                    }
                     //Debit from Wallet 

                        return _responseService.ExecutionResponse<PlanWithdrawalResponse>("Successful", null, true);
                }
                else if (model.WithdrawalChoice == WithdrawalChoice.RollOver)
                {

                    decimal totalAmountSaved = plan.TotalAmountSaved + plan.TotalInterestAccrued;

                    var newPlan = await _planRepository.CreatePlan(new PlanDto
                    {
                        Name = plan.Name,
                        SavingsType = plan.SavingsType,
                        TargetAmount = plan.TargetAmount,
                        Tenor = plan.Tenor,
                        MaturityDate = plan.MaturityDate,
                        DateForDebit = plan.DateForDebit,
                        RecordStatus = Core.Utilities.RecordStatus.Active,
                        PaymentType = PaymentType.RollOver,
                        IsNonInterest = plan.IsNonInterest,
                        UserId = plan.UserId,
                        InitialAmountSaved = totalAmountSaved,
                        DailyInterest = plan.DailyInterest,
                        PlanStatus = PlanStatus.Active
                    });
                    if(newPlan != null) 
                    {
                        var createPlanHistory = await _planRepository.CreatePlanHistory(plan);
                        bool deleteOldPlan = await _planRepository.DeletePlan(plan.Id);
                        
                    }
                    return _responseService.ExecutionResponse<PlanWithdrawalResponse>("Successful", null, true);
                }
                else if (model.WithdrawalChoice == WithdrawalChoice.ExternalBank)
                {
                    //Not yet Maturity Date
                    if(plan.MaturityDate >= DateTime.Now)
                    {
                        var nuban = await _accountService.GetNuban();
                        if(nuban.Data == null)
                            return _responseService.ExecutionResponse<PlanWithdrawalResponse>("No Account set up for your plan", null, false);

                        var withdrawalSettings = await _withdrawalSettingManager.GetWithDrawalSettingByUserId(userId);
                        var bankCode = await _bankManager.GetBank(withdrawalSettings.Data.BankId);
                        var transfer = await _accountService.InterBankTransfer(new InterBankTransferRequest
                        {
                            Amount = model.Amount,
                            FromAccountNumber = nuban.Data.AccountId,
                            Narration = "Interbank Transfer",
                            Payer = withdrawalSettings.Data.AccountNumber,
                            ReceiverBankCode = bankCode.Data.Code,
                            ToAccountNumber = withdrawalSettings.Data.AccountNumber
                        });
                        if (transfer.Data.error) 
                        {
                            plan.TotalAmountSaved = (plan.TotalAmountSaved - model.Amount);
                            await UpdatePlan(plan, plan.Id);
                        }
                    }
                    else 
                    {
                        
                    }
                    return _responseService.ExecutionResponse<PlanWithdrawalResponse>("Successful", null, true);
                }
                else
                {
                    return _responseService.ExecutionResponse<PlanWithdrawalResponse>("Successful", null, true);
                }
            } 
            catch(Exception ex)
            {
                return _responseService
                   .ExecutionResponse<PlanWithdrawalResponse>("Successful", null, true);
            }
        }
    }
}   
