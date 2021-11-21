using System; 
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions; 
using BlinkCash.Core.Dtos; 
using BlinkCash.Core.Interfaces.Services; 
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Managers;

namespace BlinkCash.Core.Managers
{
    public class BankManager : IBankManager
    {
        private readonly IBankRepository _bankRepository; 
        private readonly IUserBankRepository _userbankRepository;
        private readonly IUtilityService _utilityService;
        private readonly IResponseService _responseService;
        private readonly IPayStackService _paystackService;
        public BankManager(IResponseService responseService, 
            IBankRepository bankRepository,
            IUtilityService utilityService,
            IUserBankRepository userbankRepository, IPayStackService paystackService)
         { 
        
            _bankRepository = bankRepository;
            _responseService = responseService;
            _utilityService = utilityService;
            _paystackService = paystackService;
            _userbankRepository = userbankRepository;
         }

        public async Task<ExecutionResponse<BankDto>> CreateBank(BankDto bankDto)
        {
            if (bankDto == null)
                return _responseService.ExecutionResponse<BankDto>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(bankDto.Name)
                || string.IsNullOrWhiteSpace(bankDto.Code))
                return _responseService.ExecutionResponse<BankDto>("bank name , code must be supplied", null, false);

            var sql = @"SELECT Name from Banks where Code = @code and IsDeleted = 0
                            UNION
                            SELECT Name from Banks where Name = @name and IsDeleted = 0";

            var bankCheck = _bankRepository.DapperSqlWithParams<BankDto>(sql, new { code = bankDto.Code, name = bankDto.Name }, null).Any();

            if (bankCheck)
                return _responseService.ExecutionResponse<BankDto>("bank already exist", null, false);

            var result = await _bankRepository.CreateBank(bankDto);             
            return _responseService.ExecutionResponse<BankDto>("successfully created bank", result, true);

        }

        public async Task<ExecutionResponse<BankDto>> UpdateBank(BankDto bankDto, long id)
        {
            if (bankDto == null)
                return _responseService.ExecutionResponse<BankDto>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(bankDto.Name)
               || string.IsNullOrWhiteSpace(bankDto.Code))
                return _responseService.ExecutionResponse<BankDto>("bank name, code is required", null, false);

            var targetBank = await _bankRepository.GetBank(id);
            if (targetBank == null)
                return _responseService.ExecutionResponse<BankDto>("bank does not exist", null, false);
            bankDto.ModifiedBy =  _utilityService.UserName();
            var result = await _bankRepository.UpdateBank(bankDto,id);
            return _responseService.ExecutionResponse<BankDto>("successfully updated bank", null, true);

        }

        public async Task<ExecutionResponse<BankDto>> GetBank(long id)
        {
            var bank = await _bankRepository.GetBank(id);
            if (bank == null)
                return _responseService.ExecutionResponse<BankDto>("bank does not exist", null, false);

            return _responseService.ExecutionResponse<BankDto>("Successfully retrieved bank", bank, true);
        }

        public async Task<ExecutionResponse<Banks[]>> GetBanks()
        {
            var banks = await _bankRepository.GetBanks();

            var banklist = banks.Select(b => new Banks {  Id = b.Id, Name = b.Name, Code = b.Code}).OrderBy(x => x.Name).ToArray();
            return _responseService.ExecutionResponse<Banks[]>("successfully retrieved banks", banklist, true);
        }


        public async Task<ExecutionResponse<BankResponse[]>> GetBanksFromPaystack()
        {
            var banks = await _paystackService.GetBanks(); 
            if (banks.status != true)
                return _responseService.ExecutionResponse<BankResponse[]>("Paystack Transaction Reference is invalid", null, false);
            var banklist = banks.Select(b => new Banks { Id = b.Id, Name = b.Name, Code = b.Code }).OrderBy(x => x.Name).ToArray();
            return _responseService.ExecutionResponse<BankResponse[]>("successfully retrieved banks", banklist, true);
        }


        public async Task<ExecutionResponse<string>> DeleteBank(long Id)
        {
            var bank = await _bankRepository.DeleteBank(Id);
            if (!bank)
                return _responseService.ExecutionResponse<string>("bank was not deleted successfully", null, false);

            return _responseService.ExecutionResponse<string>("bank was not deleted successfully", null, false);
        }

        public async Task<ExecutionResponse<BankDto>> GetBankByName(string name)
        {
            var bank = await _bankRepository.GetBankByName(name);

            return _responseService.ExecutionResponse<BankDto>("successfully retrieved bank", bank, true);
        }


        public async Task<ExecutionResponse<UserBankDto>> AddUserBank(UserBankDto bankDto)
        {
            if (bankDto == null)
                return _responseService.ExecutionResponse<UserBankDto>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(bankDto.AccountNumber)
                || bankDto.BankId < default(long))
                return _responseService.ExecutionResponse<UserBankDto>("Account Number , Bank Id must be supplied", null, false);
            string UserId = _utilityService.UserId();

            var sql = @"SELECT AccountNumber from UserBank where BankId = @BankId and UserId = @userId";

            var bankCheck = _userbankRepository.DapperSqlWithParams<UserBankDto>(sql, new { BankId = bankDto.BankId, userId = UserId }, null).Any();

            if (bankCheck)
                return _responseService.ExecutionResponse<UserBankDto>("Bank already exist for User",null, false );
            bankDto.UserId = UserId;
            var result = await _userbankRepository.AddUserBank(bankDto);
            return _responseService.ExecutionResponse<UserBankDto>("Successfully added user's bank", result, true);

        }

        public async Task<ExecutionResponse<UserBankDto[]>> GetUserBanks()
        {
            string UserId = _utilityService.UserId();
            var banks = await _userbankRepository.GetUserBanks(UserId); 
            banks = banks.OrderBy(x => x.BankId).ToArray();
            return _responseService.ExecutionResponseList("successfully retrieved user banks", banks, true);
        }
        public async Task<ExecutionResponse<string>> DeleteUserBank(long Id)
        {
            string UserId = _utilityService.UserId();
            var bank = await _userbankRepository.DeleteUserBank(Id, UserId);
            if (!bank)
                return _responseService.ExecutionResponse<string>("user bank was not deleted successfully", null, false);

            return _responseService.ExecutionResponse<string>("user bank was not deleted successfully", null, false);
        }

        public async Task<ExecutionResponse<UserBankDto>> UpdateUserBank(UserBankDto bankDto, long id)
        {
            if (bankDto == null)
                return _responseService.ExecutionResponse<UserBankDto>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(bankDto.AccountNumber)
     || bankDto.BankId < default(long))
                return _responseService.ExecutionResponse<UserBankDto>("Account Number , Bank Id must be supplied", null, false);
            string UserId = _utilityService.UserId();

            var targetBank = await _userbankRepository.GetUserBank(id, UserId);
            if (targetBank == null)
                return _responseService.ExecutionResponse<UserBankDto>("user bank does not exist", null, false);
            bankDto.ModifiedBy = _utilityService.UserName();
            var result = await _userbankRepository.UpdateUserBank(bankDto, id, UserId);
            return _responseService.ExecutionResponse<UserBankDto>("successfully updated user bank", null, true);

        }

        public async Task<ExecutionResponse<UserBankDto>> GetUserBank(long id)
        {

            string UserId = _utilityService.UserId();
            var bank = await _userbankRepository.GetUserBank(id, UserId);
            if (bank == null)
                return _responseService.ExecutionResponse<UserBankDto>("user bank does not exist", null, false);

            return _responseService.ExecutionResponse<UserBankDto>("Successfully retrieved user bank", bank, true);
        }

        public async Task<ExecutionResponse<UserBankDto>> GetUserBankByBankId(long bankId)
        {

            string UserId = _utilityService.UserId();
            var bank = await _userbankRepository.GetUserBankByBankId(bankId, UserId);
            if (bank == null)
                return _responseService.ExecutionResponse<UserBankDto>("user bank does not exist", null, false);

            return _responseService.ExecutionResponse<UserBankDto>("Successfully retrieved user bank", bank, true);
        }
    }
}
