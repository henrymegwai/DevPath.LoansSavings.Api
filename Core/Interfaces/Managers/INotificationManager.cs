using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface INotificationManager
    {
        Task<ExecutionResponse<NotificationDto>> CreateNotification(NotificationDto notificationDto);
        Task<ExecutionResponse<NotificationDto>> GetNotification(long id); 
        Task<ExecutionResponse<NotificationResult>> GetNotifications(int NotificationType, int pageSize, int page);
    }
}
