using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IPlanRepository
    {
        Task<PlanDto> CreatePlan(PlanDto model);
        Task<PlanDto> UpdatePlan(PlanDto model, long id);
        Task<bool> DeletePlan(long Id);
        Task<PlanDto> GetPlan(long Id);
        Task<int> Count();
        Task<List<PlanDto>> GetAllPlanWithInterest();
        Task<PlanDto[]> GetPlans(PlanSearchVm planSearchDto, int Page, int PageSize); 
        Task<PlanDto> GetPlanByPlanId(string planId);
        Task<PlanDto> GetPlanName(string planName, string userId);
        Task<PlanDto> UpdatePlanTotalAmountSaved(PlanDto model, long id);
        Task<PlanDto> UpdateDailyInterest(decimal todayInterest, long id);
        Task<PlanHistoryDto> CreatePlanHistory(PlanDto model);
        Task<PlanDto> UpdatePlanStatus(PlanDto model, long id);
    }
}
