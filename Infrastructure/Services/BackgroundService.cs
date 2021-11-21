using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class BackgroundService : IBackgroundService
    {
        private readonly IPlanRepository _planRepository;

        public BackgroundService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public async Task RunDailySchedule()
        {
            await RunDailyInterest();
        }         
        private async Task RunDailyInterest()
        {
            var allplanswithInterest = await _planRepository.GetAllPlanWithInterest();
            if(allplanswithInterest.Count() > 0)
            {
                foreach (var plan in allplanswithInterest)
                {
                    decimal UpdateDailyInterest = (plan.TotalAmountSaved * plan.DailyInterest) / 100;

                    await _planRepository.UpdateDailyInterest(UpdateDailyInterest, plan.Id);
                }
            }
        }
    }
}
