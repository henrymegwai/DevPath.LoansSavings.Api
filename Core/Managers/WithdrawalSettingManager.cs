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
    public class WithdrawalSettingManager : IWithdrawalSettingManager
    {
        private readonly IWithDrawalSettingRepository _withDrawalSettingRepository;
        private readonly IUtilityService _utilityService;
        private readonly IResponseService _responseService;
        public WithdrawalSettingManager(IWithDrawalSettingRepository withDrawalSettingRepository,
            IResponseService responseService,IUtilityService utilityService)
        {
             
            _responseService = responseService;
            _utilityService = utilityService;
            _withDrawalSettingRepository = withDrawalSettingRepository;
        }


        public async Task<ExecutionResponse<WithDrawalSettingDto>> CreateWithDrawalSetting(WithDrawalSettingDto witdrawalSettingsDto)
        {
            string UserId = _utilityService.UserId();
            if (witdrawalSettingsDto == null)
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("invalid request");

            if (string.IsNullOrWhiteSpace(witdrawalSettingsDto.AccountNumber)
                || witdrawalSettingsDto.BankId < default(long))
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("Account Number, Bank is required");

            var sql = @"SELECT AccountNumber from WithDrawalSetting where BankId = @bankId 
                            UNION
                            SELECT AccountNumber from WithDrawalSetting where UserId = @userId";

            var withdrawalcheck = _withDrawalSettingRepository.DapperSqlWithParams<WithDrawalSettingDto>(sql, new { bankId = witdrawalSettingsDto.BankId, userId = witdrawalSettingsDto.UserId }, null);

            if (withdrawalcheck.Count > 0) 
            {
                WithDrawalSettingDto withdrwl = withdrawalcheck.FirstOrDefault();
                var withdrawal = await _withDrawalSettingRepository.GetWithDrawalSettingByBankId(UserId, withdrwl.BankId);

                await UpdateWithDrawalSetting(withdrawal, withdrawal.Id);
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("Successfully created Withdrawal Setting", withdrawal, true);
            }
            witdrawalSettingsDto.UserId = UserId;
            var result = await _withDrawalSettingRepository.CreateWithDrawalSetting(witdrawalSettingsDto);
            return _responseService.ExecutionResponse<WithDrawalSettingDto>("Successfully created Withdrawal Setting", result, true);

        }

        public async Task<ExecutionResponse<WithDrawalSettingDto>> UpdateWithDrawalSetting(WithDrawalSettingDto withDrawalSettingDto, long id)
        {
            string UserId = _utilityService.UserId();
            if (withDrawalSettingDto == null)
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("invalid request");

            if (string.IsNullOrWhiteSpace(withDrawalSettingDto.AccountNumber)
               || withDrawalSettingDto.BankId < default(long))
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("Account Number, Bank is required");

            var targetBank = await _withDrawalSettingRepository.GetWithDrawalSettingByBankId(UserId, withDrawalSettingDto.BankId);
            if (targetBank == null)
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("Withdrawal Setting does not exist", targetBank, false);
            withDrawalSettingDto.ModifiedBy = _utilityService.UserName();
            var result = await _withDrawalSettingRepository.UpdateWithDrawalSetting(withDrawalSettingDto, id);
            return _responseService.ExecutionResponse<WithDrawalSettingDto>("successfully updated Withdrawal Setting", result, true);

        }

        public async Task<ExecutionResponse<WithDrawalSettingDto>> GetWithDrawalSettingById(long id)
        {
            string UserId = _utilityService.UserId();
            var bank = await _withDrawalSettingRepository.GetWithDrawalSettingById(UserId,id);
            if (bank == null)
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("Withdrawal Setting does not exist");

            return _responseService.ExecutionResponse<WithDrawalSettingDto>("successfully retrieved Withdrawal Setting", bank, true);
        }

        public async Task<ExecutionResponse<WithDrawalSettingDto>> GetWithDrawalSettingByBank(long bankId)
        {
            string UserId = _utilityService.UserId();
            var bank = await _withDrawalSettingRepository.GetWithDrawalSettingByBankId(UserId, bankId);
            if (bank == null)
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("Withdrawal Setting does not exist");

            return _responseService.ExecutionResponse<WithDrawalSettingDto>("successfully retrieved Withdrawal Setting", bank, true);
        }

        public async Task<ExecutionResponse<WithDrawalSettingDto>> GetWithDrawalSettingByUserId(string userId)
        {
             
            var bank = await _withDrawalSettingRepository.GetWithDrawalSettingByUserId(userId);
            if (bank == null)
                return _responseService.ExecutionResponse<WithDrawalSettingDto>("Withdrawal Setting does not exist");

            return _responseService.ExecutionResponse<WithDrawalSettingDto>("successfully retrieved Withdrawal Setting", bank, true);
        }

        public async Task<ExecutionResponse<string>> DeleteWithDrawalSetting(long Id)
        {
            var bank = await _withDrawalSettingRepository.DeleteWithDrawalSetting(Id);
            if (!bank)
                return _responseService.ExecutionResponse<string>("Withdrawal Setting was not deleted successfully", null, false);

            return _responseService.ExecutionResponse<string>("Withdrawal Setting was not deleted successfully", null, false);
        }
    }
}
