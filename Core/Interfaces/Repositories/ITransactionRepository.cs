using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<TransactionDto> Create(TransactionDto model);
        List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null);
        Task<TransactionDto> Get(string reference);
        //Task<TransactionDto> GetByPlan(string PlanId);
        Task<TransactionDto> Update(TransactionDto model);
        Task<int> Count();
        Task<SavingsConfigurationDto> GetSavingsConfiguration();
        Task<SavingsConfigurationDto> CreateSavingsConfiguration(SavingsConfigurationDto model);
        Task<SavingsConfigurationDto> UpdateSavingsConfiguration(SavingsConfigurationDto model);   
        Task<LoanConfigurationDto> CreateLoanConfiguration(LoanConfigurationDto model);
        Task<LoanConfigurationDto> UpdateLoanConfiguration(LoanConfigurationDto model); 
        Task<LoanConfigurationDto> GetLoanConfiguration();
    }
}
