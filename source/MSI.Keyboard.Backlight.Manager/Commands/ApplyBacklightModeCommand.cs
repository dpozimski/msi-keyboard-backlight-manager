using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Commands
{
    public class ApplyBacklightModeCommandHandler : IRequest
    {
        public BacklightMode Mode { get; }
    }
}
