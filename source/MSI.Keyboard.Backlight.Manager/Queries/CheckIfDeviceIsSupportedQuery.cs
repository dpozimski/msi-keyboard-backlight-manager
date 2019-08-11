using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Queries
{
    public class CheckIfDeviceIsSupportedQuery : IRequest<bool>
    {
    }
}
