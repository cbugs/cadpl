using Hangfire;
using Services.Hangfire.HangfireJob;

namespace Services.Hangfire
{
    public class HangfireManager
    {
        public void LoadScheduleJob()
        {
            // Send failed Consultix transaction every 15 minutes
            RecurringJob.AddOrUpdate<ConsultixRetryJob>("ConsultixRetryJob", job => job.Process(), Cron.MinuteInterval(15));

            // Test job everyday
            RecurringJob.AddOrUpdate(() => new FilePurgeJob().Process(), Cron.DayInterval(1));

            // Test job every 1 minute
            // RecurringJob.AddOrUpdate(() => new TestJob().Process(), Cron.MinuteInterval(1));

            // Purge data every day
            // RecurringJob.AddOrUpdate<DataPurgeJob>("DataPurgeJob", job => job.Process(), Cron.Daily);
        }
    }
}