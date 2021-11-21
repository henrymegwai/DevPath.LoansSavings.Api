using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Data.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private AppDbContext _dbContext;
        //private readonly IHttpAccessorService _httpAccessorService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWalletBalanceRepository _walletBalanceRepository;
        public WalletRepository(AppDbContext dbContext,
            IServiceProvider serviceProvider, IWalletBalanceRepository walletBalanceRepository)
        {
            _walletBalanceRepository = walletBalanceRepository;
            _serviceProvider = serviceProvider;
            //_httpAccessorService = httpAccessorService;
            _dbContext = dbContext;
        }

        private void IntializeDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
            }
        }
        public async Task<WalletDto> Get(long id)
        {
            WalletDto walletDto = null;
            IntializeDbContext();
            try
            {
                var d = await _dbContext.Wallet 
                      .FirstOrDefaultAsync(x => x.Id == id);

                if (d == null)
                    return null;
                walletDto = d.Map(); 
                //Wallet Balance
                var walletBalance = await _walletBalanceRepository.GetBalances(d.Id);
                if (!walletBalance.itExist)
                    walletBalance = await _walletBalanceRepository.CreateAndGetBalances(d.Id);
                walletDto.Balance = walletBalance.availableBalance;
                walletDto.BookBalance = walletBalance.bookBalance;
            }
            catch (Exception ex)
            {
                return null;
            }
            return walletDto;
        }

        public async  Task<WalletDto[]> Get()
        {
            IntializeDbContext();
            var d = await _dbContext.Wallet 
                .Include(w => w.Currency)
                .ToArrayAsync();

            if (d == null)
                return null;

            List<WalletDto> lst = new List<WalletDto>();
            foreach (var tm in d)
            {
                var e = tm.Map(); 
                if (tm.Currency != null)
                    e.Currency = tm.Currency.Map();

                var walletBalance = await _walletBalanceRepository.GetBalances(tm.Id);
                if (!walletBalance.itExist)
                    walletBalance = await _walletBalanceRepository.CreateAndGetBalances(tm.Id);
                e.Balance = walletBalance.availableBalance;
                e.BookBalance = walletBalance.bookBalance;
                lst.Add(e);
            }

            return lst.ToArray();
        }

        public async Task<WalletDto[]> Get(string userId)
        {
            IntializeDbContext();
            var d = await _dbContext.Wallet.Include(a => a.Currency).Where(s => s.UserId == userId).ToArrayAsync();

            if (d.Length <= 0)
                return Array.Empty<WalletDto>();

            List<WalletDto> lst = new List<WalletDto>();
            foreach (var tm in d)
            {
                var e = tm.Map();
                
                if (tm.Currency != null)
                    e.Currency = tm.Currency.Map();

                var walletBalance = await _walletBalanceRepository.GetBalances(tm.Id);
                if (!walletBalance.itExist)
                    walletBalance = await _walletBalanceRepository.CreateAndGetBalances(tm.Id);
                e.Balance = walletBalance.availableBalance;
                e.BookBalance = walletBalance.bookBalance;
                lst.Add(e);
            }

            return lst.ToArray();
        }

        public async  Task<WalletDto> GetByUserId(string userId)
        {
            IntializeDbContext();
            var d = await _dbContext.Wallet 
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (d == null)
                return null;
            var e = d.Map(); 

            var walletBalance = await _walletBalanceRepository.GetBalances(d.Id);
            if (!walletBalance.itExist)
                walletBalance = await _walletBalanceRepository.CreateAndGetBalances(d.Id);
            e.Balance = walletBalance.availableBalance;
            e.BookBalance = walletBalance.bookBalance;

            return e;
        }

        public async Task<WalletDto> GetByUserId(string userId, string planId)
        {
            IntializeDbContext();
            var d = await _dbContext.Wallet
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PlanId == planId);

            if (d == null)
                return null;
            var e = d.Map();

            var walletBalance = await _walletBalanceRepository.GetBalances(d.Id);
            if (!walletBalance.itExist)
                walletBalance = await _walletBalanceRepository.CreateAndGetBalances(d.Id);
            e.Balance = walletBalance.availableBalance;
            e.BookBalance = walletBalance.bookBalance;

            return e;
        }

        public async Task<WalletDto> Update(WalletDto model)
        {
            IntializeDbContext();
            var entity = await _dbContext.Set<Wallet>().FindAsync(model.Id);

            entity.TransactionThreshhold = model.TransactionThreshhold;
            entity.DailyLimit = model.DailyLimit; 
            entity.WalletType = model.WalletType;
            entity.IsActive = model.IsActive;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            entity.CurrencyId = model.CurrencyId; 
            await _dbContext.SaveChangesAsync();

            return entity.Map();
        }


        //private async Task<bool> UpdateWalletBookBalanceAmount(WalletFundingRequest entity, decimal amount)
        //{

        //    try
        //    {
        //        bool isSaved = false;

        //        decimal openingBookBalance = 0;
        //        decimal closingBookBalance = 0;

        //        using (var connection = _dbContext.GetDbConnection())
        //        {
        //            while (!isSaved)
        //            {
        //                var wallet = await connection.QueryFirstOrDefaultAsync<WalletBalance>($"select * " +
        //                    $"from WalletBalances where WalletId ={entity.WalletId} and BalanceType=1");
        //                if (wallet == null)
        //                    return false;

        //                openingBookBalance = wallet.Balance;
        //                closingBookBalance = openingBookBalance - amount;

        //                string query = $" update WalletBalances set Balance = {closingBookBalance}  OUTPUT inserted.RowVersion where Id = @b and RowVersion= @a";

        //                var rowVer = await connection.ExecuteScalarAsync<byte[]>(query, new { a = wallet.RowVersion, b = wallet.Id });

        //                if (rowVer != null)
        //                    isSaved = true;
        //            }
        //        }

        //        isSaved = false;
        //        while (!isSaved)
        //        {
        //            using (var connection = _dbContext.GetDbConnection())
        //            {
        //                try
        //                {
        //                    string qrry = $" update WalletFundingRequests set BookBalanceOpening={openingBookBalance}," +
        //                                        $"BookBalanceClosing={closingBookBalance},IsCompleted=1,CompletedDate=getdate(),BookBalanceUpdatedDate=getdate() where Id={entity.Id};";
        //                    int count = await connection.ExecuteAsync(qrry);
        //                    if (count >= 0)
        //                    {
        //                        isSaved = true;
        //                        return true;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    IndexingManager indexingManager = new IndexingManager();
        //                    await indexingManager.LogToIndex(new VigiPay.BrokerContract.VigipayIndexingBroker
        //                    {
        //                        ClientCode = Utilities.Constants.IndexingWalletCode,
        //                        Reference = entity.TransactionReference,
        //                        PayLoad = JsonConvert.SerializeObject(new { message = ex.Message, data = entity })
        //                    });

        //                    StackifyLib.Logger.QueueException("Book balance processes", ex);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IndexingManager indexingManager = new IndexingManager();
        //        await indexingManager.LogToIndex(new VigiPay.BrokerContract.VigipayIndexingBroker
        //        {
        //            ClientCode = Utilities.Constants.IndexingWalletCode,
        //            Reference = entity.TransactionReference,
        //            PayLoad = JsonConvert.SerializeObject(new { message = ex.Message, data = entity })
        //        });

        //        StackifyLib.Logger.Queue("ERROR", $"{ex.Message} {ex.StackTrace} {ex.InnerException}");
        //    }

        //    return false;
        //}
    }
}
