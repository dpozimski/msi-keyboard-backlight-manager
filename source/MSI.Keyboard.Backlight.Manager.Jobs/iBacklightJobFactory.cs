using MSI.Keyboard.Backlight.Manager.Jobs.Models;
namespace MSI.Keyboard.Backlight.Manager.Jobs
{
    public interface IBacklightJobFactory
    {
        IBacklightJob Create(BacklightJobType jobType);
    }
}
