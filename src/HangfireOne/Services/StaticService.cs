using System;
using System.Diagnostics;
using Hangfire;

namespace HangfireOne.Services
{
    public static class StaticService
    {
        public static void Count()
        {
            for (int i = 0; i < 100; i++)
            {
                Trace.TraceInformation("{0}", i);
            }
        }

        [AutomaticRetry(Attempts = 10)]
        public static void CountWithCrashChance()
        {
            var rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                if (rnd.NextDouble() > .9)
                {
                    string message = $"ERROR{i}";
                    Trace.TraceError(message);
                    throw new Exception(message);
                }
                Trace.TraceInformation("{0}", i);
            }
        }
    }

}
