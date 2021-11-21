using System; 
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlinkCash.Core.Dtos;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface IBankManager
    {
        Task<ExecutionResponse<BankDto>> CreateBank(BankDto bankDto);
        Task<ExecutionResponse<BankDto>> UpdateBank(BankDto bankDto, long id);
        Task<ExecutionResponse<BankDto>> GetBank(long Id);
        Task<ExecutionResponse<Banks[]>> GetBanks(); 
        Task<ExecutionResponse<string>> DeleteBank(long Id);
        Task<ExecutionResponse<BankDto>> GetBankByName(string name);
        Task<ExecutionResponse<UserBankDto>> AddUserBank(UserBankDto bankDto);
        Task<ExecutionResponse<UserBankDto[]>> GetUserBanks();
        Task<ExecutionResponse<UserBankDto>> GetUserBank(long id);
        Task<ExecutionResponse<UserBankDto>> UpdateUserBank(UserBankDto bankDto, long id);
        Task<ExecutionResponse<string>> DeleteUserBank(long Id);
        Task<ExecutionResponse<UserBankDto>> GetUserBankByBankId(long bankId);
    }

 
}
