using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager
{
    public interface IBacklightConfigurationRepository
    {
        Task<JobsConfiguration> GetConfiguration();
        Task SaveConfiguration(JobsConfiguration configuration);
    }
}