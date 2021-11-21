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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public TransactionRepository(AppDbContext context, IConfiguration config)
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
        public async Task<TransactionDto> Create(TransactionDto model)
        {
            Transaction transaction = model.Map();
            _context.Set<Transaction>().Add(transaction);
            await _context.SaveChangesAsync();
            return transaction.Map();
        }

        public async Task<TransactionDto> Get(string reference)
        {
            var result = await _context.Set<Transaction>()
               .FirstOrDefaultAsync(x => x.TransactionReference == reference);
            var res = result.Map();
            return res;
        }

        public async Task<SavingsConfigurationDto> GetSavingsConfiguration()
        {
            var result = await _context.Set<SavingsConfiguration>().FirstOrDefaultAsync();
            var res = result.Map();
            return res;
        }

        public Task<TransactionDto> Update(TransactionDto model)
        {
            throw new NotImplementedException();
        }

        public List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql, (object)parms).ToList();
            }
        }

        public async Task<int> Count()
        {
            var entity = await _context.Set<Transaction>().CountAsync();
            return entity;
        }


        public async Task<SavingsConfigurationDto> CreateSavingsConfiguration(SavingsConfigurationDto model)
        {
            SavingsConfiguration savingsConfiguration = model.Map();
            _context.Set<SavingsConfiguration>().Add(savingsConfiguration);
            await _context.SaveChangesAsync();
            return savingsConfiguration.Map();
        }

        public async Task<SavingsConfigurationDto> UpdateSavingsConfiguration(SavingsConfigurationDto model)
        {
            var entity = await _context.Set<SavingsConfiguration>().FirstOrDefaultAsync();

            if (entity == null)
                return null;
            entity.Config = model.Config;
            await _context.SaveChangesAsync();
            return entity.Map();
        }
        public async Task<LoanConfigurationDto> CreateLoanConfiguration(LoanConfigurationDto model)
        {
            LoanConfiguration savingsConfiguration = model.Map();
            _context.Set<LoanConfiguration>().Add(savingsConfiguration);
            await _context.SaveChangesAsync();
            return savingsConfiguration.Map();
        }

        public async Task<LoanConfigurationDto> UpdateLoanConfiguration(LoanConfigurationDto model)
        {
            var entity = await _context.Set<LoanConfiguration>().FirstOrDefaultAsync();

            if (entity == null)
                return null;
            entity.Config = model.Config;
            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public async Task<LoanConfigurationDto> GetLoanConfiguration()
        {
            var result = await _context.Set<LoanConfiguration>().FirstOrDefaultAsync();
            var res = result.Map();
            return res;
        }
    }
}
