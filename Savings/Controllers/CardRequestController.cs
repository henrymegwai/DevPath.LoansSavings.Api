using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlinkCash.Savings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardRequestController : ControllerBase
    {
        private readonly ICardRequestManager _cardRequestManager;

        public CardRequestController(ICardRequestManager cardRequestManager)
        {
            _cardRequestManager = cardRequestManager;
        }


        [HttpPost()]
        public async Task<IActionResult> AddCardRequest([FromBody] CardRequestModel model)
        {
            try
            {
                model.Validate();
                var payoutOption = await _cardRequestManager.GetCardRequestByCardType(model.CardType);
                if (payoutOption.Data != null)
                    return BadRequest("Card Request already added for this user");

                var result = await _cardRequestManager.AddCardRequest(new CardRequestDto { Address = model.Address, Address2 = model.Address2, CardType = model.CardType, Landmark = model.Landmark, LGA = model.LGA, PaymentReference = model.PaymentReference, City = model.City, PaymentType = model.PaymentType, State = model.State});
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


        [HttpPatch("{id}/update")]
        public async Task<IActionResult> UpdateCardRequest([FromBody] UpdateCardRequestRequest model, long id)
        {
            try
            {
                model.Validate();

                if (id <= default(long))
                    return BadRequest("Card Request Id is required");

                var payoutOption = await _cardRequestManager.GetCardRequestById(id);
                if (payoutOption.Data == null)
                    return BadRequest("Card Request does not exist");

                var result = await _cardRequestManager.UpdateCardRequest( 
                   new CardRequestDto { Address = model.Address, Address2 = model.Address2, CardType = model.CardType, Landmark = model.Landmark, LGA = model.LGA, PaymentReference = model.PaymentReference, City = model.City, PaymentType = model.PaymentType, State = model.State
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardRequestById(long id)
        {
            try
            {
                if (id <= default(long))
                    return BadRequest("id is required");

                var result = await _cardRequestManager.GetCardRequestById(id);
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


        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetCardRequests(int page = 1, int pageSize = 20)
        {
            try
            {
               
                var result = await _cardRequestManager.GetCardRequests(page, pageSize);
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
