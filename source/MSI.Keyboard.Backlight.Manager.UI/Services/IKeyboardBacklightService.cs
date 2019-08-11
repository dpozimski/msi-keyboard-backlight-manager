using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.UI.Services
{
    public interface IKeyboardBacklightService
    {
        Task RestoreIfNeeded();
        Task<bool> IsDeviceSupported();
        Task<JobsConfiguration> GetConfiguration();
        Task ApplyConfiguration(JobsConfiguration configuration);
        Task StopBacklightKeyboardManagement();
    }
}
