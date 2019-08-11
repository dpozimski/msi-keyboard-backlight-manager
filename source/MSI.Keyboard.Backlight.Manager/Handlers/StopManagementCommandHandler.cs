using FluentScheduler;
using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Handlers
{
    public class StopManagementCommandHandler : IRequestHandler<StopManagementCommand>
    {
        public Task<Unit> Handle(StopManagementCommand request, CancellationToken cancellationToken)
        {
            JobManager.StopAndBlock();

            return Unit.Task;
        }
    }
}
