using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager
{
    public interface IConfigurationRepository
    {
        Task<BacklightConfiguration> GetConfiguration();
        Task SaveConfiguration(BacklightConfiguration configuration);
    }
}