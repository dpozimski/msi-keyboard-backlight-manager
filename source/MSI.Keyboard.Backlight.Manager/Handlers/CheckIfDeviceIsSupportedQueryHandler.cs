using MediatR;
using MSI.Keyboard.Backlight.Manager.Queries;
using MSI.Keyboard.Backlight.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Handlers
{
    public class CheckIfDeviceIsSupportedQueryHandler : IRequestHandler<CheckIfDeviceIsSupportedQuery, bool>
    {
        private readonly IKeyboardService _keyboardService;

        public CheckIfDeviceIsSupportedQueryHandler(IKeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public Task<bool> Handle(CheckIfDeviceIsSupportedQuery request, CancellationToken cancellationToken)
        {
            var result = _keyboardService.IsDeviceSupported();

            return Task.FromResult(result);
        }
    }
}
