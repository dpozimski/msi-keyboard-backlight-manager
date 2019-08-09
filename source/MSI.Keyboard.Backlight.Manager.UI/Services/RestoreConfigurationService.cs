using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using MSI.Keyboard.Backlight.Manager.Settings;

namespace MSI.Keyboard.Backlight.Manager.UI.Services
{
    public class RestoreConfigurationService : IRestoreConfigurationService
    {
        private readonly IFrontendAppSettings _frontendAppSettings;
        private readonly IMediator _mediator;

        public RestoreConfigurationService(IFrontendAppSettings frontendAppSettings,
                                           IMediator mediator)
        {
            _frontendAppSettings = frontendAppSettings;
            _mediator = mediator;
        }

        public async void RestoreIfNeeded()
        {
            if (!_frontendAppSettings.ApplyConfigurationOnStartup)
                return;

            var configuration = await _mediator.Send(new GetConfigurationQuery());

            await _mediator.Send(new ApplyConfigurationCommand(configuration));
        }
    }
}
