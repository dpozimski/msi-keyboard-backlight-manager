using MSI.Keyboard.Backlight.Manager.Settings;
using MSI.Keyboard.Backlight.Manager.UI.Services;
using System;
using System.Windows.Input;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly IFrontendAppSettings _frontendAppSettings;
        private readonly IKeyboardBacklightService _keyboardBacklightService;
        private bool _deviceSupported;
        private bool _configurationChanged;
        private BacklightConfiguration _backlightConfiguration;

        public bool DeviceSupported
        {
            get => _deviceSupported;
            private set => RaiseAndSetIfChanged(ref _deviceSupported, value);
        }

        public bool ConfigurationChanged
        {
            get => _configurationChanged;
            private set => RaiseAndSetIfChanged(ref _configurationChanged, value);
        }

        public int Intensity
        {
            get => _backlightConfiguration.Intensity;
            set
            {
                _backlightConfiguration.Intensity = value;
                RaisePropertyChanged();
                ConfigurationChanged = true;
            }
        }

        public bool TaskbarColorDependentSelected
        {
            get => _backlightConfiguration.Mode == BacklightMode.TaskbarColorDependent;
        }

        public bool RgbDependentSelected
        {
            get => _backlightConfiguration.Mode == BacklightMode.Rgb;
        }

        public bool ApplyConfigurationOnStartup
        {
            get => _frontendAppSettings.ApplyConfigurationOnStartup;
            set => _frontendAppSettings.ApplyConfigurationOnStartup = value;
        }

        public bool StartMinimized
        {
            get => _frontendAppSettings.StartMinimized;
            set => _frontendAppSettings.StartMinimized = value;
        }

        public bool RunOnStartup
        {
            get => _frontendAppSettings.RunOnStartup;
            set => _frontendAppSettings.RunOnStartup = value;
        }

        public ICommand ApplyConfigurationCommand { get; }

        public ICommand ApplyBacklightModeCommand { get; }

        public ICommand RestoreBacklightConfigurationCommand { get; }

        public ConfigurationViewModel(IFrontendAppSettings frontendAppSettings,
                                      IKeyboardBacklightService keyboardBacklightService)
        {
            _frontendAppSettings = frontendAppSettings;
            _keyboardBacklightService = keyboardBacklightService;
            _backlightConfiguration = new BacklightConfiguration();
            _configurationChanged = true;

            ApplyConfigurationCommand = new RelayCommand<object>(ApplyConfiguration);
            ApplyBacklightModeCommand = new RelayCommand<string>(ApplyBacklightMode);
            RestoreBacklightConfigurationCommand = new RelayCommand<object>(RestoreBacklightConfiguration);
        }

        private async void RestoreBacklightConfiguration(object obj)
        {
            _deviceSupported = await _keyboardBacklightService.IsDeviceSupported();
            _backlightConfiguration = await _keyboardBacklightService.GetConfiguration();

            RaisePropertyChanged(null);
        }

        private void ApplyBacklightMode(string obj)
        {
            var mode = (BacklightMode)Enum.Parse(typeof(BacklightMode), obj);

            _backlightConfiguration.Mode = mode;

            ConfigurationChanged = true;
        }

        private async void ApplyConfiguration(object obj)
        {
            await _keyboardBacklightService.ApplyConfiguration(_backlightConfiguration);

            ConfigurationChanged = false;
        }
    }
}
