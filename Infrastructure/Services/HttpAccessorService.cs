using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SwervePay.Infrastructure.Services
{
    public class HttpAccessorService : IHttpAccessorService
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public HttpAccessorService(IHttpContextAccessor httpContextAccessor)
        {
        
            _httpAccessor = httpContextAccessor;
        }

        public Task<UserDto> CurrentUserInfo()
        {
            throw new NotImplementedException();
        }

        public string CurrentUserName()
        {
            try
            {
                var f = _httpAccessor.HttpContext.User.Claims;

                var email = _httpAccessor.HttpContext.User.Claims
                       .FirstOrDefault(x => x.Type == ClaimTypes.Email);
                if (email == null)
                    return null;

                return email.Value;
            }
            catch (Exception ex)
            {
                //Log.Info(ex);
                return GetCurrentUserSubject();
            }
        }
        public string GetCurrentUserSubject()
        {
            try
            {
                var sub = _httpAccessor.HttpContext.User.Claims
                       .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (sub != null)
                {
                    return sub.Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return null;
            }
        }
        //public async Task<long[]> CurrentPartnerDetailsCp()
        //{
        //    string ssoId = GetCurrentUserSubject();
        //    if (string.IsNullOrEmpty(ssoId))
        //        return Array.Empty<long>();

        //    var user = await _orgService.GetUserBySSOID(ssoId);
        //    if (!user.Status)
        //        return Array.Empty<long>();

        //    if (!user.Data.CorporateId.HasValue)
        //        return Array.Empty<long>();

        //    var partnerDetails = _partnerService.GetPartnerDetail(user.Data.CorporateId.Value);

        //    if (!partnerDetails.Status)
        //        return Array.Empty<long>();

        //    if (partnerDetails.Data.CorporateType == CorporateType.CP.ToString())
        //    {
        //        return new long[] { partnerDetails.Data.PartnerId };
        //    }
        //    else
        //    {
        //        return (await _counterPartyRepository
        //            .GetPartnerByLiquidityProviderId(partnerDetails.Data.LiquidityProviderId)).Select(s => s.Id).ToArray();
        //    }
        //}
        //public async Task<UserDto> CurrentUserCorporateType()
        //{
        //    string ssoId = GetCurrentUserSubject();
        //    if (string.IsNullOrEmpty(ssoId))
        //        return null;

        //    var user = await _orgService.GetUserBySSOID(ssoId);
        //    if (!user.Status)
        //        return null;

        //    if (!user.Data.CorporateId.HasValue)
        //        return null;

        //    var partnerDetails = _partnerService.GetPartnerDetail(user.Data.CorporateId.Value);
        //    if (partnerDetails.Status)
        //        return partnerDetails.Data;
        //    return null;
        //}
        public string GetRequestIP()
        {
            String ip = "127.0.0.1";
            try
            {

                ip = _httpAccessor.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(ip))
                    ip = _httpAccessor.HttpContext.Request.Headers["REMOTE_ADDR"];

                return ip == "::1" ? "127.0.0.1" : ip;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return ip;
            }
        }
        public string GetToken()
        {
            try
            {

                if (_httpAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    var res = _httpAccessor.HttpContext.Request.Headers["Authorization"];
                    if (!string.IsNullOrEmpty(res))
                        return res;
                }

                return null;
            }
            catch (Exception ex)
            {
               // Log.Info(ex);
                return null;
            }
        }
    }
}
