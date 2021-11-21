using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Data.Entities;
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
    public class StandingOrderRepository : IStandingOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public StandingOrderRepository(AppDbContext context)
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
        public async Task<StandingOrderDto> CreateStandingOrder(StandingOrderDto model)
        {
            StandingOrder standingOrder = model.Map();
            _context.Set<StandingOrder>().Add(standingOrder);
            await _context.SaveChangesAsync();
            return standingOrder.Map();
        }

        public async Task<bool> DeleteStandingOrder(long Id)
        {
            var entity = await _context.Set<StandingOrder>().FindAsync(Id);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StandingOrderDto> GetStandingOrder(long Id)
        {
            var entity = await _context.Set<StandingOrder>()
                .FindAsync(Id);

            if (entity == null)
                return null;

            return entity.Map();
        }
    }
}
