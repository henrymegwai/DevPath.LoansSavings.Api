using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface IPlanManager
    {
        Task<ExecutionResponse<string>> CreatePlan(PlanDto  model);
        Task<ExecutionResponse<PlanDto>> UpdatePlan(PlanDto model, long planId);
        Task<ExecutionResponse<string>> DeletePlan(long planId);
        Task<ExecutionResponse<PlanDto>> GetPlan(long Id); 
        Task<ExecutionResponse<PlanDto>> GetPlanByPlanId(string planId);
        Task<ExecutionResponse<PlanDto>> TopUpPlan(string PlanId, decimal Amount, PaymentType PaymentType, string PaystackTransactionRef);
        Task<ExecutionResponse<PlanDto>> GetPlanByName(string planName);
        Task<ExecutionResponse<PlanSearchResult>> GetPlans(PlanSearchVm model, int Page, int PageSize);
        Task<ExecutionResponse<PlanWithdrawalResponse>> WithDrawFromPlan(WithdrawalRequest model);
    }
}
