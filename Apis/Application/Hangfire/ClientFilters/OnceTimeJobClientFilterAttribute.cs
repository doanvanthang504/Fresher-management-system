using Application.Hangfire.Models;
using Global.Shared.JsonConverters;
using Hangfire.Client;
using Hangfire.Common;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Application.Hangfire.ClientFilters
{
    internal class OnceTimeJobClientFilterAttribute : JobFilterAttribute, IClientFilter
    {
        private const string JOB_ID_KEY = "JobId";

        public void OnCreated(CreatedContext filterContext)
        {
            if (!filterContext.Canceled)
            {
                var jobKey = GetJobKey(filterContext.Job);
                var keyValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(JOB_ID_KEY, filterContext.BackgroundJob.Id)
                };
                filterContext.Connection.SetRangeInHash(jobKey, keyValuePairs);
            }
        }

        public void OnCreating(CreatingContext filterContext)
        {
            var jobKey = GetJobKey(filterContext.Job);
            var entries = filterContext.Connection.GetAllEntriesFromHash(jobKey);
            if (entries != null && entries.ContainsKey(JOB_ID_KEY))
            {
                filterContext.Canceled = true;
            }
        }

        private static string GetJobKey(Job job)
        {
            var jobData = new JobData(job);
            var serializedJobData = JsonSerializer.Serialize(jobData, ApplicationWideJsonConverter.DefaultSerializerOptions);
            var jobDataBytes = Encoding.UTF8.GetBytes(serializedJobData);
            using var md5Hasher = MD5.Create();
            var hashedBytes = md5Hasher.ComputeHash(jobDataBytes);
            return Convert.ToHexString(hashedBytes);
        }
    }
}
