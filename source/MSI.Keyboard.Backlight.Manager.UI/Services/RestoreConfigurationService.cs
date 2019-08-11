using System.Threading.Tasks;
using System.Windows;
using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using MSI.Keyboard.Backlight.Manager.Notifications;
using MSI.Keyboard.Backlight.Manager.Queries;
using MSI.Keyboard.Backlight.Manager.Settings;

namespace MSI.Keyboard.Backlight.Manager.UI.Services
{
    public class RestoreConfigurationService : IRestoreConfigurationService
    {
        private readonly IFrontendAppSettings _frontendAppSettings;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;

        public RestoreConfigurationService(IFrontendAppSettings frontendAppSettings,
                                           INotificationService notificationService,
                                           IMediator mediator)
        {
            _frontendAppSettings = frontendAppSettings;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async void RestoreIfNeeded()
        {
            await RestoreKeyboardConfiguration();
            RestoreFrontendConfiguration();
        }

        private async Task RestoreKeyboardConfiguration()
        {
            if (!_frontendAppSettings.ApplyConfigurationOnStartup)
                return;

            var deviceSupported = await _mediator.Send(new CheckIfDeviceIsSupportedQuery());

            if(!deviceSupported)
            {
                _notificationService.ShowError("Your device is not supported");

                return;
            }

            var configuration = await _mediator.Send(new GetConfigurationQuery());

            await _mediator.Send(new ApplyConfigurationCommand(configuration));
        }

        private void RestoreFrontendConfiguration()
        {
            if (_frontendAppSettings.StartMinimized)
                return;

            var window = Application.Current.MainWindow as MainWindow;

            window.RestoreFromTray();
        }
    }
}
