using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHangfireService
    {
        public void CreateFireAndForgetTask(Expression<Func<Task>> expression);

        public void CreateFireAndForgetTask<TValue>(Expression<Func<TValue, Task>> methodCall);

        public void CreateDelayedTask(Expression<Func<Task>> expression, TimeSpan delayedTime);

        public void CreateDelayedTask<TValue>(Expression<Func<TValue, Task>> methodCall, TimeSpan delayedTime);
    }
}
