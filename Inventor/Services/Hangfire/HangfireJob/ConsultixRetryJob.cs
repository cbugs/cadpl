using System;
using Hangfire;
using Cadbury.Inventor.Core.Module;

namespace Services.Hangfire.HangfireJob
{
    [Queue("normal")]
    public class ConsultixRetryJob : JobBase
    {
        public int Process()
        {
            try
            {
                var consultixRetryModule = new ConsultixRetryModule();
                consultixRetryModule.Process();

                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}