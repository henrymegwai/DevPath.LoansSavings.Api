using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<int> Count();
        Task<NotificationDto> CreateNotification(NotificationDto model);
        List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null);
        List<T> DapperSqlWithParams<T>(string sql, string connectionnName = null);
        Task<NotificationDto> GetNotification(long Id); 
        Task<NotificationResponse[]> GetNotifications(int NotificationType, int PageSize, int Page);
    }
}
