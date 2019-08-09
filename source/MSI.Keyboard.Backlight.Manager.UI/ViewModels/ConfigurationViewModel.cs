using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using MSI.Keyboard.Backlight.Manager.Settings;
using System;
using System.Windows.Input;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IFrontendAppSettings _frontendAppSettings;

        private bool _configurationChanged;
        private BacklightConfiguration _backlightConfiguration;

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

        public ConfigurationViewModel(IMediator mediator,
                                      IFrontendAppSettings frontendAppSettings)
        {
            _mediator = mediator;
            _frontendAppSettings = frontendAppSettings;

            _backlightConfiguration = new BacklightConfiguration();
            _configurationChanged = true;

            ApplyConfigurationCommand = new RelayCommand<object>(ApplyConfiguration);
            ApplyBacklightModeCommand = new RelayCommand<string>(ApplyBacklightMode);
            RestoreBacklightConfigurationCommand = new RelayCommand<object>(RestoreBacklightConfiguration);
        }

        private async void RestoreBacklightConfiguration(object obj)
        {
            _backlightConfiguration = await _mediator.Send(new GetConfigurationQuery());

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
            await _mediator.Send(new ApplyConfigurationCommand(_backlightConfiguration));

            ConfigurationChanged = false;
        }
    }
}
