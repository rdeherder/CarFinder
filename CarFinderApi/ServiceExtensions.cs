using CarFinderApi.Library.ExternalDataAccess;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CarFinderApi
{
    public static class ServiceExtensions
    {
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration config)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(config.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();
        }

        public static void ConfigureHangfireJobs(this IApplicationBuilder app, IConfiguration config, IRecurringJobManager recurringJobs)
        {
            app.UseHangfireDashboard();

            int refreshTimeOutInMinutes = config.GetValue<int>("ExternalCarsApiRefreshTimeOutInMinutes");
            string interval = IntervalInMinutesAsCronExpression(refreshTimeOutInMinutes);

            recurringJobs.AddOrUpdate<ExternalCarsData>("GetExternalCarsData", job => job.RetrieveAllFromExternalApi(), interval);
        }

        /// <summary>
        /// Returns a string in cron expression formatted as do the task every x minutes, 
        /// starting from (now + x minutes) where x is the interval.
        /// </summary>
        /// <remarks>
        /// I.e. When the intervalparameter is 15, it returns a cron expression 
        /// to execute a task every 15 minutes from fifteen minutes from now.
        /// </remarks>
        private static string IntervalInMinutesAsCronExpression(int intervalInMinutes)
        {
            if (intervalInMinutes <= 0)
            {
                throw new Exception("Please enter a valid interval.");
            }

            int currentMinute = DateTime.Now.Minute;

            // Cron format:
            // Seconds 	Minutes     Hours 	Day Of Month 	Month 	Day Of Week
            var result = $"* {currentMinute + intervalInMinutes}/{intervalInMinutes} * * * *";

            return result;
        }
    }
}
