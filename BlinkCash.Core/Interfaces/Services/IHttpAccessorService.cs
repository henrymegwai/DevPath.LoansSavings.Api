using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IHttpAccessorService
    {
        string GetCurrentUserSubject();
        string CurrentUserName();
        //Task<long[]> CurrentPartnerDetailsCp();
        Task<UserDto> CurrentUserInfo();
        string GetRequestIP();
        string GetToken();
    }
}
