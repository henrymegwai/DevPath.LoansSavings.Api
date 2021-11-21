using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<ExecutionResponse<WithdrawalResponse>> InterBankTransfer(InterBankTransferRequest model);
        Task<ExecutionResponse<AccountDto>> GetNuban(); 
        Task<ExecutionResponse<NubanBalanceResponse>> GetNubanBalance();
        Task<ExecutionResponse<string>> UpdateAccountWithHasRequestedCard(string AccountId, bool HasRequestedCard);
    }
}
