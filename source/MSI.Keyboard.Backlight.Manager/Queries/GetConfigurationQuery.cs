using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Commands
{
    public class GetConfigurationQuery : IRequest<BacklightConfiguration>
    {
    }
}
