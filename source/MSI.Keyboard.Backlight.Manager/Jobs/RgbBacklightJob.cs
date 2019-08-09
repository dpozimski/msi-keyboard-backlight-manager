using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System.Drawing;

namespace MSI.Keyboard.Backlight.Manager.Jobs
{
    public class RgbBacklightJob : IRgbBacklightJob
    {
        private readonly IKeyboardService _keyboardService;
        private readonly IBacklightConfigurationBuilder _backlightConfigurationBuilder;

        public int Intensity { get; set; }
        public bool RequireRefreshing => false;

        public RgbBacklightJob(IKeyboardService keyboardService,
                               IBacklightConfigurationBuilder backlightConfigurationBuilder)
        {
            _keyboardService = keyboardService;
            _backlightConfigurationBuilder = backlightConfigurationBuilder;
        }

        public void Execute()
        {
            var backlightConfiguration = _backlightConfigurationBuilder
                .ForRegion(Enums.Region.Start, Color.Red, Intensity)
                .ForRegion(Enums.Region.Center, Color.Green, Intensity)
                .ForRegion(Enums.Region.End, Color.Blue, Intensity)
                .Build();

            _keyboardService.ApplyConfigurationAsync(backlightConfiguration).Wait();
        }
    }
}
