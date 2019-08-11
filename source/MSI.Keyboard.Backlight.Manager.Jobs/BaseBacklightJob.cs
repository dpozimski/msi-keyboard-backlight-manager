using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Jobs
{
    public abstract class BaseBacklightJob : IBacklightJob
    {
        private readonly IKeyboardService _keyboardService;
        private readonly IBacklightConfigurationBuilder _backlightConfigurationBuilder;

        public int Intensity { get; set; }
        public abstract TimeSpan RefreshInterval { get; }

        public BaseBacklightJob(IKeyboardService keyboardService,
                                IBacklightConfigurationBuilder backlightConfigurationBuilder)
        {
            _keyboardService = keyboardService;
            _backlightConfigurationBuilder = backlightConfigurationBuilder;
        }

        public async Task Execute()
        {
            var configuration = Configure(_backlightConfigurationBuilder);

            await _keyboardService.ApplyConfigurationAsync(configuration);
        }

        protected abstract BacklightConfiguration Configure(IBacklightConfigurationBuilder builder);
    }
}
