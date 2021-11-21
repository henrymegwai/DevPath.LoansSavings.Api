using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Utilities;
using BlinkCash.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class WalletBalanceRepository : IWalletBalanceRepository
    {
        //private readonly ILoggerService _loggerService;
        private readonly AppDbContext _dbContext;
        public WalletBalanceRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<WalletBalanceDto[]> Create(long walletId)
        {
            try
            {
                var entities = new[]{ new WalletBalance
            {
                BalanceType = BalanceType.AvailableBalance,
                WalletId = walletId,
            }, new WalletBalance
            {
                BalanceType = BalanceType.BookBalance,
                WalletId = walletId,
            }};

                _dbContext.WalletBalance.AddRange(entities);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //_loggerService.Error(ex);
            }

            return await GetByWalletId(walletId);
        }

        public async Task<WalletBalanceDto[]> GetByWalletId(long walletId)
        {
            var entity = await _dbContext.WalletBalance.Where(a => a.WalletId == walletId).ToArrayAsync();
            if (entity.Length <= 0)
                return Array.Empty<WalletBalanceDto>();

            return entity.Select(a => a.Map()).ToArray();
        }


        public async Task<(bool itExist, decimal availableBalance, decimal bookBalance)> CreateAndGetBalances(long walletId)
        {
            try
            {
                var entities = new[]{ new WalletBalance
            {
                BalanceType = BalanceType.AvailableBalance,
                WalletId = walletId,
            }, new WalletBalance
            {
                BalanceType = BalanceType.BookBalance,
                WalletId = walletId,
            }};

                _dbContext.WalletBalance.AddRange(entities);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //_loggerService.Error(ex);
            }

            return await GetBalances(walletId);
        }


        public async Task<(bool itExist, decimal availableBalance, decimal bookBalance)> GetBalances(long walletId)
        {
            var entity = await _dbContext.WalletBalance.Where(a => a.WalletId == walletId).ToArrayAsync();
            if (entity.Length <= 0)
                return (false, 0, 0);
            var available = entity.FirstOrDefault(a => a.BalanceType == BalanceType.AvailableBalance);
            var bookbalance = entity.FirstOrDefault(a => a.BalanceType == BalanceType.BookBalance);

            return (true, (available == null ? 0 : available.Balance), (bookbalance == null ? 0 : bookbalance.Balance));
        }

    }
}
