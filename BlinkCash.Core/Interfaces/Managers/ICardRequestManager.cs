using BlinkCash.Core.Dtos;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface ICardRequestManager
    {
        Task<ExecutionResponse<CardRequestDto>> AddCardRequest(CardRequestDto cardRequestDto);
        Task<ExecutionResponse<string>> DeleteCardRequest(long Id); 
        Task<ExecutionResponse<CardRequestDto>> GetCardRequestById(long Id);
        Task<ExecutionResponse<CardRequestDto>> GetCardRequestByCardType(CardType cardType);
        Task<ExecutionResponse<CardRequestResult>> GetCardRequests(int Page, int PageSize);
        Task<ExecutionResponse<CardRequestDto>> UpdateCardRequest(CardRequestDto cardRequestDto, long id);
    }
}
