using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Commands
{
    public class ApplyConfigurationCommand : IRequest
    {
        public BacklightConfiguration Configuration { get; }

        public ApplyConfigurationCommand(BacklightConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}