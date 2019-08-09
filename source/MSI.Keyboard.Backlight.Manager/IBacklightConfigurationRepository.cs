using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager
{
    public interface IBacklightConfigurationRepository
    {
        Task<BacklightConfiguration> GetConfiguration();
        Task SaveConfiguration(BacklightConfiguration configuration);
    }
}