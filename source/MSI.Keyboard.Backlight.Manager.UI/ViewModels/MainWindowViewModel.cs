using MSI.Keyboard.Backlight.Manager.UI.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace MSI.Keyboard.Backlight.Manager.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRestoreConfigurationService _restoreConfigurationService;

        public ICommand OpenGithubCommand { get; }
        public ICommand RestoreConfigurationCommand { get; }

        public MainWindowViewModel(IRestoreConfigurationService restoreConfigurationService)
        {
            OpenGithubCommand = new RelayCommand<object>(OpenGithub);
            RestoreConfigurationCommand = new RelayCommand<object>(RestoreConfiguration);

            _restoreConfigurationService = restoreConfigurationService;
        }

        private void RestoreConfiguration(object obj)
        {
            _restoreConfigurationService.RestoreIfNeeded();
        }

        private void OpenGithub(object obj)
        {
            Process.Start("cmd", "/C start https://www.softpower.pl");
        }
    }
}
