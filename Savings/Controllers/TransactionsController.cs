using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlinkCash.Savings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : BaseController
    {
        private readonly ITransactionManager _transactionManager;
        public TransactionsController(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }
         
        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> Get(string planId, int page = 1, int pageSize = 20)
        {


            try
            {

                var result = await _transactionManager.GetByPlan(planId, page, pageSize);
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
