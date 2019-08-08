using FluentScheduler;
using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using MSI.Keyboard.Backlight.Manager.Jobs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Handlers
{
    public class ApplyConfigurationCommandHandler : IRequestHandler<ApplyConfigurationCommand>
    {
        private readonly IConfigurationRepository _repository;
        private readonly IBacklightTaskbarDependentJob _backlightTaskbarDependentJob;

        public ApplyConfigurationCommandHandler(IConfigurationRepository repository,
                                                         IBacklightTaskbarDependentJob backlightTaskbarDependentJob)
        {
            _repository = repository;
            _backlightTaskbarDependentJob = backlightTaskbarDependentJob;
        }

        public async Task<Unit> Handle(ApplyConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = await _repository.GetConfiguration();

            var registry = new Registry();

            JobManager.StopAndBlock();

            if(configuration.BacklightTaskbarDependent)
            {
                registry.Schedule(_backlightTaskbarDependentJob)
                    .ToRunNow()
                    .AndEvery((int)configuration.RefreshInterval.TotalMilliseconds)
                    .Milliseconds();
            }

            JobManager.Initialize(registry);

            return Unit.Value;
        }
    }
}
