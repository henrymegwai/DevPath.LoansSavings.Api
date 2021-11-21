using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface ITransactionManager
    {
        Task<ExecutionResponse<TransactionResult>> GetByPlan(string PlanId, int Page, int PageSize);
        Task<ExecutionResponse<SavingsConfigurationDto>> GetSavingsConfiguration();
        Task<ExecutionResponse<SavingsConfigurationDto>> CreateSavingsConfiguration(SavingsConfigurationDto model);
        Task<ExecutionResponse<SavingsConfigurationDto>> UpdateSavingsConfiguration(SavingsConfigurationDto model);
        Task<ExecutionResponse<LoanConfigurationDto>> CreateLoanConfiguration(LoanConfigurationDto model);
        Task<ExecutionResponse<LoanConfigurationDto>> UpdateLoanConfiguration(LoanConfigurationDto model);
        Task<ExecutionResponse<LoanConfigurationDto>> GetLoanConfiguration();
    }
}
