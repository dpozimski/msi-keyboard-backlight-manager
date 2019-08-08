using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Commands
{
    public class ApplyBacklightTaskbarDependentCommand : IRequest
    {
        public bool Enabled { get; }

        public ApplyBacklightTaskbarDependentCommand(bool enabled)
        {
            Enabled = enabled;
        }
    }
}
