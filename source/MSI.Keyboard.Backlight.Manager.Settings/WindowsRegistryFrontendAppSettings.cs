using Microsoft.Win32;
using System.Reflection;

namespace MSI.Keyboard.Backlight.Manager.Settings
{
    public class WindowsRegistryFrontendAppSettings : IFrontendAppSettings
    {
        private const string AppName = "MSI.Keyboard.Backlight.Manager";

        private const string RunSectionLocation = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string AppSectionLocation = "SOFTWARE\\SoftPower";

        private readonly string _executingAssemblyLocation;

        public bool StartMinimized
        {
            get => GetBooleanValueFromRegistry(AppSectionLocation, nameof(StartMinimized));
            set => SetRegistryValue(AppSectionLocation, nameof(StartMinimized), value.ToString());
        }

        public bool ApplyConfigurationOnStartup
        {
            get => GetBooleanValueFromRegistry(AppSectionLocation, nameof(ApplyConfigurationOnStartup));
            set => SetRegistryValue(AppSectionLocation, nameof(ApplyConfigurationOnStartup), value.ToString());
        }

        public bool RunOnStartup
        {
            get => GetRegistryValue(RunSectionLocation, AppName) != null;
            set => SetRegistryValue(RunSectionLocation, AppName, value ? _executingAssemblyLocation : null);
        }

        public WindowsRegistryFrontendAppSettings()
        {
            _executingAssemblyLocation = Assembly.GetEntryAssembly().Location.Replace(".dll", ".exe");
        }

        private bool GetBooleanValueFromRegistry(string location, string name)
        {
            return bool.Parse(GetRegistryValue(location, name) ?? "False");
        }

        private string GetRegistryValue(string location, string name)
        {
            var subKey = Registry.CurrentUser.CreateSubKey(location);

            return subKey.GetValue(name) as string;
        }

        private void SetRegistryValue(string location, string name, string value)
        {
            var subKey = Registry.CurrentUser.CreateSubKey(location, true);

            if(value is null)
            {
                subKey.DeleteValue(name);
            }
            else
            {
                subKey.SetValue(name, value);
            }
        }
    }
}