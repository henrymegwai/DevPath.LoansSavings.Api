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
    public class NotificationManager : INotificationManager
    {
        private readonly INotificationRepository _notificationRepository;  
        private readonly IUtilityService _utilityService;
        private readonly IResponseService _responseService;

        public NotificationManager(IResponseService responseService,
            INotificationRepository notificationRepository,
            IUtilityService utilityService
            )
        {
            _notificationRepository = notificationRepository;
            _responseService = responseService;
            _utilityService = utilityService;
        }

        public async Task<ExecutionResponse<NotificationDto>> CreateNotification(NotificationDto notificationDto)
        {
            if (notificationDto == null)
                return _responseService.ExecutionResponse<NotificationDto>("invalid request", null, false);

            var result = await _notificationRepository.CreateNotification(notificationDto);
            return _responseService.ExecutionResponse<NotificationDto>("successfully created notification", result, true);

        }

         

        public async Task<ExecutionResponse<NotificationDto>> GetNotification(long id)
        {
            var notification = await _notificationRepository.GetNotification(id);
            if (notification == null)
                return _responseService.ExecutionResponse<NotificationDto>("notification does not exist", null, false);

            return _responseService.ExecutionResponse<NotificationDto>("Successfully retrieved notification", notification, true);
        }

        public async Task<ExecutionResponse<NotificationResult>> GetNotifications(int NotificationType, int pageSize, int page)
        {
            var notifications = await _notificationRepository.GetNotifications(NotificationType,pageSize,page);
            int totalCount = await _notificationRepository.Count();
            var result = new NotificationResult { Notifications = notifications, Pages = page, PageSize = pageSize, Total = totalCount };
            return _responseService.ExecutionResponse<NotificationResult>("successfully retrieved notifications", result, true);
        }

         
    }
}
