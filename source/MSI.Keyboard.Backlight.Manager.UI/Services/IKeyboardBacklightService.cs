using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.UI.Services
{
    public interface IKeyboardBacklightService
    {
        Task RestoreIfNeeded();
        Task<bool> IsDeviceSupported();
        Task<BacklightConfiguration> GetConfiguration();
        Task ApplyConfiguration(BacklightConfiguration configuration);
        Task StopBacklightKeyboardManagement();
    }
}
