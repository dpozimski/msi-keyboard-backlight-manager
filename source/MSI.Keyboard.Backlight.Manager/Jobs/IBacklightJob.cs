using FluentScheduler;

namespace MSI.Keyboard.Backlight.Manager.Jobs
{
    public interface IBacklightJob : IJob
    {
        int Intensity { get; set; }
        bool RequireRefreshing { get; }
    }
}
