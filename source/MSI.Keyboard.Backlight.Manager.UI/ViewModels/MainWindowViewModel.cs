using MSI.Keyboard.Backlight.Manager.UI.Services;
using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Input;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRestoreConfigurationService _restoreConfigurationService;

        public DateTime Now => DateTime.Now;

        public ICommand OpenGithubCommand { get; }
        public ICommand RestoreConfigurationCommand { get; }

        public MainWindowViewModel(IRestoreConfigurationService restoreConfigurationService)
        {
            OpenGithubCommand = new RelayCommand<object>(OpenGithub);
            RestoreConfigurationCommand = new RelayCommand<object>(RestoreConfiguration);

            InitTimer();
            _restoreConfigurationService = restoreConfigurationService;
        }

        private void RestoreConfiguration(object obj)
        {
            _restoreConfigurationService.RestoreIfNeeded();
        }

        private void InitTimer()
        {
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += (o, e) => this.RaisePropertyChanged(nameof(Now));
            timer.Start();
        }

        private void OpenGithub(object obj)
        {
            Process.Start("cmd", "/C start https://www.softpower.pl");
        }
    }
}
