using BlinkCash.Core.Dtos;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface ICardRequestRepository
    {
        Task<CardRequestDto> AddCardRequest(CardRequestDto model);
        List<T> DapperSqlWithParams<T>(string sql, string connectionnName = null);
        List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null);
        Task<bool> DeleteCardRequest(long Id, string UserId); 
        Task<CardRequestDto> GetCardRequestById(long Id, string userId);
        Task<CardRequestDto> GetCardRequestByCardType(CardType cardType, string userId);
        Task<CardRequestDto[]> GetCardRequests(string UserId);
        Task<CardRequestDto> UpdateCardRequest(CardRequestDto model, long id, string userId);
        Task<int> Count();
    }
}
