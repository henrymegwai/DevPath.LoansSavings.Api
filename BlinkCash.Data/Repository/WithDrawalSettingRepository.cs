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
    public class WithDrawalSettingRepository : IWithDrawalSettingRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public WithDrawalSettingRepository(AppDbContext context)
        {
            _context = context;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("BlinkCashDbContext"));
            }
        }

        public List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql, (object)parms).ToList();
            }
        }

        public async Task<WithDrawalSettingDto> CreateWithDrawalSetting(WithDrawalSettingDto model)
        {
            WithDrawalSetting bank = model.Map();
            _context.Set<WithDrawalSetting>().Add(bank);
            await _context.SaveChangesAsync();
            return bank.Map();
        }

        public async Task<bool> DeleteWithDrawalSetting(long Id)
        {
            var entity = await _context.Set<WithDrawalSetting>().FindAsync(Id);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WithDrawalSettingDto> GetWithDrawalSetting(long Id)
        {
            var entity = await _context.Set<WithDrawalSetting>()
               .FindAsync(Id);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<WithDrawalSettingDto> GetWithDrawalSettingById(string UserId, long Id)
        {
            var entity = await _context.Set<WithDrawalSetting>()
                .FirstOrDefaultAsync(x=>x.UserId == UserId && x.Id == Id);

            if (entity == null)
                return null;

            return entity.Map();
        }
        public async Task<WithDrawalSettingDto> GetWithDrawalSettingByBankId(string UserId, long bankId)
        {
            var entity = await _context.Set<WithDrawalSetting>()
                .FirstOrDefaultAsync(x => x.UserId == UserId && x.BankId == bankId);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<WithDrawalSettingDto> GetWithDrawalSettingByUserId(string UserId)
        {
            var entity = await _context.Set<WithDrawalSetting>()
                .FirstOrDefaultAsync(x => x.UserId == UserId);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<WithDrawalSettingDto> UpdateWithDrawalSetting(WithDrawalSettingDto model, long id)
        {
            var entity = await _context.Set<WithDrawalSetting>().FindAsync(id);

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
    }
}
