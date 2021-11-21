using System;
using System.Threading.Tasks;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models;
using BlinkCash.Savings.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;   

namespace BlinkCash.Savings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationManager _notificationManager;

        public NotificationsController(INotificationManager bankManager)
        {
            _notificationManager = bankManager;
        }

        [AllowAnonymous]
        [HttpGet("{notificationcategory}/{PageSize}/{Page}")]
        public async Task<IActionResult> Get(int notificationcategory, int pageSize = 20, int page = 1)
        {
            try
            {
                var bank = await _notificationManager.GetNotifications(notificationcategory,pageSize,page);
                
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
 

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NotificationRequest model)
        {
            try
            {
                model.Validate();

                var result = await _notificationManager.CreateNotification(new NotificationDto
                {
                    Amount = model.Amount,
                    Channel = model.Channel,
                    AccountName = model.AccountName,
                    Naration = model.Naration,
                    NotificationType = model.NotificationType
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
         

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                if (id <= default(long))
                    return BadRequest("id is required");

                var result = await _notificationManager.GetNotification(id);
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