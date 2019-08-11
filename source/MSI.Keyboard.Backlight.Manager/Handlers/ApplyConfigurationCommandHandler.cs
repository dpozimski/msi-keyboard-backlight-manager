using FluentScheduler;
using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using MSI.Keyboard.Backlight.Manager.Jobs;
using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Handlers
{
    public class ApplyConfigurationCommandHandler : IRequestHandler<ApplyConfigurationCommand>
    {
        private readonly IBacklightConfigurationRepository _repository;
        private readonly IBacklightJobFactory _backlightJobFactory;

        public ApplyConfigurationCommandHandler(IBacklightConfigurationRepository repository,
                                                IBacklightJobFactory backlightJobFactory)
        {
            _repository = repository;
            _backlightJobFactory = backlightJobFactory;
        }

        public async Task<Unit> Handle(ApplyConfigurationCommand request, CancellationToken cancellationToken)
        {
            await SaveConfiguration(request.Configuration);
            ApplyConfigurationToKeyboardService(request.Configuration);

            return Unit.Value;
        }

        private async Task SaveConfiguration(JobsConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            await _repository.SaveConfiguration(configuration);
        }

        private void ApplyConfigurationToKeyboardService(JobsConfiguration configuration)
        {
            var registry = new Registry();

            JobManager.StopAndBlock();
            JobManager.RemoveAllJobs();

            var backlightJob = _backlightJobFactory.Create(configuration.Mode);

            backlightJob.Intensity = configuration.Intensity;

            var quartzBacklightJob = new QuartzBacklightJobAdapter(backlightJob);
            var timeUnit = registry.Schedule(quartzBacklightJob)
                                   .NonReentrant()
                                   .ToRunNow();

            if (backlightJob.RefreshInterval > TimeSpan.Zero)
            {
                timeUnit.AndEvery((int)backlightJob.RefreshInterval.TotalMilliseconds)
                        .Milliseconds();
            }

            JobManager.Initialize(registry);
        }
    }
}
