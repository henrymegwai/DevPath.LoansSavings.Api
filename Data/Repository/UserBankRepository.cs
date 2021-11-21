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
    public class UserBankRepository : IUserBankRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public UserBankRepository(AppDbContext context, IConfiguration config)
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
        public async Task<UserBankDto> AddUserBank(UserBankDto model)
        {
            UserBank bank = model.Map();
            _context.Set<UserBank>().Add(bank);
            await _context.SaveChangesAsync();
            return bank.Map();
        }

        public async Task<bool> DeleteUserBank(long Id, string UserId)
        {
            var entity = await _context.Set<UserBank>()
                .FirstOrDefaultAsync(x => x.Id == Id && x.UserId == UserId);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserBankDto> GetUserBank(long Id, string UserId)
        {
            var entity = await _context.Set<UserBank>()
                .FirstOrDefaultAsync(x=> x.Id == Id && x.UserId == UserId);

            if (entity == null)
                return null;

            return entity.Map();
        }

         

        public async Task<UserBankDto> UpdateUserBank(UserBankDto model, long id, string userId)
        {
            var entity = await _context.Set<UserBank>()
              .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (entity == null)
                return null;
            entity.AccountNumber = model.AccountNumber;
            entity.AccountName = model.AccountName;
            entity.BankId = model.BankId;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = model.ModifiedBy;             
            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public List<T> DapperSqlWithParams<T>(string sql, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        public async Task<UserBankDto[]> GetUserBanks(string UserId)
        {
            var entity = await _context.Set<UserBank>().Where(x=>x.UserId == UserId).ToArrayAsync();
            if (entity == null)
                return null;

            return entity.Select(x => x.Map()).ToArray();
        }

        public List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql, (object)parms).ToList();
            }
        }

        public async Task<UserBankDto> GetUserBankByBankId(long bankId, string userId)
        {
            var entity = await _context.Set<UserBank>()
               .FirstOrDefaultAsync(x => x.BankId == bankId && x.UserId == userId);

            if (entity == null)
                return null;

            return entity.Map();
        }
    }
}
