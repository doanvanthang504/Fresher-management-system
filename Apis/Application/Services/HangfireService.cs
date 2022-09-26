using Application.Interfaces;
using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HangfireService : IHangfireService
    {
        private readonly IBackgroundJobClient _backgroundJob;

        public HangfireService(IBackgroundJobClient backgroundJob)
        {
            _backgroundJob = backgroundJob;
        }

        public void CreateDelayedTask(Expression<Func<Task>> expression, TimeSpan delayedTime)
        {
            _backgroundJob.Schedule(expression, delayedTime);
        }

        public void CreateDelayedTask<TValue>(Expression<Func<TValue, Task>> methodCall, TimeSpan delayedTime)
        {
            _backgroundJob.Schedule(methodCall, delayedTime);
        }

        public void CreateFireAndForgetTask(Expression<Func<Task>> expression)
        {
            _backgroundJob.Enqueue(expression);
        }

        public void CreateFireAndForgetTask<TValue>(Expression<Func<TValue, Task>> methodCall)
        {
            _backgroundJob.Enqueue(methodCall);
        }
    }
}
