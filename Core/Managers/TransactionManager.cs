using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Managers
{
    public class TransactionManager: ITransactionManager
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IResponseService _responseService;

        public TransactionManager(
           ITransactionRepository transactionRepository, IResponseService responseService)
        {
            _transactionRepository = transactionRepository;
            _responseService = responseService;
        }

       

        public async Task<ExecutionResponse<TransactionResult>> GetByPlan(string PlanId, int Page, int PageSize)
        {
            var sql = @"select e.Id,e.TransactionReference,e.GatewayReference,e.TransactionStatus,
                            e.TransactionStatusDescription,e.TransactionDate,e.Purpose,e.PayerName,e.BankCode,
                            e.AccountNumber,e.AccountName,e.CreatedDate as CreatedDate, e.BankName, e.Amount, e.DailyInterest, e.Route, e.PlanId  
                           from Transactions e ";
            if (!string.IsNullOrEmpty(PlanId)) 
            {
                sql = sql + " where e.PlanId = '" + PlanId + "'";
            }
            int skip = (Page - 1) * PageSize;
            sql = sql + " order by e.CreatedDate desc offset  " + skip + "  rows fetch next " + PageSize + " rows only";

            var transactions = _transactionRepository.DapperSqlWithParams<TransactionDto>(sql, null);
            var tranxs = transactions.ToArray();
            int totalCount = await _transactionRepository.Count();
            var result = new TransactionResult { Transactions = tranxs, Pages = Page, PageSize = PageSize, Total = totalCount };
            return _responseService.ExecutionResponse<TransactionResult>("Successfully retrieved Transactions requests", result, true);
        }

        public async Task<ExecutionResponse<SavingsConfigurationDto>> GetSavingsConfiguration()
        {
            
            var bank = await _transactionRepository.GetSavingsConfiguration();
            if (bank == null)
                return _responseService.ExecutionResponse<SavingsConfigurationDto>("Savings Configuration does not exist", null, false);

            return _responseService.ExecutionResponse<SavingsConfigurationDto>("Successfully retrieved Savings Configuration", bank, true);
        }

        public async Task<ExecutionResponse<SavingsConfigurationDto>> UpdateSavingsConfiguration(SavingsConfigurationDto model)
        {
            if (model == null)
                return _responseService.ExecutionResponse<SavingsConfigurationDto>("invalid request", null, false);
           
            var targetBank = await _transactionRepository.GetSavingsConfiguration();
            if (targetBank == null)
                return _responseService.ExecutionResponse<SavingsConfigurationDto>("Savings Configuration not exist", null, false); 
            var result = await _transactionRepository.UpdateSavingsConfiguration(model);
            return _responseService.ExecutionResponse<SavingsConfigurationDto>("Successfully updated Savings Configuration", null, true);
        }
        

        public async Task<ExecutionResponse<SavingsConfigurationDto>> CreateSavingsConfiguration(SavingsConfigurationDto model)
        {
            if (model == null)
                return _responseService.ExecutionResponse<SavingsConfigurationDto>("invalid request", null, false);

            var result = await _transactionRepository.CreateSavingsConfiguration(model);
            return _responseService.ExecutionResponse<SavingsConfigurationDto>("Successfully created Savings Configuration", result, true);
        }

        public async Task<ExecutionResponse<LoanConfigurationDto>> UpdateLoanConfiguration(LoanConfigurationDto model)
        {
            if (model == null)
                return _responseService.ExecutionResponse<LoanConfigurationDto>("invalid request", null, false);
           
            var targetBank = await _transactionRepository.GetLoanConfiguration();
            if (targetBank == null)
                return _responseService.ExecutionResponse<LoanConfigurationDto>("Loan Configuration not exist", null, false); 
            var result = await _transactionRepository.UpdateLoanConfiguration(model);
            return _responseService.ExecutionResponse<LoanConfigurationDto>("Successfully updated Loan Configuration", null, true);
        }

        public async Task<ExecutionResponse<LoanConfigurationDto>> CreateLoanConfiguration(LoanConfigurationDto model)
        {
            if (model == null)
                return _responseService.ExecutionResponse<LoanConfigurationDto>("invalid request", null, false);

            var result = await _transactionRepository.CreateLoanConfiguration(model);
            return _responseService.ExecutionResponse<LoanConfigurationDto>("Successfully created Loan Configuration", result, true);
        }

        public async Task<ExecutionResponse<LoanConfigurationDto>> GetLoanConfiguration()
        {

            var bank = await _transactionRepository.GetLoanConfiguration();
            if (bank == null)
                return _responseService.ExecutionResponse<LoanConfigurationDto>("Loan Configuration does not exist", null, false);

            return _responseService.ExecutionResponse<LoanConfigurationDto>("Successfully retrieved Loan Configuration", bank, true);
        }

    }
}
