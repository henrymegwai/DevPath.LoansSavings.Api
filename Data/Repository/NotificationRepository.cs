using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Data.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public NotificationRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("BlinkCashDbContext"));
            }
        }
        public async Task<NotificationDto> CreateNotification(NotificationDto model)
        {
            Notification bank = model.Map();
            _context.Set<Notification>().Add(bank);
            await _context.SaveChangesAsync();
            return bank.Map();
        }

      
        public async Task<NotificationDto> GetNotification(long Id)
        {
            var entity = await _context.Set<Notification>()
                .FindAsync(Id);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<int> Count()
        {
            var entity = await _context.Set<Notification>().CountAsync();
            return entity;
        }

        public List<T> DapperSqlWithParams<T>(string sql, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        public async Task<NotificationResponse[]> GetNotifications(int NotificationType, int PageSize, int Page)
        {

            string query = GetQueryString(NotificationType);
            int skip = (Page - 1) * PageSize;
            query = query + " order by CreatedDate desc offset  " + skip + "  rows fetch next " + PageSize + " rows only";
            var result = await Execute(query);
            return result; 
 
        }
         
        private string GetQueryString(int NotificationType)
        {
            string sqlQuery = @"select * from [Notification] where 1=1";

            
                switch (NotificationType)
                {
                    
                    case 1:
                        sqlQuery += @" and ((NotificationType = '" + (int)NotificationType + "'))";
                        break;
                    case 2:
                        sqlQuery += @" and ((NotificationType = '" + (int)NotificationType + "'))";
                    break;
                    case 0:
                        sqlQuery += @"";
                        break;
                    default:
                        break;
                } 
            return sqlQuery;
        }


        private async Task<NotificationResponse[]> Execute(string sqlQuery)
        {
            List<NotificationResponse> result = new List<NotificationResponse>();
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = sqlQuery;
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (var response = await command.ExecuteReaderAsync())
                    {
                        while (response.Read())
                        {
                            var e = new NotificationResponse();
                            e.AccountName = response["AccountName"].ToString();
                            e.Amount = string.IsNullOrEmpty(response["Amount"].ToString()) ? 0 : decimal.Parse(response["Amount"].ToString());
                            e.Id = long.Parse(response["Id"].ToString());
                            e.Channel = response["Channel"].ToString();
                            e.Naration = response["Naration"].ToString();
                            e.CreatedDate = string.IsNullOrEmpty(response["CreatedDate"].ToString()) ? string.Empty : DateTime.Parse(response["CreatedDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            result.Add(e);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }

            return result.ToArray();
        }

        public List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql, (object)parms).ToList();
            }
        }

        
    }
}
