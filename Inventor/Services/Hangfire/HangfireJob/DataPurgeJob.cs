using Cadbury.Inventor.Core.Module;
using System;
using System.Configuration;

namespace Services.Hangfire.HangfireJob
{
    public class DataPurgeJob : JobBase
    {
        private const int daysPeriod = 15;

        public int Process()
        {
            try
            {
                var dataPurgeModule = new DataPurgeModule()
                {
                    DaysPeriodFromNow = Convert.ToInt32(ConfigurationManager.AppSettings["PurgePeriod"])
                };
                dataPurgeModule.Process();

                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}