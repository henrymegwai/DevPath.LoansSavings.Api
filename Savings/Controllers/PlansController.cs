using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models;
using BlinkCash.Savings.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlinkCash.Savings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : BaseController
    {
        private readonly IPlanManager _planManager;

        public PlansController(IPlanManager planManager)
        {
            _planManager = planManager;

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PlanRequestModel model)
        {
            try
            {
                model.Validate(); 

                var result = await _planManager.CreatePlan(new PlanDto
                {
                    Name = model.Name,
                    SavingsType = model.SavingsType,
                    TargetAmount = model.TargetAmount,
                    Tenor = model.Tenor,
                    MaturityDate = model.MaturityDate,
                    DateForDebit = model.DateForDebit,
                    PaystackTransactionRef = model.PaystackTransactionRef,
                    RecordStatus = Core.Utilities.RecordStatus.Active,  PaymentType = model.PaymentType
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


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] PlanPatchRequest model, long id)
        {
            try
            {
                model.Validate();

                if (id <= default(long))
                    throw new Exception("Plan Id is required");

                var payoutOption = await _planManager.GetPlan(id);
                if (payoutOption.Data == null)
                    throw new Exception("Plan does not exist");

                var result = await _planManager.UpdatePlan(new PlanDto
                { 
                    TargetAmount = model.TargetAmount,
                    FrequentAmountSaved = model.FrequentAmountSaved,
                    DebitFrequency = model.DebitFrequency
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= default(long))
                    throw new Exception("PlanId is required");
                var result = await _planManager.DeletePlan(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{planId}")]
        public async Task<IActionResult> GetByPlanId(string planId)
        {
            try
            {
                if (string.IsNullOrEmpty(planId))
                    throw new Exception("planId is required");

                var result = await _planManager.GetPlanByPlanId(planId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("{page}/{pageSize}")]
        public async Task<IActionResult> GetPlans(PlanSearchVm model, int page = 1, int pageSize = 20)
        {
            try
            {
                var result = await _planManager.GetPlans(model, page, pageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("{id}/withdraw")]
        public async Task<IActionResult> WithDrawal(WithdrawalRequest model)
        {
            try
            {
                var result = await _planManager.WithDrawFromPlan(model);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("topup")]
        public async Task<IActionResult> TopUpPlan(TopUpPlanRequest model)
        {
            try
            {
                var result = await _planManager.TopUpPlan(model.PlanId, model.Amount, model.PaymentType, model.PaystackTransactionRef);
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
    }
}
