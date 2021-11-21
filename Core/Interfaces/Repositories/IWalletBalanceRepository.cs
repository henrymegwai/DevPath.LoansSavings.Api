using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IWalletBalanceRepository
    {
        Task<WalletBalanceDto[]> GetByWalletId(long walletId);
        Task<WalletBalanceDto[]> Create(long walletId);
        Task<(bool itExist, decimal availableBalance, decimal bookBalance)> CreateAndGetBalances(long walletId);
        Task<(bool itExist, decimal availableBalance, decimal bookBalance)> GetBalances(long walletId);
    }
}
