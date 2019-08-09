using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Settings
{
    public class JsonBacklightConfigurationRepository : IBacklightConfigurationRepository
    {
        private readonly string _filePath;

        public JsonBacklightConfigurationRepository()
        {
            _filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
                "SoftPower",
                "MSI.Keyboard.Backlight.Manager.Settings",
                "MSIKBMConfig.json");
        }


        public async Task<BacklightConfiguration> GetConfiguration()
        {
            if(FileExists())
            {
                return await GetConfigurationFromFile();
            }

            return GetDefaultConfiguration();
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

        private async Task SaveConfigurationToFile(BacklightConfiguration configuration)
        {
            if(!FileExists())
            {
                var directoryPath = Path.GetDirectoryName(_filePath);
                Directory.CreateDirectory(directoryPath);
            }

            using (var fs = File.Open(_filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (var sw = new StreamWriter(fs))
            {
                var content = JsonConvert.SerializeObject(configuration);

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
                Mode = BacklightMode.TaskbarColorDependent,
                Intensity = 100,
                RefreshInterval = TimeSpan.FromMilliseconds(100)
            };
        }
    }
}
