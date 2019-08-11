using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using System;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Settings
{
    public class InMemoryCacheBacklightConfigurationRepository : IBacklightConfigurationRepository
    {
        private readonly JsonBacklightConfigurationRepository _jsonBacklightConfigurationRepository;

        private JobsConfiguration _cachedBacklightConfiguration;

        public InMemoryCacheBacklightConfigurationRepository(JsonBacklightConfigurationRepository jsonBacklightConfigurationRepository)
        {
            _jsonBacklightConfigurationRepository = jsonBacklightConfigurationRepository;
        }

        public async Task<JobsConfiguration> GetConfiguration()
        {
            if (_cachedBacklightConfiguration != null)
                return _cachedBacklightConfiguration;

            return _cachedBacklightConfiguration = await _jsonBacklightConfigurationRepository.GetConfiguration();
        }

        public async Task SaveConfiguration(JobsConfiguration configuration)
        {
            await _jsonBacklightConfigurationRepository.SaveConfiguration(configuration);

            _cachedBacklightConfiguration = configuration;
        }
    }
}
