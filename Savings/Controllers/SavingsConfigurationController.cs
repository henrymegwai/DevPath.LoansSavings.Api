using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class SavingsConfigurationController : ControllerBase
    {
        private readonly ITransactionManager _transactionManager;

        public SavingsConfigurationController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var bank = await _transactionManager.GetSavingsConfiguration();
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SavingsConfiguration([FromBody] SavingsConfigurationRequest model)
        {
            try
            {
                model.Validate();


                var savingCon = await _transactionManager.GetSavingsConfiguration();
                if (savingCon.Status) 
                {
                    ExecutionResponse<SavingsConfigurationDto> response = new ExecutionResponse<SavingsConfigurationDto>();
                    response.Message = "Savings Configuration already exist";
                    response.Data = savingCon.Data;
                    return BadRequest(response);
                    
                }
                var result = await _transactionManager.CreateSavingsConfiguration(new SavingsConfigurationDto { Config = model.ConfigSettings });

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

        [AllowAnonymous]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] SavingsConfigurationRequest model, int id)
        {
            try
            {
                model.Validate();

                if (id <= default(long))
                    return BadRequest(" Id is required");

                var payoutOption = await _transactionManager.GetSavingsConfiguration();
                if (!payoutOption.Status)
                    return BadRequest(payoutOption);

                var result = await _transactionManager.UpdateSavingsConfiguration(new SavingsConfigurationDto
                {
                    Id = id,
                    Config = model.ConfigSettings
                });

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
