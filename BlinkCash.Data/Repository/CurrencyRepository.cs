using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private const string _key = "currencies";
        private readonly IMemoryCache _memoryCache;
        private readonly AppDbContext _dbContext;
        public CurrencyRepository(AppDbContext dbContext, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _dbContext = dbContext;
        }

        public async Task<CurrencyDto[]> GetCurrencies(string[] currencyCodes)
        {
            var p = await GetCurrencies();

            if (p.Length > 0)
            {
                return p
                .Where(s => currencyCodes.Any(x => x == s.CurrencyCode))
                .ToArray();
            }


            return await _dbContext.Set<Currency>()
                .Where(s => currencyCodes.Any(x => x == s.CurrencyCode))
                .Select(x => new CurrencyDto
                {
                    CurrencyCode = x.CurrencyCode,
                    Id = x.Id,
                    Name = x.Name,
                    NumericCode = x.NumericCode
                })
                .ToArrayAsync();
        }
        public async Task<CurrencyDto[]> GetCurrencies()
        {
            CurrencyDto[] bnks;
            bool result = _memoryCache.TryGetValue(_key, out bnks);
            if (result)
                return bnks;

            bnks = await _dbContext.Set<Currency>()
                .Select(x => new CurrencyDto
                {
                    CurrencyCode = x.CurrencyCode,
                    Id = x.Id,
                    Name = x.Name,
                    NumericCode = x.NumericCode
                })
                .ToArrayAsync();

            if (bnks.Length > 0)
                _memoryCache.Set(_key, bnks, DateTimeOffset.Now.AddMinutes(20));

            return bnks;
        }

    }
}
