using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IBankRepository
    {
        Task<BankDto> CreateBank(BankDto model);
        Task<bool> DeleteBank(long Id);
        Task<BankDto> GetBank(long Id);
        Task<BankDto[]> GetBanks();
        Task<BankDto> UpdateBank(BankDto model, long id);
        List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null);
        List<T> DapperSqlWithParams<T>(string sql, string connectionnName = null);
        Task<BankDto> GetBankByName(string name);
    }
}
