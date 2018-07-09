using System;

namespace Services.Hangfire.HangfireJob
{
    public class TestJob : JobBase
    {
        public int Process()
        {
            try
            {
                Console.WriteLine("Hey !");

                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}