using Hangfire;

namespace HangfireOne.Services
{
    public class InstanceService
    {
        IInjectedInterface _injectedInterface;

        public InstanceService(IInjectedInterface injectedInterface)
        {
            _injectedInterface = injectedInterface;
        }

        [Queue("critical")]
        public void CallBackgroundJob()
        {
            _injectedInterface.PerformBackgroundJob();
        }

        [Queue("contingency")]
        public void CallRecurrentJob()
        {
            _injectedInterface.PerformRecurrentJob();
        }

        [Queue("default")]
        public void CallScheduledJob()
        {
            _injectedInterface.PerformScheduledJob();
        }

    }
}
