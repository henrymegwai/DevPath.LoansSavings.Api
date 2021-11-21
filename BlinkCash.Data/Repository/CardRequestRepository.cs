using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Utilities;
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
    public class CardRequestRepository : ICardRequestRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public CardRequestRepository(AppDbContext context, IConfiguration config)
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
        public async Task<CardRequestDto> AddCardRequest(CardRequestDto model)
        {
            CardRequest bank = model.Map();
            _context.Set<CardRequest>().Add(bank);
            await _context.SaveChangesAsync();
            return bank.Map();
        }

        public async Task<bool> DeleteCardRequest(long Id, string UserId)
        {
            var entity = await _context.Set<CardRequest>()
                .FirstOrDefaultAsync(x => x.Id == Id && x.UserId == UserId);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        } 

        public async Task<CardRequestDto> UpdateCardRequest(CardRequestDto model, long id, string userId)
        {
            var entity = await _context.Set<CardRequest>()
              .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (entity == null)
                return null;
             
            entity.Address = model.Address;
            entity.Address2 = model.Address2;
            entity.CardType = model.CardType;
            entity.City = model.City;
            entity.DeliveryStatus = model.DeliveryStatus;
            entity.Landmark = model.Landmark;
            entity.LGA = model.LGA;
            entity.PaymentReference = model.PaymentReference;
            entity.PaymentType = model.PaymentType;
            entity.State = model.State;
            entity.UserId = model.UserId;
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

        public async Task<CardRequestDto[]> GetCardRequests(string UserId)
        {
            var entity = await _context.Set<CardRequest>().Where(x => x.UserId == UserId).ToArrayAsync();
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

        public async Task<CardRequestDto> GetCardRequestById(long Id, string userId)
        {
            var entity = await _context.Set<CardRequest>()
               .FirstOrDefaultAsync(x => x.Id == Id && x.UserId == userId);

            if (entity == null)
                return null;

            return entity.Map();
        } 
        public async Task<CardRequestDto> GetCardRequestByCardType(CardType cardType, string userId)
        {
            var entity = await _context.Set<CardRequest>()
               .FirstOrDefaultAsync(x => x.CardType == cardType && x.UserId == userId);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<int> Count()
        {
            var entity = await _context.Set<CardRequest>().CountAsync();
            return entity;
        }
    }
}
