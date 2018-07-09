using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using Services.Hangfire;
using System;

[assembly: OwinStartup(typeof(Services.Startup))]
namespace Services
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var sqlOptions = new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(15), // Default value
                PrepareSchemaIfNecessary = true, //default true
                JobExpirationCheckInterval = TimeSpan.FromHours(1), // 1hr default
                CountersAggregateInterval = TimeSpan.FromMinutes(5) // 5 mins default
            };

            // Storage is the only thing required for basic configuration.
            // Just discover what configuration options do you have.
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultHangfire", sqlOptions)
                //.UseColouredConsoleLogProvider()
                .UseElmahLogProvider();
                //.UseActivator(...)
                //.UseLogProvider(...)

            var backgroundJobServerOptions = new BackgroundJobServerOptions()
            {
                Queues = new[] { "normal", "default" },
                SchedulePollingInterval = new TimeSpan(0, 0, 10),
                ServerCheckInterval = new TimeSpan(0, 0, 10),
                ServerName = string.Format(
                    "{0}.{1}",
                    Environment.MachineName,
                    Guid.NewGuid())
            };

            app.UseHangfireServer(backgroundJobServerOptions);
            GlobalConfiguration.Configuration.UseFilter(new JobContext());

            var hangfireManager = new HangfireManager();
            hangfireManager.LoadScheduleJob();
        }
    }
}