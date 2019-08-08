using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager
{
    public class JsonConfigurationRepository : IConfigurationRepository
    {
        private readonly string _filePath;

        public JsonConfigurationRepository()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MSIKBMConfig.json");
        }


        public async Task<BacklightConfiguration> GetConfiguration()
        {
            if(FileExists())
            {
                return await GetConfigurationFromFile();
            }

            var defaultConfiguration = GetDefaultConfiguration();

            await SaveConfigurationToFile(defaultConfiguration);

            return defaultConfiguration;
        }

        public async Task SaveConfiguration(BacklightConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            await SaveConfigurationToFile(configuration);
        }

        private bool FileExists()
        {
            return File.Exists(_filePath);
        }

        private async Task SaveConfigurationToFile(BacklightConfiguration defaultConfiguration)
        {
            using (var fs = File.OpenWrite(_filePath))
            using (var sw = new StreamWriter(fs))
            {
                var content = JsonConvert.SerializeObject(_filePath);

                await sw.WriteAsync(content);
            }
        }

        private async Task<BacklightConfiguration> GetConfigurationFromFile()
        {
            using (var fs = File.OpenRead(_filePath))
            using (var sr = new StreamReader(fs))
            {
                var content = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<BacklightConfiguration>(content);
            }
        }

        private BacklightConfiguration GetDefaultConfiguration()
        {
            return new BacklightConfiguration()
            {
                BacklightTaskbarDependent = true
            };
        }
    }
}
