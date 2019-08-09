using System;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Settings
{
    public class InMemoryCacheBacklightConfigurationRepository : IBacklightConfigurationRepository
    {
        private readonly JsonBacklightConfigurationRepository _jsonBacklightConfigurationRepository;

        private BacklightConfiguration _cachedBacklightConfiguration;

        public InMemoryCacheBacklightConfigurationRepository(JsonBacklightConfigurationRepository jsonBacklightConfigurationRepository)
        {
            _jsonBacklightConfigurationRepository = jsonBacklightConfigurationRepository;
        }

        public async Task<BacklightConfiguration> GetConfiguration()
        {
            if (_cachedBacklightConfiguration != null)
                return _cachedBacklightConfiguration;

            return _cachedBacklightConfiguration = await _jsonBacklightConfigurationRepository.GetConfiguration();
        }

        public async Task SaveConfiguration(BacklightConfiguration configuration)
        {
            await _jsonBacklightConfigurationRepository.SaveConfiguration(configuration);

            _cachedBacklightConfiguration = configuration;
        }
    }
}
