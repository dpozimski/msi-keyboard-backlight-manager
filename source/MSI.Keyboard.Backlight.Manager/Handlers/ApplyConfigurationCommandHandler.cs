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
        private readonly IBacklightConfigurationRepository _repository;
        private readonly ITaskbarDependentBacklightJob _taskbarDependentBacklightJob;
        private readonly IRgbBacklightJob _rgbBacklightJob;

        public ApplyConfigurationCommandHandler(IBacklightConfigurationRepository repository,
                                                ITaskbarDependentBacklightJob taskbarDependentBacklightJob,
                                                IRgbBacklightJob rgbBacklightJob)
        {
            _repository = repository;
            _taskbarDependentBacklightJob = taskbarDependentBacklightJob;
            _rgbBacklightJob = rgbBacklightJob;
        }

        public async Task<Unit> Handle(ApplyConfigurationCommand request, CancellationToken cancellationToken)
        {
            await SaveConfiguration(request.Configuration);
            ApplyConfigurationToKeyboardService(request.Configuration);

            return Unit.Value;
        }

        private async Task SaveConfiguration(BacklightConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            await _repository.SaveConfiguration(configuration);
        }

        private void ApplyConfigurationToKeyboardService(BacklightConfiguration configuration)
        {
            var registry = new Registry();

            JobManager.StopAndBlock();

            var backlightJob = GetBacklightJob(configuration.Mode);

            backlightJob.Intensity = configuration.Intensity;

            var timeUnit = registry.Schedule(backlightJob)
                                   .NonReentrant()
                                   .ToRunNow();

            if (backlightJob.RequireRefreshing)
            {
                timeUnit.AndEvery((int)configuration.RefreshInterval.TotalMilliseconds)
                        .Milliseconds();
            }

            JobManager.Initialize(registry);
        }

        private IBacklightJob GetBacklightJob(BacklightMode mode)
        {
            switch(mode)
            {
                case BacklightMode.TaskbarColorDependent:
                    return _taskbarDependentBacklightJob;
                case BacklightMode.Rgb:
                    return _rgbBacklightJob;
                default:
                    throw new NotImplementedException(mode.ToString());
            }
        }
    }
}
