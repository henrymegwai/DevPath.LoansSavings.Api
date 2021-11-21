using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using BlinkCash.Data.DapperConnection;
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
    public class PlanRepository : IPlanRepository
    {
        private readonly DbContext _context;
        private readonly IDapperContext _dapperContext;
        private readonly IConfiguration _config;
        public PlanRepository(DbContext context, IDapperContext dapperContext, IConfiguration config)
        {
            _context = context;
            _dapperContext = dapperContext;
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new System.Data.SqlClient.SqlConnection("BlinkCashDbContext");
            }
        }
        public async Task<PlanDto> CreatePlan(PlanDto model)
        {
            Plan plan = model.Map();
            _context.Set<Plan>().Add(plan);
            await _context.SaveChangesAsync();
            return plan.Map();
        }

        public async Task<PlanHistoryDto> CreatePlanHistory(PlanDto model)
        {
            PlanHistory plan = model.MapPlanHistory();
            _context.Set<PlanHistory>().Add(plan);
            await _context.SaveChangesAsync();
            return plan.MapPlanHistory();
        }

        public async Task<bool> DeletePlan(long Id)
        {
            var entity = await _context.Set<Plan>().FindAsync(Id);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PlanDto> GetPlan(long Id)
        {
            var entity = await _context.Set<Plan>()
                .FindAsync(Id);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<int> Count()
        {
            var entity = await _context.Set<Plan>().CountAsync();
            return entity;
        }

        public async Task<PlanDto> GetPlanByPlanId(string planId)
        {
            var entity = await _context.Set<Plan>()
                .FirstOrDefaultAsync(x => x.PlanId == planId);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<PlanDto> GetPlanName(string planName, string userId)
        {
            var entity = await _context.Set<Plan>()
                .FirstOrDefaultAsync(x => x.Name == planName.ToLower() && x.UserId == userId);

            if (entity == null)
                return null;

            return entity.Map();
        }

        public async Task<PlanDto[]> GetPlans(PlanSearchVm planSearchDto, int Page, int PageSize)
        {
            string query = GetQueryString(planSearchDto);
            int skip = (Page - 1) * PageSize;
            query = query + " order by CreatedDate desc offset  " + skip + "  rows fetch next " + PageSize + " rows only";
            var result = await Execute(query);
            return result;
        }


        private string GetQueryString(PlanSearchVm planSearchDto)
        {
            string sqlQuery = @"select * from [Plan] where 1=1";

            if (planSearchDto.IsNonInterest.GetValueOrDefault())
                sqlQuery += @" and IsNonInterest = '" + planSearchDto.IsNonInterest + "'";

            if (!planSearchDto.IsNonInterest.GetValueOrDefault())
                sqlQuery += @" and IsNonInterest = '" + planSearchDto.IsNonInterest + "'";

            if (Enum.IsDefined(typeof(PlanStatusCategory), planSearchDto.Status))
            {

                switch (planSearchDto.Status)
                {
                    case PlanStatusCategory.All:
                        break;
                    case PlanStatusCategory.Active:
                        sqlQuery += @" and ((PlanStatus = '" + (int)PlanStatus.Active + "'))";

                        break;

                    case PlanStatusCategory.NotActive:
                        sqlQuery += @" and ((PlanStatus = '" + (int)PlanStatus.NotActive + "'))";
                        break;
                    case PlanStatusCategory.Onhold:
                        sqlQuery += @" and ((PlanStatus = '" + (int)PlanStatus.Onhold + "' ))";
                        break;
                    default:
                        break;
                }
            }

            if (Enum.IsDefined(typeof(SavingType), planSearchDto.SavingType))
            {

                switch (planSearchDto.SavingType)
                {

                    case SavingType.Fixed:
                        sqlQuery += @" and ((SavingsType = '" + (int)SavingType.Fixed + "'))";

                        break;

                    case SavingType.Target:
                        sqlQuery += @" and ((SavingsType = '" + (int)SavingType.Target + "'))";
                        break;
                    case SavingType.Regular:
                        sqlQuery += @" and ((SavingsType = '" + (int)SavingType.Regular + "' ))";
                        break;
                    default:
                        break;
                }
            }
            return sqlQuery;
        }

        public async Task<PlanDto> UpdatePlan(PlanDto model, long id)
        {
            var entity = await _context.Set<Plan>().FindAsync(id);

            if (entity == null)
                return null;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = model.ModifiedBy;
            entity.DebitFrequency = model.DebitFrequency;
            entity.FrequentAmountSaved = model.FrequentAmountSaved;
            entity.TargetAmount = model.TargetAmount;
            entity.TotalAmountSaved = model.TotalAmountSaved;
            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public async Task<PlanDto> UpdatePlanStatus(PlanDto model, long id)
        {
            var entity = await _context.Set<Plan>().FindAsync(id);

            if (entity == null)
                return null;
            entity.ModifiedDate = DateTime.Now;
            entity.PlanStatus = model.PlanStatus;
            await _context.SaveChangesAsync();
            return entity.Map();
        }



        public async Task<PlanDto> UpdateDailyInterest(decimal todayInterest, long id)
        {
            var entity = await _context.Set<Plan>().FindAsync(id);

            if (entity == null)
                return null;
            entity.TotalInterestAccrued += todayInterest;
            _context.Entry<Plan>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity.Map();
        }


        public async Task<PlanDto> UpdatePlanTotalAmountSaved(PlanDto model, long id)
        {
            var entity = await _context.Set<Plan>().FindAsync(id);

            if (entity == null)
                return null;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = model.ModifiedBy;
            entity.TotalAmountSaved = model.TotalAmountSaved;
            await _context.SaveChangesAsync();
            return entity.Map();
        }


        public async Task<PlanDto[]> Execute(string sqlQuery)
        {
            List<PlanDto> result = new List<PlanDto>();
            try
            {
                var connection = _context.Database.GetDbConnection();
                _context.Database.OpenConnection();
                var command = connection.CreateCommand();
                command.CommandText = sqlQuery;
                command.CommandType = CommandType.Text;

                //await connection.OpenAsync();

                using (var response = await command.ExecuteReaderAsync())
                {
                    if (response.HasRows)
                    {
                        while (response.Read())
                        {
                            var e = new PlanDto();
                            e.Name = response["Name"].ToString();
                            e.DateCreated = DateTime.Parse(response["CreatedDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            e.Id = long.Parse(response["Id"].ToString());
                            e.ModifiedBy = response["ModifiedBy"].ToString();
                            e.DailyInterest = string.IsNullOrEmpty(response["DailyInterest"].ToString()) ? 0 : decimal.Parse(response["DailyInterest"].ToString());
                            e.MaturityDate = DateTime.Parse(response["CreatedDate"].ToString());
                            result.Add(e);
                        }
                    }

                }
                await connection.CloseAsync();
            }
            catch (System.Exception ex)
            {
                throw;
            }

            return result.ToArray();
        }

        public async Task<List<PlanDto>> GetAllPlanWithInterest()
        {
            string query = "select * from Plan " +
               "where IsNonInterest =  @IsNonInterest and TransactionStatus in @status and Route in (2,4) order by CreatedDate desc";

            using (var connection = _dapperContext.GetDbConnection())
            {
                var plans = await connection.QueryFirstOrDefaultAsync<List<Plan>>(query, new
                {
                    @IsNonInterest = false

                });
                if (plans == null)
                    return null;

                return plans.Select(x => x.Map()).ToList();
            }
        }


    }
}
