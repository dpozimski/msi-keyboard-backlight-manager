using System.Threading.Tasks;
using System.Windows;
using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using MSI.Keyboard.Backlight.Manager.Queries;
using MSI.Keyboard.Backlight.Manager.Settings;

namespace MSI.Keyboard.Backlight.Manager.UI.Services
{
    public class KeyboardBacklightService : IKeyboardBacklightService
    {
        private readonly IFrontendAppSettings _frontendAppSettings;
        private readonly IMediator _mediator;

        public KeyboardBacklightService(IFrontendAppSettings frontendAppSettings,
                                        IMediator mediator)
        {
            _frontendAppSettings = frontendAppSettings;
            _mediator = mediator;
        }

        public async Task StopBacklightKeyboardManagement()
        {
            await _mediator.Send(new StopManagementCommand());
        }

        public async Task<bool> IsDeviceSupported()
        {
            return await _mediator.Send(new CheckIfDeviceIsSupportedQuery());
        }

        public async Task<JobsConfiguration> GetConfiguration()
        {
            return await _mediator.Send(new GetConfigurationQuery());
        }

        public async Task ApplyConfiguration(JobsConfiguration configuration)
        {
            await _mediator.Send(new ApplyConfigurationCommand(configuration));
        }

        public async Task RestoreIfNeeded()
        {
            await RestoreKeyboardConfiguration();
            RestoreFrontendConfiguration();
        }

        private async Task RestoreKeyboardConfiguration()
        {
            if (!_frontendAppSettings.ApplyConfigurationOnStartup)
                return;

            var configuration = await _mediator.Send(new GetConfigurationQuery());

            await _mediator.Send(new ApplyConfigurationCommand(configuration));
        }

        private void RestoreFrontendConfiguration()
        {
            var window = Application.Current.MainWindow as MainWindow;

            if (_frontendAppSettings.StartMinimized)
            {
                window.GoToTray();
            }
            else
            {
                window.RestoreFromTray();
            }
        }
    }
}
