using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IStandingOrderRepository
    {
        Task<StandingOrderDto> CreateStandingOrder(StandingOrderDto model);
        Task<bool> DeleteStandingOrder(long Id);
        Task<StandingOrderDto> GetStandingOrder(long Id);
    }
}
