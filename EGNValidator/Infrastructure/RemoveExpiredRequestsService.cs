using EGNValidator.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EGNValidator.Infrastructure
{
    public class RemoveExpiredRequestsService : ScheduledProcessor
    {
        protected override string Schedule => "3 0 * * * ";

        public RemoveExpiredRequestsService(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {

        }
        public async override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var requestManager = serviceProvider.GetRequiredService<IValidationManager>();
            await requestManager.RemoveExpiredRequests();
        }
    }
}
