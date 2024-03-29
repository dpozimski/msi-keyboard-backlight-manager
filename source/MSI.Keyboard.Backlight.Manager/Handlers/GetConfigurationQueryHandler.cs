﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MSI.Keyboard.Backlight.Manager.Jobs.Models;
using MSI.Keyboard.Backlight.Manager.Queries;

namespace MSI.Keyboard.Backlight.Manager.Commands
{
    public class GetConfigurationQueryHandler : IRequestHandler<GetConfigurationQuery, JobsConfiguration>
    {
        private readonly IBacklightConfigurationRepository _repository;

        public GetConfigurationQueryHandler(IBacklightConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<JobsConfiguration> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetConfiguration();
        }
    }
}
