using System;
using System.Threading.Tasks;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Savings.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;   

namespace BlinkCash.Savings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankManager _bankManager;

        public BanksController(IBankManager bankManager)
        {
            _bankManager = bankManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var bank = await _bankManager.GetBanks();
                if (!bank.Status)
                    return BadRequest(bank);

                return Ok(bank);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] BankRequest model, long id)
        {
            try
            {
                model.Validate();

                if (id <= default(long))
                    return BadRequest("Bank Id is required");

                var payoutOption = await _bankManager.GetBank(id);
                if (payoutOption.Data == null)
                    return BadRequest("Bank does not exist");

                var result = await _bankManager.UpdateBank(new BankDto
                {
                    Name = model.Name, Code = model.Code
                }, id);

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BankRequest model)
        {
            try
            {
                model.Validate();


                var payoutOption = await _bankManager.GetBankByName(model.Name);
                if (payoutOption.Data != null)
                    return BadRequest("Bank already exist");


                var result = await _bankManager.CreateBank(new BankDto { Name = model.Name, Code = model.Code });

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= default(long))
                    return BadRequest("BankId is required");
                var result = await _bankManager.DeleteBank(id); 
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                if (id <= default(long))
                    return BadRequest("id is required");

                var result = await _bankManager.GetBank(id);
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("user/addbank")]
        public async Task<IActionResult> AddUserBank([FromBody] AddBankRequest model)
        {
            try
            {
                model.Validate();


                var payoutOption = await _bankManager.GetUserBankByBankId(model.BankId);
                if (payoutOption.Data != null)
                    return BadRequest("Bank already added for this user");

                var result = await _bankManager.AddUserBank(new UserBankDto { AccountNumber = model.AccountNumber, AccountName = model.AccountName, BankId = model.BankId });
                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPatch("user/{id}/updatebank")]
        public async Task<IActionResult> UpdateUserBank([FromBody] UpdateUserBankRequest model, long id)
        {
            try
            {
                model.Validate();

                if (id <= default(long))
                    return BadRequest("User Bank Id is required");

                var payoutOption = await _bankManager.GetUserBank(id);
                if (payoutOption.Data == null)
                    return BadRequest("User Bank does not exist");

                var result = await _bankManager.UpdateUserBank(new UserBankDto
                {
                    AccountNumber = model.AccountNumber,
                    AccountName = model.AccountName,
                    BankId = model.BankId
                }, id);

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpGet("userbank/{id}")]
        public async Task<IActionResult> GetUserBankById(long id)
        {
            try
            {
                if (id <= default(long))
                    return BadRequest("id is required");

                var result = await _bankManager.GetUserBank(id);
                if(!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpGet("userbanks")]
        public async Task<IActionResult> GetUserBanks(long id)
        {
            try
            {
                if (id <= default(long))
                    return BadRequest("id is required");

                var result = await _bankManager.GetUserBanks();
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}