using Cadbury.Inventor.Core.Module;
using System;
using System.ComponentModel;

namespace Services.Hangfire.HangfireJob
{
    public class FilePurgeJob : JobBase
    {
        private const int daysPeriod = 3;

        public int Process()
        {
            try
            {
                var filePurgeModule = new FilePurgeModule()
                {
                    DaysPeriodFromNow = daysPeriod
                };
                filePurgeModule.Process();

                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}