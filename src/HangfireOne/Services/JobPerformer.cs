using System.Diagnostics;
using System.Threading.Tasks;

namespace HangfireOne.Services
{
    public class JobPerformer : IInjectedInterface
    {
        public void PerformBackgroundJob()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Delay(100);
                Trace.TraceInformation($"BackgroundJob{i}", i);
            }
        }

        public void PerformRecurrentJob()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Delay(100);
                Trace.TraceInformation($"RecurrentJob:{i}");
            }
        }

        public void PerformScheduledJob()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Delay(100);
                Trace.TraceInformation($"ScheduledJob: {i}");
            }
        }
    }
}
