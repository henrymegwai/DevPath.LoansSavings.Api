using BlinkCash.Core.Configs;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpService _httpService;
        private readonly IUtilityService _utilityService;
        public AccountService(IOptions<AppSettings> appSettings, IHttpService httpService, IUtilityService utilityService)
        {
            _appSettings = appSettings.Value;
            _httpService = httpService;
            _utilityService = utilityService;
        }

        public async Task<ExecutionResponse<AccountDto>> GetNuban()
        {
            try
            { 
                Uri _url = new Uri(new Uri(_appSettings.NubanUrl), "/api/Account/nuban");
                var token = _utilityService.GetUserToken();
                if(string.IsNullOrEmpty(token))
                    return new ExecutionResponse<AccountDto> { Data = null, Message = "Unauthorized Request", Status = false }; 
                var response = await _httpService.Get(_url.ToString(), new Dictionary<string, string>(), token);
                var s = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ExecutionResponse<AccountDto>>(s);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new ExecutionResponse<AccountDto> { Data = result.Data, Message = result.Message, Status = result.Status };

                return new ExecutionResponse<AccountDto> { Data = result.Data, Message = result.Message, Status = result.Status }; ;
            }
            catch (Exception ex)
            {
                return new ExecutionResponse<AccountDto> { Data = null, Message = "Something went wrong", Status = false }; 
            }
        }

        

        public  async Task<ExecutionResponse<NubanBalanceResponse>> GetNubanBalance()
        {
            try
            {
                Uri _url = new Uri(new Uri(_appSettings.NubanUrl), "/api/Account/nuban");
                var token = _utilityService.GetUserToken();
                if (string.IsNullOrEmpty(token))
                    return new ExecutionResponse<NubanBalanceResponse> { Data = null, Message = "Unauthorized Request", Status = false };
                var response = await _httpService.Get(_url.ToString(), new Dictionary<string, string>(), token);
                var s = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ExecutionResponse<NubanBalanceResponse>>(s);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new ExecutionResponse<NubanBalanceResponse> { Data = result.Data, Message = result.Message, Status = result.Status };

                return new ExecutionResponse<NubanBalanceResponse> { Data = result.Data, Message = result.Message, Status = result.Status }; ;
            }
            catch (Exception ex)
            {
                return new ExecutionResponse<NubanBalanceResponse> { Data = null, Message = "Something went wrong", Status = false };
            }
        }

        public async Task<ExecutionResponse<WithdrawalResponse>> InterBankTransfer(InterBankTransferRequest model)
        {

            try
            {
                var token = _utilityService.GetUserToken();
                if (string.IsNullOrEmpty(token))
                    return new ExecutionResponse<WithdrawalResponse> { Data = null, Message = "Unauthorized Request", Status = false };
                var payload = new
                {
                    amount = model.Amount,
                    payer = model.Payer,
                    fromAccountNumber = model.FromAccountNumber,
                    toAccountNumber = model.ToAccountNumber,
                    receiverAccountType = model.ReceiverAccountType,
                    receiverBankCode = model.ReceiverBankCode,
                    receiverPhoneNumber = model.ReceiverPhoneNumber,
                    narration = model.Narration
                };

                Uri _url = new Uri(new Uri(_appSettings.NubanUrl), "/api/Account/interbank/transfer");
                var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await _httpService.Post(_url.ToString(), stringContent, new Dictionary<string, string> (), string.Empty);
                var s = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ExecutionResponse<WithdrawalResponse>>(s);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new ExecutionResponse<WithdrawalResponse> { Data = result.Data, Message = result.Message, Status = result.Status };

                return new ExecutionResponse<WithdrawalResponse> { Data = result.Data, Message = result.Message, Status = result.Status }; ;
            }
            catch (Exception ex)
            {
                return new ExecutionResponse<WithdrawalResponse> { Data = null, Message = "Something went wrong", Status = false }; ;
            }
        }

        public async Task<ExecutionResponse<string>> UpdateAccountWithHasRequestedCard(string AccountId, bool HasRequestedCard)
        {
            try
            {
                var token = _utilityService.GetUserToken();
                if (string.IsNullOrEmpty(token))
                    return new ExecutionResponse<string> { Data = null, Message = "Unauthorized Request", Status = false };
                var payload = new
                {
                    accountId = AccountId,
                    hasRequestedCard = HasRequestedCard
                };

                Uri _url = new Uri(new Uri(_appSettings.NubanUrl), "/api/Account/update/cardrequest/flag");
                var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await _httpService.Post(_url.ToString(), stringContent, new Dictionary<string, string>(), string.Empty);
                var s = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ExecutionResponse<string>>(s);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new ExecutionResponse<string> { Data = result.Data, Message = result.Message, Status = result.Status };

                return new ExecutionResponse<string> { Data = result.Data, Message = result.Message, Status = result.Status }; ;
            }
            catch (Exception ex)
            {
                return new ExecutionResponse<string> { Data = null, Message = "Something went wrong", Status = false }; ;
            }
        }
    }
}
