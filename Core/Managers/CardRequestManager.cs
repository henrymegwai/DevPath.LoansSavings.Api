using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Managers
{
    public class CardRequestManager: ICardRequestManager
    {
        private readonly IAccountService _accountService;
        private readonly ICardRequestRepository _cardRequestRepository; 
        private readonly IUtilityService _utilityService;
        private readonly IResponseService _responseService;

        public CardRequestManager(IResponseService responseService,
            IUtilityService utilityService,
            ICardRequestRepository cardRequestRepository,
            IAccountService accountService)
        {
            _cardRequestRepository = cardRequestRepository;
            _responseService = responseService;
            _utilityService = utilityService;
            _accountService = accountService;
        }

        public async Task<ExecutionResponse<CardRequestDto>> AddCardRequest(CardRequestDto bankDto)
        {
            if (bankDto == null)
                return _responseService.ExecutionResponse<CardRequestDto>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(bankDto.Address)
                || string.IsNullOrWhiteSpace(bankDto.City) || string.IsNullOrWhiteSpace(bankDto.State) || string.IsNullOrWhiteSpace(bankDto.Landmark) || string.IsNullOrWhiteSpace(bankDto.LGA))
                return _responseService.ExecutionResponse<CardRequestDto>("Address, City, State, Landmark or LGA is required ", null, false);
            string UserId = _utilityService.UserId();

            var sql = @"SELECT CardType from CardRequest where CardType = @cardType and UserId = @userId";

            var bankCheck = _cardRequestRepository.DapperSqlWithParams<CardRequestDto>(sql, new { cardType = bankDto.CardType, userId = UserId }, null).Any();

            if (bankCheck)
                return _responseService.ExecutionResponse<CardRequestDto>("Card request already exist for User", null, false);
            bankDto.UserId = UserId;
            var result = await _cardRequestRepository.AddCardRequest(bankDto);
            var nuban = await _accountService.GetNuban();
            if (nuban.Status)
            {
                await _accountService.UpdateAccountWithHasRequestedCard(nuban.Data.AccountId, true);
            }
            return _responseService.ExecutionResponse<CardRequestDto>("Card request was successful", result, true);

        }

        public async Task<ExecutionResponse<CardRequestResult>> GetCardRequests(int Page, int PageSize)
        {
            //string UserId = _utilityService.UserId(); 
            var sql = @"select e.Id,e.CardType as CardType,e.Address as Address,e.Address2 as Address2,
                        e.City as City,e.State as State,e.LGA as LGA,e.LandMark as Landmark,e.DeliveryStatus as DeliveryStatus,
                        e.PaymentType as PaymentType,e.PaymentReference as PaymentReference,e.CreatedDate as CreatedDate, e.UserId as UserId from CardRequest e";
            int skip = (Page - 1) * PageSize;
            sql = sql + " order by CreatedDate desc offset  " + skip + "  rows fetch next " + PageSize + " rows only";

            var cardRequests = _cardRequestRepository.DapperSqlWithParams<CardRequestDto>(sql, null);
            var cardReqsts = cardRequests.ToArray();
            int totalCount = await _cardRequestRepository.Count();
            var result = new CardRequestResult { CardRequests = cardReqsts, Pages = Page, PageSize = PageSize, Total = totalCount };
            return _responseService.ExecutionResponse("Successfully retrieved Card requests", result, true);
        }
        public async Task<ExecutionResponse<string>> DeleteCardRequest(long Id)
        {
            string UserId = _utilityService.UserId();
            var bank = await _cardRequestRepository.DeleteCardRequest(Id, UserId);
            if (!bank)
                return _responseService.ExecutionResponse<string>("Card request was not deleted successfully", null, false);
            var nuban = await _accountService.GetNuban();
            if (nuban.Status)
            {
                await _accountService.UpdateAccountWithHasRequestedCard(nuban.Data.AccountId, false);
            }
            return _responseService.ExecutionResponse<string>("Card request was not deleted successfully", null, false);
        }

        public async Task<ExecutionResponse<CardRequestDto>> UpdateCardRequest(CardRequestDto bankDto, long id)
        {
            if (bankDto == null)
                return _responseService.ExecutionResponse<CardRequestDto>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(bankDto.Address)
                 || string.IsNullOrWhiteSpace(bankDto.City) || string.IsNullOrWhiteSpace(bankDto.State) || string.IsNullOrWhiteSpace(bankDto.Landmark) || string.IsNullOrWhiteSpace(bankDto.LGA))
                return _responseService.ExecutionResponse<CardRequestDto>("Address, City, State, Landmark or LGA is required ", null, false);
            string UserId = _utilityService.UserId();

            var targetBank = await _cardRequestRepository.GetCardRequestById(id, UserId);
            if (targetBank == null)
                return _responseService.ExecutionResponse<CardRequestDto>("Card request does not exist", null, false);
            bankDto.ModifiedBy = _utilityService.UserName();
            var result = await _cardRequestRepository.UpdateCardRequest(bankDto, id, UserId);
            return _responseService.ExecutionResponse<CardRequestDto>("successfully updated Card request", null, true);

        }

        public async Task<ExecutionResponse<CardRequestDto>> GetCardRequestById(long id)
        {

            string UserId = _utilityService.UserId();
            var bank = await _cardRequestRepository.GetCardRequestById(id, UserId);
            if (bank == null)
                return _responseService.ExecutionResponse<CardRequestDto>("Card request does not exist", null, false);

            return _responseService.ExecutionResponse<CardRequestDto>("Successfully retrieved Card request", bank, true);
        }

        public async Task<ExecutionResponse<CardRequestDto>> GetCardRequestByCardType(CardType cardType)
        {

            string UserId = _utilityService.UserId();
            var bank = await _cardRequestRepository.GetCardRequestByCardType(cardType, UserId);
            if (bank == null)
                return _responseService.ExecutionResponse<CardRequestDto>("Card request does not exist", null, false);

            return _responseService.ExecutionResponse<CardRequestDto>("Successfully retrieved Card request", bank, true);
        }
    }
}
