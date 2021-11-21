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
    public class PayStackService : IPayStackService
    {
        private readonly IHttpService _httpService;
        private readonly AppSettings _appSettings;
        public PayStackService(IHttpService httpService, IOptions<AppSettings> options)
        {
            _httpService = httpService;
            _appSettings = options.Value;
        }

       
        public async Task<PaymentResponse> VerifyTransaction(string paymentReference)
        {
            try 
            {
                var response = await _httpService.Get($"{_appSettings.PaystackUrl}/transaction/verify/{paymentReference}", new Dictionary<string, string>(), _appSettings.PaystackKey);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<PaymentResponse>(result);
                    return responseData;
                }
                return new PaymentResponse { status = false, data = null, message = responseContent };
            }
            catch(Exception ex) 
            {
                new Exception(ex.Message);
                return new PaymentResponse { status = false, data = null, message = "Verification Not Successful" };
            }
            
        }

        public async Task<BankResponse> GetBanks()
        {
            try
            {
                var response = await _httpService.Get($"{_appSettings.PaystackUrl}/bank", new Dictionary<string, string>(), _appSettings.PaystackKey);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<BankResponse>(result);
                    return responseData;
                }
                return new BankResponse { status = false, data = null, message = responseContent };
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return new BankResponse { status = false, data = null, message = "Get Bank List was Not Successful" };
            }

        }
    }
}
