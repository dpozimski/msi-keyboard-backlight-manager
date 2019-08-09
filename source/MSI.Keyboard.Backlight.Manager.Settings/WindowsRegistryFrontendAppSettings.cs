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
            get => GetRegistryValue(AppSectionLocation, nameof(StartMinimized)) != null;
            set => SetRegistryValue(AppSectionLocation, nameof(StartMinimized), value);
        }

        public bool ApplyConfigurationOnStartup
        {
            get => GetRegistryValue(AppSectionLocation, nameof(ApplyConfigurationOnStartup)) != null;
            set => SetRegistryValue(AppSectionLocation, nameof(ApplyConfigurationOnStartup), value);
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

        private object GetRegistryValue(string location, string name)
        {
            var subKey = Registry.CurrentUser.CreateSubKey(location);

            return subKey.GetValue(name);
        }

        private void SetRegistryValue(string location, string name, object value)
        {
            var subKey = Registry.CurrentUser.CreateSubKey(location, true);

            if(value == default)
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