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
    public class BankRepository : IBankRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public BankRepository(AppDbContext context, IConfiguration config)
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
        public async Task<BankDto> CreateBank(BankDto model)
        {
            Bank bank = model.Map();
            _context.Set<Bank>().Add(bank);
            await _context.SaveChangesAsync();
            return bank.Map();
        }

        public async Task<bool> DeleteBank(long Id)
        {
            var entity = await _context.Set<Bank>().FindAsync(Id);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BankDto> GetBank(long Id)
        {
            var entity = await _context.Set<Bank>()
                .FindAsync(Id);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<BankDto> GetBankByName(string name)
        {
            var entity = await _context.Set<Bank>().FirstOrDefaultAsync(x=>x.Name.ToLower() == name.ToLower());

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<BankDto> UpdateBank(BankDto model, long id)
        {
            var entity = await _context.Set<Bank>().FindAsync(id);

            if (entity == null)
                return null;
            entity.Name = model.Name;
            entity.Code = model.Code;
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

        public async Task<BankDto[]> GetBanks()
        {
            var entity = await _context.Set<Bank>().ToArrayAsync();
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
    }
}
