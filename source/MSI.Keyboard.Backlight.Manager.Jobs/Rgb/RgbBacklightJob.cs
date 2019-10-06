using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using System;
using System.Drawing;

namespace MSI.Keyboard.Backlight.Manager.Jobs.RgbBacklight
{
    public class RgbBacklightJob : BaseBacklightJob
    {
        public override TimeSpan RefreshInterval => TimeSpan.Zero;

        public RgbBacklightJob(IKeyboardService keyboardService, IBacklightConfigurationBuilder backlightConfigurationBuilder) 
            : base(keyboardService, backlightConfigurationBuilder)
        {
        }

        public override bool CanExecute() => true;

        protected override BacklightConfiguration Configure(IBacklightConfigurationBuilder builder)
        {
            return builder
                    .ForRegion(Enums.Region.Start, Color.Red, Intensity)
                    .ForRegion(Enums.Region.Center, Color.Green, Intensity)
                    .ForRegion(Enums.Region.End, Color.Blue, Intensity)
                    .Build();
        }
    }
}
