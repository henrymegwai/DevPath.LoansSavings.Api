using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IWithDrawalSettingRepository
    {
        Task<WithDrawalSettingDto> CreateWithDrawalSetting(WithDrawalSettingDto model);
        List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null);
        Task<bool> DeleteWithDrawalSetting(long Id);
        Task<WithDrawalSettingDto> GetWithDrawalSetting(long Id);
        Task<WithDrawalSettingDto> GetWithDrawalSettingById(string UserId, long Id);
        Task<WithDrawalSettingDto> GetWithDrawalSettingByBankId(string UserId, long bankId);
        Task<WithDrawalSettingDto> UpdateWithDrawalSetting(WithDrawalSettingDto model, long id);
        Task<WithDrawalSettingDto> GetWithDrawalSettingByUserId(string UserId); 
    }
}
