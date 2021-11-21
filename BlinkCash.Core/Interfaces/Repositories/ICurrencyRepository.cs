using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface ICurrencyRepository
    {
        Task<CurrencyDto[]> GetCurrencies(string[] currencyCodes);
        Task<CurrencyDto[]> GetCurrencies();
    }
}
