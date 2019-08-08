using MediatR;
using MSI.Keyboard.Backlight.Manager.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Handlers
{
    public class ApplyBacklightTaskbarDependentCommandHandler : IRequestHandler<ApplyBacklightTaskbarDependentCommand>
    {
        private readonly IConfigurationRepository _backlightConfigurationRepository;
        private readonly IMediator _mediator;

        public ApplyBacklightTaskbarDependentCommandHandler(
            IConfigurationRepository backlightConfigurationRepository,
            IMediator mediator)
        {
            _backlightConfigurationRepository = backlightConfigurationRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ApplyBacklightTaskbarDependentCommand request, CancellationToken cancellationToken)
        {
            var configuration = await _backlightConfigurationRepository.GetConfiguration();

            configuration.BacklightTaskbarDependent = request.Enabled;

            await _backlightConfigurationRepository.SaveConfiguration(configuration);

            return await _mediator.Send(new ApplyConfigurationCommand());
        }
    }
}
