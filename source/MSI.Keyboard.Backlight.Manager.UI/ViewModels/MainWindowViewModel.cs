using MSI.Keyboard.Backlight.Manager.Notifications;
using MSI.Keyboard.Backlight.Manager.UI.Services;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IKeyboardBacklightService _keyboardBacklightService;
        private readonly INotificationService _notificationService;

        public ICommand OpenGithubCommand { get; }
        public ICommand RestoreConfigurationCommand { get; }
        public ICommand StopKeyboardBacklightManagementCommand { get; }

        public MainWindowViewModel(IKeyboardBacklightService keyboardBacklightService,
                                   INotificationService notificationService)
        {
            OpenGithubCommand = new RelayCommand<object>(OpenGithub);
            RestoreConfigurationCommand = new RelayCommand<object>(RestoreConfiguration);
            StopKeyboardBacklightManagementCommand = new RelayCommand<object>(StopKeyboardBacklightManagement);

            _keyboardBacklightService = keyboardBacklightService;
            _notificationService = notificationService;
        }

        private async void StopKeyboardBacklightManagement(object obj)
        {
            await _keyboardBacklightService.StopBacklightKeyboardManagement();
        }

        private async void RestoreConfiguration(object obj)
        {
            var deviceSupported = await _keyboardBacklightService.IsDeviceSupported();

            if (!deviceSupported)
            {
                _notificationService.ShowError("Your device is not supported");

                return;
            }

            await _keyboardBacklightService.RestoreIfNeeded();
        }

        private void OpenGithub(object obj)
        {
            Process.Start("cmd", "/C start https://www.softpower.pl");
        }
    }
}
