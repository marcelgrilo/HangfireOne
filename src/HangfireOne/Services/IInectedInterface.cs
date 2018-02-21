namespace HangfireOne.Services
{
    public interface IInjectedInterface
    {
        void PerformBackgroundJob();
        void PerformScheduledJob();
        void PerformRecurrentJob();
    }
}