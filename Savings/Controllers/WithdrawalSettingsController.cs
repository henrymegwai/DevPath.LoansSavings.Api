using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Savings.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Savings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalSettingsController : ControllerBase
    {
        private readonly IWithdrawalSettingManager _withdrawalSettingMgr;

        public WithdrawalSettingsController(IWithdrawalSettingManager withdrawalSettingMgr)
        {
            _withdrawalSettingMgr = withdrawalSettingMgr;
        }
         
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] WithDrawalSettingUpdateRequest model, long id)
        {
            try
            {
                model.Validate();

                if (id <= default(long))
                    throw new Exception("Id is required");

                var payoutOption = await _withdrawalSettingMgr.GetWithDrawalSettingById(id);
                if (payoutOption.Data == null)
                    throw new Exception("Withdrawal Setting does not exist");

                var result = await _withdrawalSettingMgr.UpdateWithDrawalSetting(new WithDrawalSettingDto
                {
                   AccountNumber = model.AccountNumber, AccountName = model.AccountName, BankId =  model.BankId
                }, id);

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                response.Status = false;
                response.Data = null;
                return BadRequest(response);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WithDrawalSettingRequest model)
        {
            try
            {
                model.Validate(); 
                var payoutOption = await _withdrawalSettingMgr.GetWithDrawalSettingByBank(model.BankId);
                if (payoutOption.Data != null)
                    throw new Exception("Withdrawal Setting for selected bank already exist");


                var result = await _withdrawalSettingMgr.CreateWithDrawalSetting(new WithDrawalSettingDto { BankId = model.BankId, AccountNumber = model.AccountNumber, AccountName = model.AccountName});

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                response.Status = false;
                response.Data = null;
                return BadRequest(response);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= default(long))
                    throw new Exception("Id is required");
                var result = await _withdrawalSettingMgr.DeleteWithDrawalSetting(id);
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
