using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface IWithdrawalSettingManager
    {
        Task<ExecutionResponse<WithDrawalSettingDto>> CreateWithDrawalSetting(WithDrawalSettingDto witdrawalSettingsDto);
        Task<ExecutionResponse<string>> DeleteWithDrawalSetting(long Id);
        Task<ExecutionResponse<WithDrawalSettingDto>> GetWithDrawalSettingById(long id);
        Task<ExecutionResponse<WithDrawalSettingDto>> GetWithDrawalSettingByBank(long bankId);
        Task<ExecutionResponse<WithDrawalSettingDto>> UpdateWithDrawalSetting(WithDrawalSettingDto withDrawalSettingDto, long id);
        Task<ExecutionResponse<WithDrawalSettingDto>> GetWithDrawalSettingByUserId(string userId);
    }
}
