using Application.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace WebAPI.Middlewares
{
    public static class HangfireMiddleware
    {
        public static void UseCronJobs(this IApplicationBuilder _)
        {
            //RecurringJob.AddOrUpdate<ICronJobService>(x => x.AutoGetChemicalAsync(), Cron.Minutely());
            //RecurringJob.AddOrUpdate<ICronJobService>(x => x.AutoGetChemicalPagingsionAsync(), Cron.Minutely());
            RecurringJob.AddOrUpdate<ICronJobService>(x => x.AutoCreateReminderAsync(), Cron.Minutely());
        }
    }
}
