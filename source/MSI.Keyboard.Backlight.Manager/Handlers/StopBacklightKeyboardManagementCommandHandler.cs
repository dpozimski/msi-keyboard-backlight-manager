using FluentScheduler;
using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Handlers
{
    public class StopBacklightKeyboardManagementCommandHandler : IRequestHandler<StopBacklightKeyboardManagementCommand>
    {
        public Task<Unit> Handle(StopBacklightKeyboardManagementCommand request, CancellationToken cancellationToken)
        {
            JobManager.StopAndBlock();

            return Unit.Task;
        }
    }
}
