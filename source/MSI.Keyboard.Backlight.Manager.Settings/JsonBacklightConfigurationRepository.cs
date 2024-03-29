﻿using MSI.Keyboard.Backlight.Manager.Jobs.Models;
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


        public async Task<JobsConfiguration> GetConfiguration()
        {
            if(FileExists())
            {
                return await GetConfigurationFromFile();
            }

            return GetDefaultConfiguration();
        }

        public async Task SaveConfiguration(JobsConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            await SaveConfigurationToFile(configuration);
        }

        private bool FileExists()
        {
            return File.Exists(_filePath);
        }

        private async Task SaveConfigurationToFile(JobsConfiguration configuration)
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

        private async Task<JobsConfiguration> GetConfigurationFromFile()
        {
            using (var fs = File.OpenRead(_filePath))
            using (var sr = new StreamReader(fs))
            {
                var content = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<JobsConfiguration>(content);
            }
        }

        private JobsConfiguration GetDefaultConfiguration()
        {
            return new JobsConfiguration()
            {
                Mode = BacklightJobType.TaskbarColorDependent,
                Intensity = 100
            };
        }
    }
}
