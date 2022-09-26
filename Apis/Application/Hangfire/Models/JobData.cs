using Hangfire.Common;
using System.Collections.Generic;

namespace Application.Hangfire.Models
{
    internal class JobData
    {
        public string MethodName { get; init; }

        public Dictionary<string, object> ArgsData { get; init; }

        public JobData(Job job)
        {
            var methodInfo = job.Method;
            MethodName = $"{methodInfo.DeclaringType!.FullName}.{methodInfo.Name}";
            ArgsData = new Dictionary<string, object>();
            var methodParams = methodInfo.GetParameters();
            for (int i = 0; i < methodParams.Length; i++)
            {
                ArgsData.Add(methodParams[i].Name!, job.Args[i]);
            }
        }
    }
}
