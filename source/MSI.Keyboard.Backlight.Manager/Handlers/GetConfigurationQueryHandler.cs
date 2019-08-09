using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Commands
{
    public class GetConfigurationQueryHandler : IRequestHandler<GetConfigurationQuery, BacklightConfiguration>
    {
        private readonly IBacklightConfigurationRepository _repository;

        public GetConfigurationQueryHandler(IBacklightConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<BacklightConfiguration> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetConfiguration();
        }
    }
}
