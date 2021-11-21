using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<WalletDto> Get(long id);
        Task<WalletDto> GetByUserId(string userId);
        Task<WalletDto> GetByUserId(string userId, string planId);
        Task<WalletDto[]> Get();
        Task<WalletDto[]> Get(string userId);
        Task<WalletDto> Update(WalletDto model); 
    }
}
