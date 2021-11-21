using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IUserBankRepository
    {
        Task<UserBankDto> AddUserBank(UserBankDto model);
        Task<bool> DeleteUserBank(long Id, string UserId);
        Task<UserBankDto> GetUserBank(long Id, string UserId);
        Task<UserBankDto[]> GetUserBanks(string UserId);
        Task<UserBankDto> UpdateUserBank(UserBankDto model, long id, string UserId);
        List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null);
        List<T> DapperSqlWithParams<T>(string sql, string connectionnName = null);
        Task<UserBankDto> GetUserBankByBankId(long bankId, string userId);
    }
}
